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
        
        // test controle nomSondage est renseigné avec function .IsValide()
        [TestMethod]
        public void NomSondageValide()
        {
            Sondage testNomSondage = Sondage.AvantInsertionEnBDD("On fait quoi ce soir ?");
            bool resultat = testNomSondage.IsValide();
            Assert.IsTrue(resultat == true, "le résultat doit être true");
        }
        // test controle nomSondage est vide avec function .IsValide()
        [TestMethod]
        public void NomSondageNonValide()
        {
            Sondage testNomSondage = Sondage.AvantInsertionEnBDD("            ");
            bool resultat = testNomSondage.IsValide();
            Assert.IsTrue(resultat == false, "le résultat doit être false");
        }
        // test controle nomSondage is null  avec function .IsValide()
        [TestMethod]
        public void NomSondageNonValideNull()
        {
            string sondageNull = null;
            Sondage testNomSondage = Sondage.AvantInsertionEnBDD(sondageNull);
            bool resultat = testNomSondage.IsValide();
            Assert.IsTrue(resultat == false, "le résultat doit être false");
        }
        // test controle choixMultiple par default il doit être false avec function .ChoixMultiple(null)
        [TestMethod]
        public void ChoixMultipleNonCocher()
        {
            string choixMultiSondageNonCoche = null;
            Sondage testChoixMultiple = Sondage.AvantInsertionEnBDD("On fait quoi ce soir ?");
            testChoixMultiple.ChoixMultiple(choixMultiSondageNonCoche);
            Assert.IsTrue(testChoixMultiple.MultiSondage == false, "le résultat doit être false");
        }
        // test contrôle choixMultiple si il est coché il doit être true avec function .ChoixMultiple("on")
        [TestMethod]
        public void ChoixMultipleCocher()
        {
            string choixMultiSondageCoche = "on";
            Sondage testChoixMultiple = Sondage.AvantInsertionEnBDD("On fait quoi ce soir ?");
            testChoixMultiple.ChoixMultiple(choixMultiSondageCoche);
            Assert.IsTrue(testChoixMultiple.MultiSondage == true, "le résultat doit être false");
        }
        // test récuperer Numéro Sécurité avec function .GetNumSecurite()
        [TestMethod]
        public void RecupererNumeroSecurite()
        {
            Sondage testNomSondage = Sondage.AvantInsertionEnBDD("On fait quoi ce soir ?");

            Assert.AreEqual("", testNomSondage.NumSecurite, "Numéro Sécurite doit être zero");
            testNomSondage.GetNumSecurite();
            Assert.AreNotEqual("", testNomSondage.NumSecurite, "Numéro Sécurite ne doit plus être zero");

        }
        // test contrôle nomReponse est renseigné avec function .IsValide()
        [TestMethod]
        public void NomQuestionValide()
        {
            Reponse testNomReponse = Reponse.AvantTestSaisieValide("Cinema");
            bool resultat = testNomReponse.IsValide();
            Assert.IsTrue(resultat == true, "le résultat doit être true");
        }
        // test contrôle nomReponse est vide avec function .IsValide()
        [TestMethod]
        public void NomQuestionNonValide()
        {
            Reponse testNomReponse = Reponse.AvantTestSaisieValide(" ");
            bool resultat = testNomReponse.IsValide();
            Assert.IsTrue(resultat == false, "le résultat doit être false");
        }
        // test contrôle nomReponse est null avec function .IsValide()
        [TestMethod]
        public void NomQuestionNonValideNull()
        {
            string reponseNull = null;
            Reponse testNomReponse = Reponse.AvantTestSaisieValide(reponseNull);
            bool resultat = testNomReponse.IsValide();
            Assert.IsTrue(resultat == false, "le résultat doit être false");
        }
        // test contrôle que la question et tous les Reponses sont valide avec function VerifierSaisieReponseCorrect
        [TestMethod]
        public void TestCreationSondageSaisieCorrect()
        {
            Sondage testNomSondage = Sondage.AvantInsertionEnBDD("On fait quoi ce soir ?");

            string[] reponse = { "Cinema", "Restaurant" };
            bool saisieCorrect = testNomSondage.VerifierSaisieReponseCorrect(reponse);

            Assert.IsTrue(saisieCorrect == true, "la saisie doit être correct");
        }
        // test contrôle la question et tous les Reponses quand question est vide avec function VerifierSaisieReponseCorrect
        [TestMethod]
        public void TestCreationSondageQuestionIncorrect()
        {
            Sondage testNomSondage = Sondage.AvantInsertionEnBDD("   ");
            string[] reponse = { "Cinema", "Restaurant" };

            bool saisieCorrect = testNomSondage.VerifierSaisieReponseCorrect(reponse);
            Assert.IsTrue(saisieCorrect == false, "la saisie ne  doit pas être correct");

        }
        // test contrôle la question et tous les Reponses quand il y a juste une reponse avec function VerifierSaisieReponseCorrect
        [TestMethod]
        public void TestCreationSondageReponseIncorrect()
        {
            Sondage testNomSondage = Sondage.AvantInsertionEnBDD("On fait quoi ce soir ?");
            string[] reponse = { "Cinema", "" };

            bool saisieCorrect = testNomSondage.VerifierSaisieReponseCorrect(reponse);

            Assert.IsTrue(saisieCorrect == false, "la saisie ne  doit pas être correct");
        }
        // test contrôle la question et tous les Reponses quand il y a deux fois exactement la même reponse avec function VerifierSaisieReponseCorrect
        [TestMethod]
        public void TestCreationSondageReponseDoubleIncorrect()
        {
            Sondage testNomSondage = Sondage.AvantInsertionEnBDD("On fait quoi ce soir ?");
            string[] reponse = { "Cinema", "Cinema" };
            bool saisieCorrect = testNomSondage.VerifierSaisieReponseCorrect(reponse);
            Assert.IsTrue(saisieCorrect == false, "la saisie ne  doit pas être correct");
        }
        // test contrôle la question et tous les Reponses quand il y a deux fois exactement la même reponse avec function  du blanc en plus VerifierSaisieReponseCorrect
        [TestMethod]
        public void TestCreationSondageReponseDoubleAvecBlancIncorrect()
        {
            Sondage testNomSondage = Sondage.AvantInsertionEnBDD("On fait quoi ce soir ?");
            string[] reponse = { "Cinema", "Cinema   " };
            bool saisieCorrect = testNomSondage.VerifierSaisieReponseCorrect(reponse);
            Assert.IsTrue(saisieCorrect == false, "la saisie ne  doit pas être correct");
        }
        // test qu'on on crée une sondage le constructeur doit mettre par defaut simple vote
        [TestMethod]
        public void SimpleVoteParDefaut()
        {
            Sondage testNomSondage = Sondage.AvantInsertionEnBDD("On fait quoi ce soir ?");
            Assert.IsFalse(testNomSondage.MultiSondage, "le multiSondage doit être false");
        }
        [TestMethod]
        // test qu'on on crée une sondage avec la saisie multisondage il doit être multiVote
        public void MultiVoteSaisie()
        {
            string choixMultiSondageCoche = "on";
            Sondage testNomSondage = Sondage.AvantInsertionEnBDD("On fait quoi ce soir ?");
            testNomSondage.ChoixMultiple(choixMultiSondageCoche);
            testNomSondage.GetNumSecurite();
            Assert.IsTrue(testNomSondage.MultiSondage, "le multiSondage doit être true");
        }
        // test on crée une sondage avec deux reponses et la saisie multisondage et on verifie qu'il y a bien deux reponses saisit
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
            // on vérifie qu'il y a bien 2 reponses créer
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
            // on vérifie qu'il y a bien 3 reponses créer
            Assert.AreEqual(3, nombreCreer, "le nombre d'enregistrement de reponse doit être 3");
            int resultat = idSondageNouveauCreation - idSondageCreation;
            // on vérifie qu'on récupère bien le id du sondage
            Assert.AreEqual(1, resultat, "le nombre id doit augmente de 1");
            // on devrait trouver l'idSondage
            try
            {
                DataAccess.CreationSondage(nouveauNomSondage);

            }
            catch (Exception)
            {
                Assert.Fail("La création doit récuperer un id");
            }
            // on devrait recuperer le sondage avec le idSondage
            try
            {

                DataAccess.RecupererSondage(idSondageNouveauCreation, out Sondage model1);

            }
            catch (Exception)
            {
                Assert.Fail("Id doit exister");
            }
        }
        // test le cas si idSondage n'existe pas
        [TestMethod]
        public void TestSiIdSondageInexistant()
        {

            try
            {

                DataAccess.RecupererSondage(54544, out Sondage model2);

                Assert.Fail("Il ne devrait pas trouver le Id ");

            }
            catch (Exception)
            {

                // cas attendu
            }
        }
        // on teste la récuperation de tous les reponses de la question
        [TestMethod]
        public void TestSiOnRecupereBienLeSondage()
        {
            Sondage nouveauNomSondage = Sondage.AvantInsertionEnBDD("On fait quoi ce soir ?");
            string[] nouveauReponse = { "Cinema", "Restaurant", "Télé" };
            bool saisieCorrect = nouveauNomSondage.VerifierSaisieReponseCorrect(nouveauReponse);
            Assert.IsTrue(saisieCorrect == true, "la saisie doit être correct");
            nouveauNomSondage.GetNumSecurite();
            int idSondageNouveauCreation = DataAccess.CreationSondage(nouveauNomSondage);
            DataAccess.RecupererSondage(idSondageNouveauCreation, out Sondage model);
            List<Reponse> toutLesReponseDuSondage = DataAccess.RecupererToutLesReponsesDuSondage(model);
           
            
            foreach (var operationCourant in toutLesReponseDuSondage)
            {
                try
                {
                    DataAccess.RecupererReponse(operationCourant.IdReponse, operationCourant.FKIdSondage, out Reponse detailReponse);
                }
                catch (Exception)
                {

                    Assert.Fail("Il devrait trouver la reponse ");
                }
            }
        }
        // on teste si le vote s'enregistre bien pour une reponse
        [TestMethod]
        public void TestSiLeVoteSePasseBien()
        {
            Sondage nouveauNomSondage = Sondage.AvantInsertionEnBDD("On fait quoi ce soir ?");
            string[] nouveauReponse = { "Cinema", "Restaurant", "Télé" };    
            nouveauNomSondage.GetNumSecurite();
            int idSondageNouveauCreation = DataAccess.CreationSondage(nouveauNomSondage);
            DataAccess.RecupererSondage(idSondageNouveauCreation, out Sondage model);
            List<Reponse> toutLesReponseDuSondage = DataAccess.RecupererToutLesReponsesDuSondage(model);
            int nombreTotalModifie = 0;
            foreach (var operationCourant in toutLesReponseDuSondage)
            {
                try
                {
                    DataAccess.RecupererReponse(operationCourant.IdReponse, operationCourant.FKIdSondage, out Reponse detailReponse);

                    DataAccess.AjoutNombreVoteReponse(detailReponse);
                    int nombreVoteApresVote = detailReponse.NombreVoteReponse;
                    nombreTotalModifie = nombreTotalModifie + 1;
                }
                catch (Exception)
                {
                    Assert.Fail("Il devrait trouver la reponse ");
                }
            }
            int resultat = toutLesReponseDuSondage.Count;
            Assert.AreEqual(nombreTotalModifie, resultat, "le nombre de vote doit correspondre à le nombre de reponse");
            foreach (var operationCourant in toutLesReponseDuSondage)
            {
                DataAccess.RecupererReponse(operationCourant.IdReponse, operationCourant.FKIdSondage, out Reponse detailReponse);
                int nombreVoteDepart = detailReponse.NombreVoteReponse;
                DataAccess.AjoutNombreVoteReponse(detailReponse);
                DataAccess.RecupererReponse(operationCourant.IdReponse, operationCourant.FKIdSondage, out Reponse detailReponseApresVote);
                int nombreVoteApresVote = detailReponseApresVote.NombreVoteReponse;
                int resultatVote = nombreVoteApresVote - nombreVoteDepart;
                Assert.AreEqual(1, resultatVote, "le nombre de vote devrait être à 1");
            }

        }
        // on test qu'à la création de sondage le montant de vote total doit être 0 et le pourcentage devrait aussi être 0
        [TestMethod]
        public void RecupererNombreVoteTotal()
        {
            string choixMultiSondageCoche = "on";
            Sondage testNomSondage = Sondage.AvantInsertionEnBDD("On fait quoi ce soir ?");
            string[] reponse = { "Cinema", "Restaurant" };
            testNomSondage.ChoixMultiple(choixMultiSondageCoche);
            testNomSondage.GetNumSecurite();
            int idSondageCreation = DataAccess.CreationSondage(testNomSondage);
            int nombreTotalCreer = Reponse.CreationNouveauReponseDuSondage(reponse, idSondageCreation);
            DataAccess.RecupererSondage(idSondageCreation, out Sondage model);
            List<Reponse> toutLesReponseDuSondage = DataAccess.RecupererToutLesReponsesDuSondage(model);

            foreach (var operationCourant in toutLesReponseDuSondage)
            {
                DataAccess.RecupererReponse(operationCourant.IdReponse, operationCourant.FKIdSondage, out Reponse detailReponse);
                model.GetNombreVoteTotal(detailReponse.NombreVoteReponse);
            }
            int resultat = model.NombreVoteTotal;
          //   on  test le calcul nombreVoteTotal avec la function GetNombreVoteTotal à la création il doit être 0
            Assert.AreEqual(0, resultat, "le nombre total de vote devrait être 0");
            foreach (var operationCourant in toutLesReponseDuSondage)
            {
                //   on  test le calcul pourcentageVote par reponse avec la function GetpourcentageVote à la création il doit être 0
                operationCourant.GetPourcentageVote(model.NombreVoteTotal);
                Assert.AreEqual(0, operationCourant.PourcentageVote, "le pourcentage devrait être à 0 %");
            }
           
        }
        //   on  test le calcul nombreVoteTotal avec la function GetNombreVoteTotal apres Vote
        //   on  test le calcul pourcentageVote par reponse avec la function GetpourcentageVote apres Vote
    
        [TestMethod]
        public void RecupererNombreVoteTotalApresVote()
        {
            string choixMultiSondageCoche = "on";
            Sondage testNomSondage = Sondage.AvantInsertionEnBDD("On fait quoi ce soir ?");
            string[] reponse = { "Cinema", "Restaurant" };
            testNomSondage.ChoixMultiple(choixMultiSondageCoche);
            testNomSondage.GetNumSecurite();
            int idSondageCreation = DataAccess.CreationSondage(testNomSondage);
            int nombreTotalCreer = Reponse.CreationNouveauReponseDuSondage(reponse, idSondageCreation);
            DataAccess.RecupererSondage(idSondageCreation, out Sondage model);
            List<Reponse> toutLesReponseDuSondage = DataAccess.RecupererToutLesReponsesDuSondage(model);

            foreach (var operationCourant in toutLesReponseDuSondage)
            {
                DataAccess.RecupererReponse(operationCourant.IdReponse, operationCourant.FKIdSondage, out Reponse detailReponse);
                model.GetNombreVoteTotal(detailReponse.NombreVoteReponse);
            }
            int resultat = model.NombreVoteTotal;
            Assert.AreEqual(0, resultat, "le nombre total de vote devrait être 0");
            foreach (var operationCourant in toutLesReponseDuSondage)
            {   
                operationCourant.GetPourcentageVote(model.NombreVoteTotal);
                Assert.AreEqual(0, operationCourant.PourcentageVote, "le pourcentage devrait être à 0 %");
            }
            foreach (var operationCourant in toutLesReponseDuSondage)
            {
                DataAccess.AjoutNombreVoteReponse(operationCourant);
                DataAccess.RecupererReponse(operationCourant.IdReponse, operationCourant.FKIdSondage, out Reponse detailApresVoteReponse);
                model.GetNombreVoteTotal(detailApresVoteReponse.NombreVoteReponse);
            }
            //   on  test le calcul nombreVoteTotal avec la function GetNombreVoteTotal apres Vote
            resultat = model.NombreVoteTotal;
            Assert.AreEqual(2, resultat, "le nombre total de vote devrait être 2");
            List<Reponse> toutLesReponseDuSondageApresVote = DataAccess.RecupererToutLesReponsesDuSondage(model);
            foreach (var operationCourant in toutLesReponseDuSondageApresVote)
            {
                //   on  test le calcul pourcentageVote par reponse avec la function GetpourcentageVote apres Vote       
                operationCourant.GetPourcentageVote(model.NombreVoteTotal);
                Assert.AreEqual(50, operationCourant.PourcentageVote, "le pourcentage devrait être à 50 %");
            }
        }
        // On test que à la creation du sondage la vote n'est pas desactiver'
        [TestMethod]
        public void SondageTestNonDesactiver()
        {
            string choixMultiSondageCoche = "on";
            Sondage testNomSondage = Sondage.AvantInsertionEnBDD("On fait quoi ce soir ?");
            string[] reponse = { "Cinema", "Restaurant" };
            testNomSondage.ChoixMultiple(choixMultiSondageCoche);
            testNomSondage.GetNumSecurite();
            int idSondageCreation = DataAccess.CreationSondage(testNomSondage);
            int nombreTotalCreer = Reponse.CreationNouveauReponseDuSondage(reponse, idSondageCreation);
            DataAccess.RecupererSondage(idSondageCreation, out Sondage model);
            Assert.IsFalse(model.EtatSondage, "l'etat du sondage doit être false");            
        }
        // On test la fonction DesactiverSondage() et le Sondage devrait être desactiver apres
        [TestMethod]
        public void SondageTestDesactiver()
        {
            string choixMultiSondageCoche = "on";
            Sondage testNomSondage = Sondage.AvantInsertionEnBDD("On fait quoi ce soir ?");
            string[] reponse = { "Cinema", "Restaurant" };
            testNomSondage.ChoixMultiple(choixMultiSondageCoche);
            testNomSondage.GetNumSecurite();
            int idSondageCreation = DataAccess.CreationSondage(testNomSondage);
            int nombreTotalCreer = Reponse.CreationNouveauReponseDuSondage(reponse, idSondageCreation);
            DataAccess.RecupererSondage(idSondageCreation, out Sondage model);
           // appel function DesactiverSondage
            model.DesactiverSondage();
            Assert.IsTrue(model.EtatSondage, "l'etat du sondage doit être true");
            // on verifie que le update de sondage s'est bien passé
            int nombreModifie = DataAccess.DesactiverVoteSondage(model);
            Assert.AreEqual(1, nombreModifie, "le nombre de mise à jour de table sondage doit être à 1");
        }
    }
}


