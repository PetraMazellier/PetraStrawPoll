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
        public void RecupererNumeroSecurite()
        {
            Sondage testNomSondage = Sondage.AvantInsertionEnBDD("On fait quoi ce soir ?");

            Assert.AreEqual("", testNomSondage.NumSecurite, "Numéro Sécurite doit être zero");
            testNomSondage.GetNumSecurite();
            Assert.AreNotEqual("", testNomSondage.NumSecurite, "Numéro Sécurite ne doit plus être zero");

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
        [TestMethod]
        public void TestCreationSondageSaisieCorrect()
        {
            Sondage testNomSondage = Sondage.AvantInsertionEnBDD("On fait quoi ce soir ?");
            string[] reponse = { "Cinema", "Restaurant" };
            bool saisieCorrect = testNomSondage.VerifierSaisieReponseCorrect(reponse);
            Assert.IsTrue(saisieCorrect == true, "la saisie doit être correct");
        }
        [TestMethod]
        public void TestCreationSondageQuestionIncorrect()
        {
            Sondage testNomSondage = Sondage.AvantInsertionEnBDD("   ");
            string[] reponse = { "Cinema", "Restaurant" };
            bool saisieCorrect = testNomSondage.VerifierSaisieReponseCorrect(reponse);
            Assert.IsTrue(saisieCorrect == false, "la saisie ne  doit pas être correct");
        }
        [TestMethod]
        public void TestCreationSondageReponseIncorrect()
        {
            Sondage testNomSondage = Sondage.AvantInsertionEnBDD("On fait quoi ce soir ?");
            string[] reponse = { "Cinema", "" };
            bool saisieCorrect = testNomSondage.VerifierSaisieReponseCorrect(reponse);
            Assert.IsTrue(saisieCorrect == false, "la saisie ne  doit pas être correct");
        }
        [TestMethod]
        public void TestCreationSondageReponseDoubleIncorrect()
        {
            Sondage testNomSondage = Sondage.AvantInsertionEnBDD("On fait quoi ce soir ?");
            string[] reponse = { "Cinema", "Cinema" };
            bool saisieCorrect = testNomSondage.VerifierSaisieReponseCorrect(reponse);
            Assert.IsTrue(saisieCorrect == false, "la saisie ne  doit pas être correct");
        }
        [TestMethod]
        public void RecupererIdSondageEnCreation()
        {
            string choixMultiSondageCoche = "on";
            Sondage testNomSondage = Sondage.AvantInsertionEnBDD("On fait quoi ce soir ?");

            string[] reponse = { "Cinema", "Restaurant" };
            bool saisieCorrect = testNomSondage.VerifierSaisieReponseCorrect(reponse);
            Assert.IsTrue(saisieCorrect == true, "la saisie doit être correct");
            testNomSondage.ChoixMultiple(choixMultiSondageCoche);
            testNomSondage.GetNumSecurite();
            int idSondageCreation = DataAccess.CreationSondage(testNomSondage);
            int nombreTotalCreer = Reponse.CreationNouveauReponseDuSondage(reponse, idSondageCreation);
            Assert.AreEqual(2, nombreTotalCreer, "le nombre d'enregistrement de reponse doit être 2");
            string choixMultiSondageNonCoche = "";
            Sondage nouveauNomSondage = Sondage.AvantInsertionEnBDD("On fait quoi ce soir ?");
            string[] nouveauReponse = { "Cinema", "Restaurant", "Télé" };

            saisieCorrect = nouveauNomSondage.VerifierSaisieReponseCorrect(nouveauReponse);
            Assert.IsTrue(saisieCorrect == true, "la saisie doit être correct");
            nouveauNomSondage.ChoixMultiple(choixMultiSondageNonCoche);
            nouveauNomSondage.GetNumSecurite();
            int idSondageNouveauCreation = DataAccess.CreationSondage(nouveauNomSondage);
            int nombreCreer = Reponse.CreationNouveauReponseDuSondage(nouveauReponse, idSondageNouveauCreation);
            Assert.AreEqual(3, nombreCreer, "le nombre d'enregistrement de reponse doit être 3");
            int resultat = idSondageNouveauCreation - idSondageCreation;
            Assert.AreEqual(1, resultat, "le nombre id doit augmente de 1");

            try
            {
                DataAccess.CreationSondage(nouveauNomSondage);

            }
            catch (Exception)
            {
                Assert.Fail("La création doit récuperer un id");
            }

            try
            {
                DataAccess.RecupererSondage(idSondageNouveauCreation, out Sondage model1);

            }
            catch (Exception)
            {
                Assert.Fail("Id doit exister");
            }
            try
            {
                DataAccess.RecupererSondage(54544, out Sondage model2);
                Assert.Fail("Il ne devrait pas trouver le Id ");

            }
            catch (Exception)
            {
                // cas attendu
            }
            DataAccess.RecupererSondage(idSondageNouveauCreation, out Sondage model);
            List<Reponse> toutLesReponseDuSondage = DataAccess.RecupererToutLesReponsesDuSondage(model);
            resultat = toutLesReponseDuSondage.Count;
            Assert.AreEqual(3, resultat, "le nombre de réponse trouve doit être de 3");
           
            foreach (var operationCourant in toutLesReponseDuSondage)
            {
                try
                {
                    DataAccess.RecupererReponse(operationCourant.IdReponse, operationCourant.FKIdSondage, out Reponse detailReponse);


                }
                catch (Exception)
                {

                    Assert.Fail("Il ne devrait trouver la reponse ");

                }


            }
        }
    }
}

