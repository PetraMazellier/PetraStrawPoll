using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StrawPoll.Controllers;

namespace StrawPoll.Models
{
    public class CreationSondage
    {
        public Sondage NouveauSondage { get; private set; }
        public List<Reponse> ReponseAuNouveauSondage { get; private set; }

        public CreationSondage (Sondage nouveauSondage, List<Reponse> reponseAuNouveauSondage)
        {
            NouveauSondage = nouveauSondage;
            ReponseAuNouveauSondage = reponseAuNouveauSondage;
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
                    return true;                
            }
        }
        public static int GetNumSecurite()
        {
            Random aleatoire = new Random();
            int entierUnChiffre = aleatoire.Next(10000); //Génère un entier compris entre 0 et 9999
            return entierUnChiffre;
        }

    }
}