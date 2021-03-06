﻿using System;
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

        public Sondage(string nomSondage, bool multiSondage, bool etatSondage, int idSondage, string numSecurite)
        {
            NomSondage = nomSondage;
            MultiSondage = multiSondage;
            EtatSondage = etatSondage;
            IdSondage = idSondage;
            NumSecurite = numSecurite;
           
        }


        public static Sondage AvantInsertionEnBDD(string nomSondage)
        {
            return new Sondage(nomSondage, ETAT_UNISONDAGE_DEFAULT, ETAT_SONDAGE_NON_DESACTIVER, 0, "");
        }

        public static Sondage RecupererIdSondagePourEcranSuivant(int idSondage)
        {
            return new Sondage("", ETAT_UNISONDAGE_DEFAULT, ETAT_SONDAGE_NON_DESACTIVER, idSondage, "");
        }
       

        public void GetNumSecurite()
        {
            string numSecurite = GetLettreMajuscule();                    
            numSecurite += GetChiffre();
            
            NumSecurite = numSecurite;
        }

        public static string GetChiffre()
        {
            Random aleatoire = new Random();
            int chiffre = aleatoire.Next(1000, 1000000); //Génère un entier compris entre 0 et 9999   
            string chiffreTrouve = chiffre.ToString();

            return chiffreTrouve;
        }
        public static string GetLettreMajuscule()
        {
            Random randomMajuscule = new Random();
            // Cette methode returns un lettre aleatoire majuscule 
            // ... Entre 'A' et 'Z' inclus.
            int NumAleatoire = randomMajuscule.Next(0, 25); // Zero à 25
            char letMajuscule = (char)('A' + NumAleatoire);
            string lettreSpecialMajuscule = letMajuscule.ToString();
            return lettreSpecialMajuscule;
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
        public bool VerifierSaisieReponseCorrect(string[] reponse)
        {
            int nombreDeReponseValide = 0;
            bool reponseValide = true;
            bool reponseDouble = false;
           
            string[] resultat = new string[reponse.Length];

            for (int i = 0; i < reponse.Length; i++)
            {
                var reponseSansBlanc = "";

                foreach (char c in reponse[i])
                {
                    if (c != ' ')
                    {
                        reponseSansBlanc = reponseSansBlanc + c;
                    }
                }
                resultat[i] = reponseSansBlanc;
            }
            if (!(IsValide()))
            {
                return reponseValide = false;
            }
            for (int i = 0; i < reponse.Length; i++)
            {
                Reponse testReponse = Reponse.AvantTestSaisieValide(reponse[i]);

                for (int j = i + 1; j < reponse.Length; j++)
                {
                    if (resultat[i] == resultat[j] & testReponse.IsValide())
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

