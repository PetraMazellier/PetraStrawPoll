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
        public int NumSecurite { get; private set; }
        public int NombreVoteTotal { get; private set; }

        public Sondage(string nomSondage, bool multiSondage, bool etatSondage, int idSondage, int numSecurite, int nombreVoteTotal)
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
            return new Sondage(nomSondage, ETAT_UNISONDAGE_DEFAULT, ETAT_SONDAGE_NON_DESACTIVER, 0, 0, 0);           
        }
       
        public static Sondage RecupererIdSondagePourEcranSuivant(int idSondage)
        {
            return new Sondage("", ETAT_UNISONDAGE_DEFAULT, ETAT_SONDAGE_NON_DESACTIVER, idSondage, 0, 0);            
        }


        public static Sondage RecupererSondageComplet(string nomSondage, bool multiSondage, bool etatSondage, int idSondage, int numSecurite) 
        {
            return new Sondage(nomSondage, multiSondage, etatSondage, idSondage, numSecurite, 0);
            
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
