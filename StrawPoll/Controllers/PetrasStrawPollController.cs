using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StrawPoll.Models;
using System.Collections.Specialized;
using System.Net;


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
        #region Affichage Page Web Condition de StrawPoll
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
        public ActionResult Creation(int? nombreReponseMax)
        {

            if (nombreReponseMax == null)
            {
                Creation nombreDeReponse = new Creation();
                return View(nombreDeReponse);
            }
            else
            {
                Creation nombreDeReponse = new Creation(nombreReponseMax.Value);
                return View(nombreDeReponse);
            }

        }
        #endregion
        #region Récuperer la saisie des données à la création
        /// <summary>
        /// Controle de la saisie :
        /// la question et les deux reponses doivent être saisi obligatoirement        
        /// Si saisie incorrect on fait un lien sur affichage CreationInvalide
        /// sinon
        /// on recupere un nombre aleatoire pour le NumSecurite
        /// on cree le sondage
        /// on recupere le IdSondage corresondant
        /// et on cree les reponses correspondant du IdSondage
        /// si tout s'est bien passé on envoit à ConfirmationCreation
        /// sinon on affiche le messae erreur correspondant avc possiblibilté de revenir à l'accueil
        /// </summary>
        /// <param name="question"></param>
        /// <param name="reponse"></param>        
        /// <param name="multiSondageString"></param>
        /// <returns></returns>
        public ActionResult SubmitCreation(int nombreReponseMaximum, string question, string[] reponse, string multiSondageString)
        {
            #region Contrôle que la question et au moins deux réponses sont saisie
            Sondage nouveauSondage = Sondage.AvantInsertionEnBDD(question);
            int nombreDeReponseValide = 0;
            nouveauSondage.ChoixMultiple(multiSondageString);
            nouveauSondage.GetNumSecurite();

            for (int i = 0; i < reponse.Length; i++)
            {
                Reponse testReponse = Reponse.AvantTestSaisieValide(reponse[i]);
                if (testReponse.IsValide())
                {
                    nombreDeReponseValide = nombreDeReponseValide + 1;
                }
            }
            if (!nouveauSondage.IsValide() || nombreDeReponseValide < 2)
            {
                return RedirectToAction("CreationInvalide", new { nombreReponseMaximum = nombreReponseMaximum });
            }
            #endregion
            #region Création du sondage et les réponses saisies
            else
            {
                #region création du sondage avec le numéro sécurité et multiSondage

                int idSondageCreation = DataAccess.CreationSondage(nouveauSondage);

                #region Création autant de réponses que saisie
                int nombreTotalCreer = 0;
                for (int i = 0; i < reponse.Length; i++)
                {
                    Reponse reponseDetail = Reponse.AvantInsertionEnBDD(reponse[i], idSondageCreation);
                    if (reponseDetail.IsValide())
                    {
                        int idReponseCreation = DataAccess.CreationReponse(reponseDetail);
                        nombreTotalCreer = nombreTotalCreer + 1;
                    }
                }
                if (nombreTotalCreer > 1)
                {
                    return RedirectToAction("ConfirmationCreation", new { idSondage = idSondageCreation, numSecurite = nouveauSondage.NumSecurite });
                }
                #endregion
                #region si la création de réponse s'est mal passé on envoie message erreur avec possiblilté de retourner à l'accueil
                else
                {
                    string messageTitre = "Programme s'est arrêté à cause d'une grave erreur ! ";
                    string messageErreur = "Raison de l'arrêt du programme : Problème en récupérant l' Id du Sondage";
                    string commentaireErreur = "Prévenez l'administrateur !!";
                    ErreurGrave nouveauErreur = new ErreurGrave(messageTitre, messageErreur, commentaireErreur);
                    return RedirectToAction("Erreur", new { messageTitre = nouveauErreur.MessageTitre, messageErreur = nouveauErreur.MessageErreur, commentaireErreur = nouveauErreur.CommentaireErreur });
                }
                #endregion

                #endregion

            }
            #endregion
        }
        #endregion
        #region Envoi page web avec affichage que la saisie du sondage n'est pas correcte

        public ActionResult CreationInvalide(int nombreReponseMaximum)
        {
            CreationInvalide nombreReponseMax = new CreationInvalide(nombreReponseMaximum);
            return View(nombreReponseMax);
        }
        #endregion
        #region Envoi page web avec confirmation pour dire que la creation s'est bien passé
        public ActionResult ConfirmationCreation(int idSondage, string numSecurite)
        {

            if (DataAccess.RecupererSondagePourDesactiver(idSondage, numSecurite, out Sondage model))
            {

                ConfirmationCreation nouveauSondage = new ConfirmationCreation(model);
                return View(nouveauSondage);
            }
            else
            {
                string messageTitre = "Programme s'est arrêté à cause d'un grave erreur ! ";
                string messageErreur = "Raison de l'arrêt du programme : Problème en récuperant le sondage créer";
                string commentaireErreur = "Prévenez l'administrateur !!";
                ErreurGrave nouveauErreur = new ErreurGrave(messageTitre, messageErreur, commentaireErreur);
                return RedirectToAction("Erreur", new { messageTitre = nouveauErreur.MessageTitre, messageErreur = nouveauErreur.MessageErreur, commentaireErreur = nouveauErreur.CommentaireErreur });
            }
        }
        #endregion
        #endregion
        #region tous les pages web concernant du vote du Sondage
        #region Affichage page web avec la question, ses reponses soit en choix multiple ou normal
        /// <summary>
        /// Grace du IdSondage on recupère la question dans la base de donnee
        /// si le sondage existe
        ///    on récupère les réponses correspondant
        ///    si la récupération s'est bien passé
        ///      si le sondage est désactiver
        ///           on envoie au VoteInterdit
        ///      si on a déja voter 
        ///           on envoie au DejaVoter
        ///      sinon   on récuperer la saisie de vote 
        ///           on envoi la page avec lien  au SubmitMulit si choix multiple
        ///                                                      sinon le lien est SubmitUni  
        ///    sinon message erreur grave avec possiblilité de revenir à l'accueil     
        /// sinon on affiche message erreur  en invitant la personne de verifier son numéro de sondage avec possiblité de revenir à l'accueil
        /// </summary>
        /// <param name="idSondage"></param>
        /// <returns></returns>
        /// 

        public ActionResult Vote(int? idSondage)
        {
            #region Récuperer le sondage et tous les réponses correpondant pour le vote
            if (!(idSondage is null))
            {

                if (DataAccess.RecupererSondage(idSondage.Value, out Sondage model))
                {
                    #region si le sondage est désactiver au transfert au VoteInterdit pour dire qu'on ne peut plus voter
                    if (model.EtatSondage == true)
                    {
                        return RedirectToAction("VoteInterdit", new { idSondage = idSondage });
                    }
                    #endregion
                    List<Reponse> toutLesReponseDuSondage = DataAccess.RecupererToutLesReponsesDuSondage(model);
                    VoteSondage nouveauVote = new VoteSondage(model, toutLesReponseDuSondage);

                    return View(nouveauVote);
                }
                #endregion
                #region le sondage saisit n'existe pas et on envoie un message erreur on invitant la personne de redemander le numéro sondage à l'ami
                else
                {
                    string messageTitre = "Le Sondage n'existe pas ! ";
                    string messageErreur = "Veuillez redemander le numéro de sondage à votre ami svp !";
                    string commentaireErreur = "Vous pouvez retourner à l'accueil !!";
                    ErreurGrave nouveauErreur = new ErreurGrave(messageTitre, messageErreur, commentaireErreur);
                    return RedirectToAction("Erreur", new { messageTitre = nouveauErreur.MessageTitre, messageErreur = nouveauErreur.MessageErreur, commentaireErreur = nouveauErreur.CommentaireErreur });
                }
            }
            else
            {
                string messageTitre = "Le Sondage n'existe pas ! ";
                string messageErreur = "Veuillez redemander le numéro de sondage à votre ami svp !";
                string commentaireErreur = "Vous pouvez retourner à l'accueil !!";
                ErreurGrave nouveauErreur = new ErreurGrave(messageTitre, messageErreur, commentaireErreur);
                return RedirectToAction("Erreur", new { messageTitre = nouveauErreur.MessageTitre, messageErreur = nouveauErreur.MessageErreur, commentaireErreur = nouveauErreur.CommentaireErreur });
            }
            #endregion
        }
        #endregion
        #region récuperer les votes  en cas votes multiples
        /// <summary>
        /// Traitement  dans le cas du multiChoix
        /// si le passage du sondage s'est bien passe
        ///   on verifie si il y a eu un vote
        ///   si oui
        ///    on récupere le sondage
        ///     si le sondage est désactive
        ///           on revoie au VoteInterdit
        ///     sinon on récupere les reponses voté
        ///           on ajoute un au totalNombre votée de la réponse
        ///           si l'ajout de vote s'est bien passé
        ///             on envoie au écran ConfirmationVote
        ///           sinon on envoie message erreur grave
        ///    sinon on renvoit au affichage vote     
        /// sinon message erreur grave avec possiblilité de revenir à l'accueil
        /// </summary>
        /// <param name="idSondage"></param>
        /// <param name="choix"></param>
        /// <returns></returns>

        public ActionResult SubmitMulti(int? idSondage, int?[] choix)
        {
            #region Test si le sondage existe 
            if (!(idSondage is null))

            {
                if (!(choix is null))
                {
                    #region si il y a une vote on récupère le sondage 
                    if (DataAccess.RecupererSondage(idSondage.Value, out Sondage model))
                    {

                        #region si le sondage est désactiver on l'envoie au VoteInterdit il ne pourrait pas voter mais voir le résultat par contre
                        if (model.EtatSondage == true)
                        {
                            return RedirectToAction("VoteInterdit", new { idSondage = idSondage.Value });
                        }
                        #endregion
                        #region si on a déjà voter on l'envoie au DejaVoter il ne pourrait pluss voter mais voir le résultat par contre
                        if (VerifierSiSondageADejaVoter(Request.Cookies, idSondage.Value))
                        {
                            return RedirectToAction("DejaVoter", new { idSondage = idSondage.Value });
                        }
                        #endregion
                        #region sinon on récupère les réponses correspondant au vote et on ajout un vote

                        int nombreTotalModifie = 0;
                        for (int i = 0; i < choix.Length; i++)
                        {
                            if (DataAccess.RecupererReponse(choix[i].Value, idSondage.Value, out Reponse detailReponse))
                            {
                                EnregistrerVotantDansLeCookie(idSondage.Value);
                                #region on ajoute 1 au nombreVoteReponse et on met à jour la table réponse
                                
                                int nombreModifie = DataAccess.AjoutNombreVoteReponse(detailReponse);
                                if (nombreModifie == 1)
                                {
                                    nombreTotalModifie = nombreTotalModifie + nombreModifie;
                                }
                                #endregion
                            }
                        }

                        #region Si le vote s'est bien passé on envoi au ConfirmationVote
                        if (nombreTotalModifie > 0)
                        {
                            return RedirectToAction("ConfirmationVote", new { idSondage = idSondage.Value });
                        }
                        #endregion
                        #region S' il y a un problème au ajout d'un vote sur la table de réponse on envoie message erreur avec possiblilité de retourner à l'acceuil
                        else
                        {
                            string messageTitre = "Programme s'est arrêté à cause d'une grave erreur ! ";
                            string messageErreur = "Raison de l'arrêt du programme : Problème de base de donnée en votant le sondage";
                            string commentaireErreur = "Prévenez l'administrateur !!";
                            ErreurGrave nouveauErreur = new ErreurGrave(messageTitre, messageErreur, commentaireErreur);
                            return RedirectToAction("Erreur", new { messageTitre = nouveauErreur.MessageTitre, messageErreur = nouveauErreur.MessageErreur, commentaireErreur = nouveauErreur.CommentaireErreur });
                        }
                        #endregion

                    }
                    #endregion
                    #region si la lecture du sondage s'est mal passé on envoi un message erreur correpondant avec possiblilité de retourner à l'accueil
                    else
                    {
                        string messageTitre = "Programme s'est arrêté à cause d'une grave erreur ! ";
                        string messageErreur = "Raison de l'arrêt du programme : Problème de base de donnée en en votant le sondage";
                        string commentaireErreur = "Prévenez l'administrateur !!";
                        ErreurGrave nouveauErreur = new ErreurGrave(messageTitre, messageErreur, commentaireErreur);
                        return RedirectToAction("Erreur", new { messageTitre = nouveauErreur.MessageTitre, messageErreur = nouveauErreur.MessageErreur, commentaireErreur = nouveauErreur.CommentaireErreur });

                    }

                }
                #endregion
                #endregion
                #region sinon on renvoi à la page vote pour que la personne vote
                else
                {
                    return RedirectToAction("Vote", new { idSondage = idSondage });
                }
            }
            #endregion            
            #endregion
            #region sinon on envoie un message d'erreur correpondant avec possiblilté de retourner à l'accueil
            else
            {
                string messageTitre = "Programme s'est arrêté à cause d'une grave erreur ! ";
                string messageErreur = "Raison de l'arrêt du programme : Problème de base de donnée en en votant le sondage";
                string commentaireErreur = "Prévenez l'administrateur !!";
                ErreurGrave nouveauErreur = new ErreurGrave(messageTitre, messageErreur, commentaireErreur);
                return RedirectToAction("Erreur", new { messageTitre = nouveauErreur.MessageTitre, messageErreur = nouveauErreur.MessageErreur, commentaireErreur = nouveauErreur.CommentaireErreur });
            }
        }

        #endregion
        #endregion
        #region récuperer les votes  en cas vote uni
        /// <summary>
        ///  Traitement  dans le cas du Choix Uni
        /// si le passage du sondage s'est bien passe
        ///   on verifie si il y a eu un vote
        ///   si oui
        ///    on récupere le sondage
        ///     si le sondage est désactive
        ///           on revoie au VoteInterdit
        ///     sinon on récupere les reponses voté
        ///           on ajoute un au totalNombre votée de la réponse
        ///           si l'ajout de vote s'est bien passé
        ///             on envoie au écran ConfirmationVote
        ///           sinon on envoie message erreur grave
        ///    sinon on renvoit au affichage vote     
        /// sinon message erreur grave
        /// </summary>
        /// <param name="idSondage"></param>
        /// <param name="reponseRadios"></param>
        /// <returns></returns>
        public ActionResult SubmitUni(int? idSondage, int? reponseRadios)
        {
            #region Test si le sondage existe

            if (!(idSondage is null))
            {
                #region si sondage existe est on a voté
                if (!(reponseRadios is null))
                {
                    #region si il y a une vote on récupère le sondage 
                    if (DataAccess.RecupererSondage(idSondage.Value, out Sondage model))
                    {
                        #region si le sondage est désactiver on l'envoie au VoteInterdit il ne pourrait pas voter mais voir le résultat par contre
                        if (model.EtatSondage == true)
                        {
                            return RedirectToAction("VoteInterdit", new { idSondage = idSondage.Value });
                        }
                        #endregion
                        #region si lon a déjà voter on l'envoie au DejaVoter il ne pourrait pluss voter mais voir le résultat par contre
                        if (VerifierSiSondageADejaVoter(Request.Cookies, idSondage.Value))
                        {
                            return RedirectToAction("DejaVoter", new { idSondage = idSondage.Value });
                        }
                        #endregion
                        #region sinon on récupère la réponse correspondant au vote et on ajout un vote

                        if (DataAccess.RecupererReponse(reponseRadios.Value, idSondage.Value, out Reponse detailReponse))
                        {

                            #region on ajoute 1 au nombreVoteReponse et on met à jour la table réponse
                        
                            int nombreModifie = DataAccess.AjoutNombreVoteReponse(detailReponse);
                            #endregion
                            EnregistrerVotantDansLeCookie(idSondage.Value);
                            #region Si le vote s'est bien passé on envoi au ConfirmationVote
                            if (nombreModifie == 1)
                            {
                                return RedirectToAction("ConfirmationVote", new { idSondage = idSondage.Value });
                            }
                            #endregion
                            #region Si il y a un problème au ajout d'un vote sur la table de réponse on envoie message erreur avec possiblilité de retourner à l'acceuil
                            else
                            {
                                string messageTitre = "Programme s'est arrêté à cause d'une grave erreur ! ";
                                string messageErreur = "Raison de l'arrêt du programme : Problème de base de donnée en en votant le sondage";
                                string commentaireErreur = "Prévenez l'administrateur !!";
                                ErreurGrave nouveauErreur = new ErreurGrave(messageTitre, messageErreur, commentaireErreur);
                                return RedirectToAction("Erreur", new { messageTitre = nouveauErreur.MessageTitre, messageErreur = nouveauErreur.MessageErreur, commentaireErreur = nouveauErreur.CommentaireErreur });
                            }
                        }
                        #endregion
                        #endregion
                        #region si la lecture du sondage s'est mal passé on envoi un message erreur correpondant avec possiblilité de retourner à l'accueil
                        else
                        {
                            string messageTitre = "Programme s'est arrêté à cause d'une grave erreur ! ";
                            string messageErreur = "Raison de l'arrêt du programme : Problème de base de donnée en votant le sondage";
                            string commentaireErreur = "Prévenez l'administrateur !!";
                            ErreurGrave nouveauErreur = new ErreurGrave(messageTitre, messageErreur, commentaireErreur);
                            return RedirectToAction("Erreur", new { messageTitre = nouveauErreur.MessageTitre, messageErreur = nouveauErreur.MessageErreur, commentaireErreur = nouveauErreur.CommentaireErreur });
                        }

                        #endregion
                    }

                    #endregion
                    #region si la lecture du sondage s'est mal passé on envoi un message erreur correpondant avec possiblilité de retourner à l'accueil
                    else
                    {
                        string messageTitre = "Programme s'est arrêté à cause d'une grave erreur ! ";
                        string messageErreur = "Raison de l'arrêt du programme : Problème de base de donnée en votant le sondage";
                        string commentaireErreur = "Prévenez l'administrateur !!";
                        ErreurGrave nouveauErreur = new ErreurGrave(messageTitre, messageErreur, commentaireErreur);
                        return RedirectToAction("Erreur", new { messageTitre = nouveauErreur.MessageTitre, messageErreur = nouveauErreur.MessageErreur, commentaireErreur = nouveauErreur.CommentaireErreur });
                    }
                    #endregion
                }
                #endregion             
                #region sinon on renvoi à la page vote pour que la personne vote
                else
                {
                    return RedirectToAction("Vote", new { idSondage = idSondage });
                }
            }
            #endregion
            #endregion
            #region sinon on envoie un message d'erreur correpondant avec possiblilté de retourner à l'accueil
            else
            {
                string messageTitre = "Programme s'est arrêté à cause d'une grave erreur ! ";
                string messageErreur = "Raison de l'arrêt du programme : Problème en en votant le sondage";
                string commentaireErreur = "Prévienez l'administrateur !!";
                ErreurGrave nouveauErreur = new ErreurGrave(messageTitre, messageErreur, commentaireErreur);
                return RedirectToAction("Erreur", new { messageTitre = nouveauErreur.MessageTitre, messageErreur = nouveauErreur.MessageErreur, commentaireErreur = nouveauErreur.CommentaireErreur });
            }
            #endregion
        }
        #endregion
        #region La personne a déjà voter pour cette sondage
        /// <summary>
        /// Affichage l'écran DejaVoter
        /// c'est dans le cas que la personne a déjà voté pour cette sondage
        /// </summary>
        /// <param name="idSondage"></param>
        /// <returns></returns>
        /// 

        public ActionResult DejaVoter(int idSondage)
        {
            Sondage model = Sondage.RecupererIdSondagePourEcranSuivant(idSondage);
            DejaVoter nouveauSondage = new DejaVoter(model);
            return View(nouveauSondage);

        }
        #endregion
        #region Le sondage est désactiver et on ne peut plus voter
        /// <summary>
        /// Affichage l'écran VoteInterdit
        /// c'est le cas que le sondage est désactiver et on ne peut plus voter
        /// </summary>
        /// <param name="idSondage"></param>
        /// <returns></returns>
        public ActionResult VoteInterdit(int idSondage)
        {
            Sondage model = Sondage.RecupererIdSondagePourEcranSuivant(idSondage);
            VoteInterdit nouveauSondage = new VoteInterdit(model);
            return View(nouveauSondage);
        }
        #endregion
        #region Le vote s'est bien passé et on passe au ConfirmationVote
        /// <summary>
        /// Affichage l'écran ConfirmationVote
        /// c'st le cas que le vote s'est bien passé et on confirme la vote
        /// </summary>
        /// <param name="idSondage"></param>
        /// <returns></returns>
        public ActionResult ConfirmationVote(int idSondage)
        {
            Sondage model = Sondage.RecupererIdSondagePourEcranSuivant(idSondage);
            ConfirmationVote nouveauSondage = new ConfirmationVote(model);
            return View(nouveauSondage);
        }
        #endregion
        #endregion
        #region tous les pages concernant l'affichage du résultat du sondage
        /// <summary>
        /// Grace du IdSondage on recupère la question dans la base de donnee
        /// si le sondage existe 
        ///   on recupère les réponses correspondants du sondage
        ///   si la récuperation se passe bien
        ///     on calcul le pourcentage de chaque vote et le nombre total de vote
        ///     et on affiche le resultat 
        ///   sinon on envoie un message erreur grave avec possiblilité de revenir à l'acceuil        
        /// sinon on affiche message que la personne doit redemander le numéro de sondage à son ami
        /// </summary>
        /// <param name="idSondage"></param>
        /// <returns></returns>
        public ActionResult Resultat(int? idSondage)
        {
            #region Récuperer le sondage si il existe
            if (!(idSondage is null))
            {
                if (DataAccess.RecupererSondage(idSondage.Value, out Sondage model))
                {
                    
                        #region Calcul du pourcentage et nombre total de vote avec affichage du résultat
                        List<Reponse> toutLesReponseDuSondage = DataAccess.RecupererToutLesReponsesDuSondagePourResultatTrierParNombreVote(model);
                        foreach (var reponseCourant in toutLesReponseDuSondage)
                        {
                            model.GetNombreVoteTotal(reponseCourant.NombreVoteReponse); 
                        }
                        foreach (var reponseCourant in toutLesReponseDuSondage)
                        {  
                            reponseCourant.GetPourcentageVote(model.NombreVoteTotal);
                        }
                        ResultatSondage nouveauResultat = new ResultatSondage(model, toutLesReponseDuSondage);
                        return View(nouveauResultat);
                        #endregion
                   
                }
                #endregion
                #region sinon on envoi un écran en invitant la peronne de verifier son numéro de sondage
                else
                {
                    string messageTitre = "Le Sondage n'existe pas ! ";
                    string messageErreur = "Veuillez redemander le numéro de sondage à votre ami svp !";
                    string commentaireErreur = "Vous pouvez retourner à l'accueil !!";
                    ErreurGrave nouveauErreur = new ErreurGrave(messageTitre, messageErreur, commentaireErreur);
                    return RedirectToAction("Erreur", new { messageTitre = nouveauErreur.MessageTitre, messageErreur = nouveauErreur.MessageErreur, commentaireErreur = nouveauErreur.CommentaireErreur });
                }
            }
            else
            {
                string messageTitre = "Le Sondage n'existe pas ! ";
                string messageErreur = "Veuillez redemander le numéro de sondage à votre ami svp !";
                string commentaireErreur = "Vous pouvez retourner à l'accueil !!";
                ErreurGrave nouveauErreur = new ErreurGrave(messageTitre, messageErreur, commentaireErreur);
                return RedirectToAction("Erreur", new { messageTitre = nouveauErreur.MessageTitre, messageErreur = nouveauErreur.MessageErreur, commentaireErreur = nouveauErreur.CommentaireErreur });
            }

        }
        #endregion
        #endregion
        public JsonResult GetNombresVotantsEnTempsReel(int idSondage)
        {

            if (DataAccess.RecupererSondage(idSondage, out Sondage model))
            {               
                    #region Calcul du pourcentage et nombre total de vote avec affichage du résultat
                    List<Reponse> toutLesReponseDuSondage = DataAccess.RecupererToutLesReponsesDuSondagePourResultatTrierParNombreVote(model);
                    foreach (var reponseCourant in toutLesReponseDuSondage)
                    {
                        model.GetNombreVoteTotal(reponseCourant.NombreVoteReponse);
                    }
                    foreach (var reponseCourant in toutLesReponseDuSondage)
                    {
                        reponseCourant.GetPourcentageVote(model.NombreVoteTotal);
                    }
                    ResultatSondage nouveauResultat = new ResultatSondage(model, toutLesReponseDuSondage);
                    return Json(nouveauResultat, JsonRequestBehavior.AllowGet);
                    #endregion               

            }
            #region Affichage d'un écran avec message d'erreur de problème de base de donnée
            ResultatSondage nouveauResultatErreur = null;
            return Json(nouveauResultatErreur, JsonRequestBehavior.AllowGet);

            #endregion

        }


        #region tous les pages concernant de la désactivation du sondage 
        #region Affichage de l'écran de désactivation
        /// <summary>
        /// <summary>
        /// Grace du IdSondage et le numéro de sécurite on recupère la question dans la base de donnee
        /// si le sondage existe
        ///   on récupère les réponses correspondant
        ///   si la récupération s'est bien passé
        ///     si le sondage est déjà désactiver
        ///       on envoie au DésactiverInterdit        
        ///     sinon   on récuperer les données du sondage et on affiche l'écran 
        ///   sinon message erreur grave avec possiblilité de revenir à l'accueil     
        /// sinon on affiche message erreur  en invitant la personne de verifier son numéro de sondage avec possiblité de revenir à l'accueil
        /// </summary>
        /// <param name="idSondage"></param>
        /// <param name="numSecurite"></param>
        /// <returns></returns>
        public ActionResult VoteDesactiver(int? idSondage, string numSecurite)
        {

            if (!(idSondage is null) &
                !(numSecurite is null))
            {

                if (DataAccess.RecupererSondagePourDesactiver(idSondage.Value, numSecurite, out Sondage model))
                {
                    #region Le sondage est déja désactiver
                    if (model.EtatSondage == true)
                    {
                        return RedirectToAction("DesactiverInterdit", new { idSondage = idSondage });
                    }
                    #endregion
                    #region On récupère tous les réponses et on affiche l'écran désactiver
                    else
                    {
                        List<Reponse> toutLesReponseDuSondage = DataAccess.RecupererToutLesReponsesDuSondage(model);
                        VoteDesactiver nouveauDesactiver = new VoteDesactiver(model, toutLesReponseDuSondage);

                        return View(nouveauDesactiver);
                    }
                    #endregion
                }
                #region Le sondage n'existe pas on affiche message erreur en invitant la personne de vérifier le numéro de sondage
                else
                {
                    string messageTitre = "Le Sondage n'existe pas ! ";
                    string messageErreur = "Veuillez revérifier le numéro d'acces pour désactiver le sondage svp !";
                    string commentaireErreur = "Vous pouvez retourner à l'accueil !!";
                    ErreurGrave nouveauErreur = new ErreurGrave(messageTitre, messageErreur, commentaireErreur);
                    return RedirectToAction("Erreur", new { messageTitre = nouveauErreur.MessageTitre, messageErreur = nouveauErreur.MessageErreur, commentaireErreur = nouveauErreur.CommentaireErreur });
                }
            }
            else
            {
                string messageTitre = "Le Sondage n'existe pas ! ";
                string messageErreur = "Veuillez revérifier le numéro d'acces pour désactiver le sondage svp !";
                string commentaireErreur = "Vous pouvez retourner à l'accueil !!";
                ErreurGrave nouveauErreur = new ErreurGrave(messageTitre, messageErreur, commentaireErreur);
                return RedirectToAction("Erreur", new { messageTitre = nouveauErreur.MessageTitre, messageErreur = nouveauErreur.MessageErreur, commentaireErreur = nouveauErreur.CommentaireErreur });
            }
            #endregion
        }
        #endregion
        #region Déasactivation du sondage
        /// <summary>
        /// On récupère le sondage grâce du idSondage
        /// si c'est bien passé
        ///   on vérifie si le sondage est déjà désactivé
        ///   si oui
        ///      on envoi l'écran DesactiverInterdit
        ///   sinon
        ///      on désactive en mettant etatSondage à true
        ///      si la mise à jour s'est bien passé
        ///        on l'envoit à l'écran ConfirmationDesactiver
        ///      sinon
        ///        on envoi un message d'erreur avec possiblilité de revenir à l'accueil
        ///  sinon on affiche message erreur  en invitant la personne de verifier son numéro de sondage avec possiblité de revenir à l'accueil           
        /// </summary>
        /// <param name="idSondage"></param>
        /// <param name="numSecurite"></param>
        /// <returns></returns>
        public ActionResult ConfirmationDesactiver(int idSondage, string numSecurite)
        {
            if (DataAccess.RecupererSondagePourDesactiver(idSondage, numSecurite, out Sondage model))
            {
                if (model.EtatSondage == true)
                {
                    return RedirectToAction("DesactiverInterdit", new { idSondage = idSondage });
                }
                model.DesactiverSondage();
                int nombreModifie = DataAccess.DesactiverVoteSondage(model);
                if (nombreModifie == 1)
                {
                    ConfirmationDesactiver nouveauSondage = new ConfirmationDesactiver(model);
                    return View(nouveauSondage);
                }
                else
                {
                    string messageTitre = "Programme s'est arrêté à cause d'une grave erreur ! ";
                    string messageErreur = "Raison de l'arrêt du programme : Problème en désactivant le sondage";
                    string commentaireErreur = "Prévenez l'administrateur !!";
                    ErreurGrave nouveauErreur = new ErreurGrave(messageTitre, messageErreur, commentaireErreur);
                    return RedirectToAction("Erreur", new { messageTitre = nouveauErreur.MessageTitre, messageErreur = nouveauErreur.MessageErreur, commentaireErreur = nouveauErreur.CommentaireErreur });
                }
            }
            else
            {
                string messageTitre = "Programme s'est arrêté à cause d'une grave erreur ! ";
                string messageErreur = "Raison de l'arrêt du programme : Problème en désactivant le sondage";
                string commentaireErreur = "Prévenez l'administrateur !!";
                ErreurGrave nouveauErreur = new ErreurGrave(messageTitre, messageErreur, commentaireErreur);
                return RedirectToAction("Erreur", new { messageTitre = nouveauErreur.MessageTitre, messageErreur = nouveauErreur.MessageErreur, commentaireErreur = nouveauErreur.CommentaireErreur });
            }

        }
        #endregion
        #region Affichage écran DesactiverInterdit dans le cas que le sondage est déjà désactiver
        public ActionResult DesactiverInterdit(int idSondage)
        {
            Sondage model = Sondage.RecupererIdSondagePourEcranSuivant(idSondage);
            DesactiverInterdit nouveauSondage = new DesactiverInterdit(model);
            return View(nouveauSondage);

        }
        #endregion
        #endregion
        #region L'écran Erreur est utilisé pour envoyer soit si poroblème avec la base de donnée soir que l'identifiant n'existe pas
        public ActionResult Erreur(string messageTitre, string messageErreur, string commentaireErreur)
        {
            ErreurGrave erreurTrouve = new ErreurGrave(messageTitre, messageErreur, commentaireErreur);

            return View(erreurTrouve);
        }
        #endregion
        #region On enregistre l'adresse user dans le cookies avec le sondage correspondant
        public void EnregistrerVotantDansLeCookie(int idSondage)
        {
            string Votant = Request.UserHostAddress;
            HttpCookie cookie = new HttpCookie("CookieUtilisateur" + idSondage);
            cookie.Value = "";
            cookie.Expires = DateTime.MaxValue;
            this.Response.Cookies.Add(cookie);
        }
        #endregion
        #region On teste si l'adresse user se trouve déjà dans le cookie avec l'idSondage correspondant 
        public static bool VerifierSiSondageADejaVoter(HttpCookieCollection cookies, int idSondage)
        {
            return cookies["CookieUtilisateur" + idSondage] != null;
        }
        #endregion
    }
}