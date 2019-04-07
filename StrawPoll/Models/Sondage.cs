using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StrawPoll.Controllers;

namespace StrawPoll.Models
{
    public class Sondage
    {
        private const bool ETAT_PAR_DEFAULT = false;

        public int IdSondage { get; private set; }
        public string NomSondage { get; private set; }
        public bool MultiSondage { get; private set; }
        public bool EtatSondage { get; private set; }
        public int NumSecurite { get; private set; }
        public int NombreVoteTotal { get; private set; }


        public Sondage(string nomSondage, bool multiSondage)
        {
            NomSondage = nomSondage;
            MultiSondage = multiSondage;
            EtatSondage = ETAT_PAR_DEFAULT;
            NumSecurite = GetNumSecurite();
        }

        
        public Sondage(string nomSondage, bool multiSondage,bool etatSondage, int idSondage)
        {
            NomSondage = nomSondage;
            MultiSondage = multiSondage;
            EtatSondage = etatSondage;
            IdSondage = idSondage;
        }
        public Sondage(string nomSondage, bool multiSondage, int idSondage, int nombreVoteTotal)
        {
            NomSondage = nomSondage;
            MultiSondage = multiSondage;
            IdSondage = idSondage;
            NombreVoteTotal = nombreVoteTotal;
        }
        public Sondage(int idSondage, string nomSondage, bool etatSondage, int numSecurite)
        {
            IdSondage = idSondage;
            NumSecurite = numSecurite;
            NomSondage = nomSondage;
            EtatSondage = etatSondage;
        }
        public Sondage(int idSondage, string nomSondage)
        {
            IdSondage = idSondage;
            NomSondage = nomSondage;
            EtatSondage = true;
        }
        public Sondage(int idSondage, int numSecurite)
        {
            IdSondage = idSondage;
            NumSecurite = numSecurite;
        }

        public Sondage(int idSondage)
        {
            IdSondage = idSondage;
        }
        public static int GetNumSecurite()
        {
            Random aleatoire = new Random();
            int entierUnChiffre = aleatoire.Next(10000); //Génère un entier compris entre 0 et 9999
            return entierUnChiffre;
        }
        
        public static bool IsValide(string nom)
        {
            switch (nom)
            {                
                case "":
                    return false;                       
                case null:
                    return false;                   
                default:
                    bool caractereTrouve = false;
                    foreach (char c in nom)
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
        public static bool IsValideNumerique(int? nombre)
        {
            switch (nombre)
            {
                case null:
                    return false;
                default:
                    return true;
            }
        }
        public int GetPourcentageVote(int nombreVoteReponse)
        {
            int pourcentageVote;
            if (NombreVoteTotal > 0)
            {
                return pourcentageVote = nombreVoteReponse * 100 / NombreVoteTotal;
            }
            else
            {
                return pourcentageVote = 0;
            }
        }
        public static bool ChoixMultiple(string multiChoix)
        {
            switch (multiChoix)
            {
                case "on":
                    return true;

                default:
                    return false;
            }
        }
    }
}