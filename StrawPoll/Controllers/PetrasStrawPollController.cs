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
        public ActionResult Accueil()
        {
            return View();
        }
        public ActionResult Condition()
        {
            return View();
        }
        public ActionResult APropos()
        {
            return View();
        }
        public ActionResult Confidentialite()
        {
            return View();
        }
        public ActionResult Creation()
        {
            return View();
        }
        public ActionResult SubmitCreation(string question, string reponse1, string reponse2, string reponse3, string reponse4, string multiSondageString)
        {
              if (question == "" || question == null || reponse1 == "" || reponse1 == null || reponse2 == "" || reponse2 == null)
                {
                    return RedirectToAction("CreationInvalide");
                }
            bool multiSondage;
              if (multiSondageString == "on")
            {
                multiSondage = true;
            }
              else
            {
                multiSondage = false;
            }

            Random aleatoire = new Random();          
            int entierUnChiffre = aleatoire.Next(10000); //Génère un entier compris entre 0 et 9999
           
            Sondage nouveauSondage = new Sondage(question, multiSondage, entierUnChiffre);            
            DataAccess.CreationSondage(nouveauSondage);

            if (DataAccess.RecupererIdSondage(nouveauSondage, out Sondage idModel))
            {
                List<Reponse> ReponseDuSondage = new List<Reponse>();                
                Reponse premierReponse = new Reponse(reponse1, idModel.IdSondage);
                ReponseDuSondage.Add(premierReponse);
                Reponse deuxiemeReponse = new Reponse(reponse2, idModel.IdSondage);
                ReponseDuSondage.Add(deuxiemeReponse);               
                if(reponse3 != "" & reponse3 != null)
                {
                    Reponse troisiemeReponse = new Reponse(reponse3, idModel.IdSondage);
                    ReponseDuSondage.Add(troisiemeReponse);                   
                }
                if (reponse4 != "" & reponse4 != null)
                {
                    Reponse quatriemeReponse = new Reponse(reponse4, idModel.IdSondage);
                    ReponseDuSondage.Add(quatriemeReponse);                    
                }
                CreationSondage listReponse = new CreationSondage(idModel, ReponseDuSondage);
                DataAccess.CreationReponse(listReponse);
                return RedirectToAction("ConfirmationCreation", new { idSondage = idModel.IdSondage });
            }
            else
            {
                string messageErreur = "Probleme en recuperant l' Id du Sondage";
                return RedirectToAction("Erreur", new { messageErreur = messageErreur });
            }  
        } 
        public ActionResult CreationInvalide()
        {
            return View();
        }
        public ActionResult ConfirmationCreation(int idSondage)
        {
            return View();
        }
        public ActionResult Vote()
        {
            return View();
        }
        public ActionResult Resultat()
        {
            return View();
        }
        public ActionResult DejaVoter()
        {
            return View();
        }
        public ActionResult VoteInterdit()
        {
            return View();
        }
        public ActionResult ConfirmationVote()
        {
            return View();
        }
        public ActionResult ConfirmationDesactiver()
        {
            return View();
        }
        public ActionResult DesactiverInterdit()
        {
            return View();
        }
        public ActionResult VoteDesactiver()
        {
            return View();
        }
        public ActionResult Erreur(string messageErreur)
        {
            ErreurGrave erreurTrouve = new ErreurGrave(messageErreur);
           
            return View(erreurTrouve);
        }
    }
}