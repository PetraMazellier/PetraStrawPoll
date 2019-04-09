using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StrawPoll.Controllers;

namespace StrawPoll.Models
{
    public class Sondage
    {
        private const bool ETAT_UNISONDAGE_DEFAULT = false;
        private const bool ETAT_SONDAGE_NON_DESACTIVER = false;
        private const bool ETAT_DESACTIVER = true;
        private const int NOMBRE_DE_VOTE_TOTAL_PAR_DEFAULT = 0;
        private const int NUMERO_SECURITE_INITIAL = 0;
        private const int IDENTIFIANT_ZERO = 0;
        public int IdSondage { get; private set; }
        public string NomSondage { get; private set; }
        public bool MultiSondage { get; private set; }
        public bool EtatSondage { get; private set; }
        public int NumSecurite { get; private set; }
        public int NombreVoteTotal { get; private set; }


        
        public Sondage(string nomSondage) 
        {
            NomSondage = nomSondage;
            MultiSondage = ETAT_UNISONDAGE_DEFAULT;
            EtatSondage = ETAT_SONDAGE_NON_DESACTIVER;
        }
        public Sondage(int idSondage)
        {
            IdSondage = idSondage;
        }
        
        public Sondage(string nomSondage, bool multiSondage, bool etatSondage, int idSondage, int numSecurite) 
        {
            NomSondage = nomSondage;
            MultiSondage = multiSondage;
            EtatSondage = etatSondage;
            IdSondage = idSondage;
            NumSecurite = numSecurite;
        }

        public Sondage(string nomSondage, bool multiSondage,   bool etatSondage,int idSondage, int numSecurite, int nombreVoteTotal)
        {
            NomSondage = nomSondage;
            MultiSondage = multiSondage;
            EtatSondage = etatSondage;
            IdSondage = idSondage;
            NumSecurite = numSecurite;
            NombreVoteTotal = nombreVoteTotal;
        }
        public void GetNumSecurite()
        {
            Random aleatoire = new Random();
            NumSecurite = aleatoire.Next(10000); //Génère un entier compris entre 0 et 9999            
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
    }
}
