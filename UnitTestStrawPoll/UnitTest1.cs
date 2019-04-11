using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StrawPoll.Models;
using StrawPoll.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Net;


namespace UnitTestStrawPoll
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void NomSondageValide()
        {
            Sondage testNomSondage = Sondage.AvantInsertionEnBDD("On fait quoi ce soir ?");
            bool resultat = testNomSondage.IsValide();
            Assert.IsTrue(resultat == true, "le résultat doit être true");
        }
        [TestMethod]
        public void NomSondageNonValide()
        {
            Sondage testNomSondage = Sondage.AvantInsertionEnBDD("            ");
            bool resultat = testNomSondage.IsValide();
            Assert.IsTrue(resultat == false, "le résultat doit être false");
        }
        [TestMethod]
        public void NomSondageNonValideNull()
        {
            string sondageNull = null;
            Sondage testNomSondage = Sondage.AvantInsertionEnBDD(sondageNull);
            bool resultat = testNomSondage.IsValide();
            Assert.IsTrue(resultat == false, "le résultat doit être false");
        }
        [TestMethod]
        public void ChoixMultipleNonCocher()
        {
            string choixMultiSondageNonCoche = null;
            Sondage testChoixMultiple = Sondage.AvantInsertionEnBDD("On fait quoi ce soir ?");
            testChoixMultiple.ChoixMultiple(choixMultiSondageNonCoche);
            Assert.IsTrue(testChoixMultiple.MultiSondage == false, "le résultat doit être false");
        }
        [TestMethod]
        public void ChoixMultipleCocher()
        {
            string choixMultiSondageCoche = "on";
            Sondage testChoixMultiple = Sondage.AvantInsertionEnBDD("On fait quoi ce soir ?");
            testChoixMultiple.ChoixMultiple(choixMultiSondageCoche);
            Assert.IsTrue(testChoixMultiple.MultiSondage == true, "le résultat doit être false");
        }
        [TestMethod]
        public void NomQuestionValide()
        {
            Reponse testNomReponse = Reponse.AvantTestSaisieValide("Cinema");
            bool resultat = testNomReponse.IsValide();
            Assert.IsTrue(resultat == true, "le résultat doit être true");
        }
        [TestMethod]
        public void NomQuestionNonValide()
        {
            Reponse testNomReponse = Reponse.AvantTestSaisieValide(" ");
            bool resultat = testNomReponse.IsValide();
            Assert.IsTrue(resultat == false, "le résultat doit être false");
        }
        [TestMethod]
        public void NomQuestionNonValideNull()
        {
            string reponseNull = null;
            Reponse testNomReponse = Reponse.AvantTestSaisieValide(reponseNull);
            bool resultat = testNomReponse.IsValide();
            Assert.IsTrue(resultat == false, "le résultat doit être false");
        }
    }
}

