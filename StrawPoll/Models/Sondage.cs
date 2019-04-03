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

        public Sondage(string nomSondage, bool multiSondage, bool etatSondage, int idSondage)
        {
            NomSondage = nomSondage;
            MultiSondage = multiSondage;            
            EtatSondage = etatSondage;
            IdSondage = idSondage;
        }
        public Sondage(string nomSondage, bool multiSondage,int idSondage)
        {
            NomSondage = nomSondage;
            MultiSondage = multiSondage;
            IdSondage = idSondage;            
        }
        public Sondage(string nomSondage, bool multiSondage, int idSondage, int nombreVoteTotal)
        {
            NomSondage = nomSondage;
            MultiSondage = multiSondage;
            IdSondage = idSondage;
            NombreVoteTotal = nombreVoteTotal;
        }
        public Sondage( int idSondage, string nomSondage, bool etatSondage)
        {                           
            IdSondage = idSondage;
            NomSondage = nomSondage;
            EtatSondage = true;
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
    }
}