using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StrawPoll.Controllers;
using System.Collections.Specialized;
using System.Net;


namespace StrawPoll.Models
{
    public class Sondage
    {
        private const bool ETAT_UNISONDAGE_DEFAULT = false;
        private const bool ETAT_SONDAGE_NON_DESACTIVER = false;

        public int IdSondage { get; private set; }
        public string NomSondage { get; private set; }
        public bool MultiSondage { get; private set; }
        public bool EtatSondage { get; private set; }
        public string NumSecurite { get; private set; }
        public int NombreVoteTotal { get; private set; }

        public Sondage(string nomSondage, bool multiSondage, bool etatSondage, int idSondage, string numSecurite, int nombreVoteTotal)
        {
            NomSondage = nomSondage;
            MultiSondage = multiSondage;
            EtatSondage = etatSondage;
            IdSondage = idSondage;
            NumSecurite = numSecurite;
            NombreVoteTotal = nombreVoteTotal;
        }


        public static Sondage AvantInsertionEnBDD(string nomSondage)
        {
            return new Sondage(nomSondage, ETAT_UNISONDAGE_DEFAULT, ETAT_SONDAGE_NON_DESACTIVER, 0, "", 0);
        }

        public static Sondage RecupererIdSondagePourEcranSuivant(int idSondage)
        {
            return new Sondage("", ETAT_UNISONDAGE_DEFAULT, ETAT_SONDAGE_NON_DESACTIVER, idSondage, "", 0);
        }


        public static Sondage RecupererSondageComplet(string nomSondage, bool multiSondage, bool etatSondage, int idSondage, string numSecurite)
        {
            return new Sondage(nomSondage, multiSondage, etatSondage, idSondage, numSecurite, 0);

        }


        public void GetNumSecurite()
        {
            string numSecurite = GetLettre();                    
            numSecurite += GetChiffre();
            numSecurite += GetLettre();
            NumSecurite = numSecurite;
        }

        public static string GetChiffre()
        {
            Random aleatoire = new Random();
            int chiffre = aleatoire.Next(1000, 1000000); //Génère un entier compris entre 0 et 9999   
            string chiffreTrouve = chiffre.ToString();

            return chiffreTrouve;
        }


        public static string GetLettre()
        {
            Random _random = new Random();
            // Cette methode returns un lettre aleatoire miniscule ou majuscule 
            // ... Entre 'a' et 'Z' inclus.
            int num = _random.Next(0, 25); // Zero à 25
            char let = (char)('A' + num);
            string lettreSpecial = let.ToString();
            return lettreSpecial;
        }
        
      
        public void DesactiverSondage()
        {
            EtatSondage = true;

        }
        public bool IsValide()
        {
            switch (NomSondage)
            {
                case "":
                    return false;
                case null:
                    return false;
                default:
                    bool caractereTrouve = false;
                    foreach (char c in NomSondage)
                    {
                        if (c != ' ')
                        {
                            caractereTrouve = true;
                        }
                    }
                    if (caractereTrouve == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
        }

        public void ChoixMultiple(string multiChoix)
        {
            if (multiChoix == "on")
            {
                MultiSondage = true;
            }
            else
            {
                MultiSondage = false;
            }
        }
        public void GetNombreVoteTotal(int nombreVote)
        {
            NombreVoteTotal = NombreVoteTotal + nombreVote;
        }
        #region Test saisie  reponse correct
        public static bool VerifierSaisieReponseCorrect(Sondage question, string[] reponse)
        {

            int nombreDeReponseValide = 0;
            bool reponseValide = true;
            bool reponseDouble = false;
            if (!question.IsValide())
            {
                return reponseValide = false;
            }
            for (int i = 0; i < reponse.Length; i++)
            {
                Reponse testReponse = Reponse.AvantTestSaisieValide(reponse[i]);

                for (int j = i + 1; j < reponse.Length; j++)
                {
                    if (reponse[i] == reponse[j] & testReponse.IsValide())
                    {
                        reponseDouble = true;
                    }
                }
                if (testReponse.IsValide())
                {
                    nombreDeReponseValide = nombreDeReponseValide + 1;
                }
            }
            if (reponseDouble == true | nombreDeReponseValide < 2)
            {
                reponseValide = false;
            }

            return reponseValide;

        }
        #endregion
    }
}
