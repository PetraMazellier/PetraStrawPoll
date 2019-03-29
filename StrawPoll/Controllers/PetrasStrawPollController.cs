using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StrawPoll.Controllers
{
    public class PetrasStrawPollController : Controller
    {
        // GET: PetrasStrawPoll
        public ActionResult Accueil()
        {
            return View();
        }

        public ActionResult Creation()
        {
            return View();
        }
        public ActionResult Condition()
        {
            return View();
        }
        public ActionResult Vote()
        {
            return View();
        }
        public ActionResult ConfirmationCreation()
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
    }
}