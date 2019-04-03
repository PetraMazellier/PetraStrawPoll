using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StrawPoll.Models;


namespace StrawPoll.Controllers
{
    public class PetrasStrawPollController : Controller
    {
        // GET: PetrasStrawPoll
        #region Page Web Sans Model Include juste Pagination page web
        #region Affichage Menu Accueil
        public ActionResult Accueil()
        {
            return View();
        }
        #endregion
        #region Affichage Page Web Condition deu StrawPoll
        public ActionResult Condition()
        {
            return View();
        }
        #endregion
        #region Affichage Page Web Fonctionnement du StrawPoll
        public ActionResult APropos()
        {
            return View();
        }
        #endregion
        #region Affichage Page Web Descriptive Confidentilaité 
        public ActionResult Confidentialite()
        {
            return View();
        }
        #endregion
        #endregion

        #region tous les pages concernant la creation du sondage
        #region Affichage page html pour pouvoir saisir les données
        public ActionResult Creation()
        {
            return View();
        }
        #endregion
        #region Récuperer la saisie des données à la création
        /// <summary>
        /// Controle de la saisie :
        /// la question et les deux premieres reponses doivent être saisi obligatoirement
        /// si la 4 ieme reponse est saisie la 3 ieme reponse doit aussi être saisie
        /// Si saisie incorrect on fait un lien sur affichage CreationInvalide
        /// sinon
        /// on recupere un nombre aleatoire pour le NumSecurite
        /// on cree le sondage
        /// on recupere le IdSondage corresondant
        /// et on cree les reponses correspondant du IdSondage
        /// si tout s'est bien passé on envoit à ConfirmationCreation
        /// sinon arret programme
        /// </summary>
        /// <param name="question"></param>
        /// <param name="reponse1"></param>
        /// <param name="reponse2"></param>
        /// <param name="reponse3"></param>
        /// <param name="reponse4"></param>
        /// <param name="multiSondageString"></param>
        /// <returns></returns>
        public ActionResult SubmitCreation(string question, string reponse1, string reponse2, string reponse3, string reponse4, string multiSondageString)
        {
            if (!CreationSondage.IsValide(question) || !CreationSondage.IsValide(reponse1) || !CreationSondage.IsValide(reponse2) || (CreationSondage.IsValide(reponse4) & !CreationSondage.IsValide(reponse3)))
            {
                return RedirectToAction("CreationInvalide");
            }
            else
            {     
                bool multiSondage = CreationSondage.ChoixMultiple(multiSondageString);
                Sondage nouveauSondage = new Sondage(question, multiSondage);
                List<Reponse> ReponseDuSondage = new List<Reponse>();
                {
                    DataAccess.CreationSondage(nouveauSondage);

                    if (DataAccess.RecupererIdSondage(nouveauSondage, out Sondage idModel))
                    {
                        Reponse premierReponse = new Reponse(reponse1, idModel.IdSondage);
                        ReponseDuSondage.Add(premierReponse);
                        Reponse deuxiemeReponse = new Reponse(reponse2, idModel.IdSondage);
                        ReponseDuSondage.Add(deuxiemeReponse);
                        Reponse troisiemeReponse = new Reponse(reponse3, idModel.IdSondage);
                        if (CreationSondage.IsValide(reponse3))
                        {
                            ReponseDuSondage.Add(troisiemeReponse);
                        }
                        Reponse quatriemeReponse = new Reponse(reponse4, idModel.IdSondage);
                        if (CreationSondage.IsValide(reponse4))
                        {
                            ReponseDuSondage.Add(quatriemeReponse);
                        }
                        
                        CreationSondage listReponse = new CreationSondage(idModel, ReponseDuSondage);
                        foreach (Reponse reponseDetail in listReponse.ReponseAuNouveauSondage)
                        {
                            DataAccess.CreationReponse(reponseDetail);
                        }
                        return RedirectToAction("ConfirmationCreation", new { idSondage = idModel.IdSondage ,numSecurite = nouveauSondage.NumSecurite});
                    }
                    else
                    {
                        string messageErreur = "Probleme en recuperant l' Id du Sondage";
                        return RedirectToAction("Erreur", new { messageErreur = messageErreur });
                    }
                }
            }
        }
        #endregion
        #region Envoi page web avec affichage que la saisie du sondage n'est pas correcte

        public ActionResult CreationInvalide()
        {
            return View();
        }
        #endregion
        #region Envoi page web avec confirmation que la creation s'est bien passé
        public ActionResult ConfirmationCreation(int idSondage,int numSecurite)
        {
            Sondage model = new Sondage(idSondage, numSecurite);
           
            ConfirmationCreation nouveauSondage = new ConfirmationCreation(model);
                
            return View(nouveauSondage);           
            
        }
        #endregion
        #endregion

        #region tous les pages web concernant du vote du Sondage
        #region Affichage page web avec la question, ses reponses soit en choix multiple ou normal
        /// <summary>
        /// Grace du IdSondage on recupère la question et tours les reponses correspondant dans la base de donnee
        /// si tout est bien passe
        /// on envoi la page et on va au SubmitVote correspondant au nombre de question
        /// nouveauVote
        /// </summary>
        /// <param name="idSondage"></param>
        /// <returns></returns>
        public ActionResult Vote(int idSondage)
        {

            if (DataAccess.RecupererSondage(idSondage, out Sondage model))
            {
                if (model.EtatSondage == true)
                {
                    return RedirectToAction("VoteInterdit", new{ idSondage = idSondage });
                }
                List<Reponse> toutLesReponseDuSondage = DataAccess.RecupererToutLesReponsesDuSondage(idSondage);              
                VoteSondage nouveauVote = new VoteSondage(model, toutLesReponseDuSondage);
                
                return View(nouveauVote);
            }
            else
            {
                string messageErreur = "Probleme en recuperant le sondage en votante";
                return RedirectToAction("Erreur", new { messageErreur = messageErreur });
            }
        }
        #endregion
        public ActionResult SubmitMulti(int? idSondage, string IdReponse, string reponse2, string reponse3,string reponse4)
        {

            /*   switch (toutLesReponseDuSondage.Count)
               {
                   case 2:
                       {
                           return View(SubmitVote2);
                       }
                   case 3:
                       {
                           return View(SubmitVote3);
                       }
                   case 4:
                       {
                           return View(SubmitVote4);
                       }
                   default:
                       string messageErreur = "Probleme en recuperant le sondage en votante";
                       return RedirectToAction("Erreur", new { messageErreur = messageErreur });*/
            return View();
        }

        public ActionResult SubmitUni(int? idSondage,string reponseRadios)
        {
            return View();
        }
        public ActionResult DejaVoter()
        {
            return View();
        }
        public ActionResult VoteInterdit(int idSondage)
        {
            return View();
        }
        public ActionResult ConfirmationVote()
        {
            return View();
        }
        #endregion
        public ActionResult Resultat(int idSondage)
        {
            if (DataAccess.RecupererSondage(idSondage, out Sondage model))
            {
                if (DataAccess.CompteNombreVoteTotal(model, out Sondage modelAvecTotalVote))
                {
                    List<Reponse> toutLesReponseDuSondage = DataAccess.RecupererToutLesReponsesDuSondageAvecNombreVote(modelAvecTotalVote);  
                    ResultatSondage nouveauResultat = new ResultatSondage(modelAvecTotalVote, toutLesReponseDuSondage);
                    return View(nouveauResultat);
                }
                else
                {
                    string messageErreur = "Probleme en recuperant le sondage d'un résultat";
                    return RedirectToAction("Erreur", new { messageErreur = messageErreur });
                }
            }
            else
            {
                string messageErreur = "Probleme en recuperant le sondage d'un résultat";
                return RedirectToAction("Erreur", new { messageErreur = messageErreur });
            }
           
        }
        public ActionResult VoteDesactiver(int idSondage, int numSecurite)
        {

            if (DataAccess.RecupererSondagePourDesactiver(idSondage, numSecurite, out Sondage model))
            {               
                List<Reponse> toutLesReponseDuSondage = DataAccess.RecupererToutLesReponsesDuSondage(idSondage);
                VoteDesactiver nouveauDesactiver = new VoteDesactiver(model, toutLesReponseDuSondage);

                return View(nouveauDesactiver);
            }
            else
            {
                string messageErreur = "Probleme en recuperant le sondage en votante";
                return RedirectToAction("Erreur", new { messageErreur = messageErreur });
            }
            
        }
        public ActionResult ConfirmationDesactiver(int idSondage)
        {            
                if (DataAccess.RecupererSondage(idSondage, out Sondage model))
                {
                    if (model.EtatSondage == true)
                    {
                        return RedirectToAction("DesactiverInterdit", new { idSondage = idSondage });
                    }
                Sondage detailSondage = new Sondage(idSondage,model.NomSondage);
                int nombreModifie = DataAccess.DesactiverVoteSondage(detailSondage);
               if(nombreModifie == 1)
                { 
                ConfirmationDesactiver nouveauSondage = new ConfirmationDesactiver(idSondage);
                return View(nouveauSondage);
                }
                else
                {
                    string messageErreur = "Probleme en desactivant le sondage";
                    return RedirectToAction("Erreur", new { messageErreur = messageErreur });
                }
            }
            else
            {
                string messageErreur = "Probleme en recuperant le sondage en desactivant";
                return RedirectToAction("Erreur", new { messageErreur = messageErreur });
            }
            
        }
        public ActionResult DesactiverInterdit(int idSondage)
        {
            DesactiverInterdit nouveauSondage = new DesactiverInterdit(idSondage);
            return View(nouveauSondage);
            
        }
       
        public ActionResult Erreur(string messageErreur)
        {
            ErreurGrave erreurTrouve = new ErreurGrave(messageErreur);

            return View(erreurTrouve);
        }
    }
}