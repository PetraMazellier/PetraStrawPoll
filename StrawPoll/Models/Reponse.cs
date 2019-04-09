using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StrawPoll.Controllers;

namespace StrawPoll.Models
{
    public class Reponse
    {

        private const int NOMBRE_DE_VOTE_PAR_DEFAULT = 0;
        private const int NOMBRE_COMPTEUR_PAR_DEFAULT = 0;
        public int IdReponse { get; private set; }
        public string NomReponse { get; private set; }
        public int NombreVoteReponse { get; private set; }
        public int FKIdSondage { get; private set; }
        public int PourcentageVote { get; private set; }
        public int CompteurReponse { get; private set; }

        public Reponse(string nomReponse) 
        {
            NomReponse = nomReponse;

        }
       public Reponse(string nomReponse, int fKIdSondage) 
        {
            NomReponse = nomReponse;
            FKIdSondage = fKIdSondage;

        }
        
      
        public Reponse(string nomReponse, int fKIdSondage,int idReponse, int nombreVoteReponse) 
        {
            NombreVoteReponse = nombreVoteReponse;
            NomReponse = nomReponse;
            FKIdSondage = fKIdSondage;
            IdReponse = idReponse;

        }
        public Reponse(string nomReponse, int fKIdSondage, int idReponse, int nombreVoteReponse,int compteurReponse)
        {
        
            NombreVoteReponse = nombreVoteReponse;
            NomReponse = nomReponse;
            FKIdSondage = fKIdSondage;
            IdReponse = idReponse;            
            CompteurReponse = compteurReponse;
        }

        public bool IsValide()
        {
            switch (NomReponse)
            {
                case "":
                    return false;
                case null:
                    return false;
                default:
                    bool caractereTrouve = false;
                    foreach (char c in NomReponse)
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
        public  void GetPourcentageVote(int nombreVoteTotal)
        {
            
            if (nombreVoteTotal > 0)
            {
                 PourcentageVote = NombreVoteReponse * 100 / nombreVoteTotal;
            }
            else
            {
                 PourcentageVote = 0;
            }
        }
        public void AjoutVoteReponse()
        {
            this.NombreVoteReponse = NombreVoteReponse + 1;
        }

       /* public enum Etat
        {
            Creer = 1,
            Desactiver = 2
        }*/
    }
}

