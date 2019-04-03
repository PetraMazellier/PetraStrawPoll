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
        public int IdReponse { get; private set; }
        public string NomReponse { get; private set; }
        public int NombreVoteReponse { get; private set; }
        public int FKIdSondage { get; private set; }
        public int PourcentageVote { get; private set; }

        public Reponse(string nomReponse, int fKIdSondage) 
        {
            NomReponse = nomReponse;
            FKIdSondage = fKIdSondage;
            NombreVoteReponse = NOMBRE_DE_VOTE_PAR_DEFAULT;

        }
        public Reponse(string nomReponse, int fKIdSondage,int idReponse)
        {         
            NomReponse = nomReponse;
            FKIdSondage = fKIdSondage;
            IdReponse = idReponse;
        }
        public Reponse(string nomReponse, int fKIdSondage, int nombreVote, int idReponse,int pourcentageVote)
        {
            NombreVoteReponse = nombreVote;
            NomReponse = nomReponse;
            FKIdSondage = fKIdSondage;
            IdReponse = idReponse;
            PourcentageVote = pourcentageVote;
        }
        public static int GetPourcentageVote(Sondage model, int nombreVoteReponse)
        {
            if(model.NombreVoteTotal > 0)
            {
                return nombreVoteReponse * 100 / model.NombreVoteTotal;
            }
            else
            {
                return 0;
            }
            
        }
        /*   public void AjoutVoteReponse()
           {
               this.NombreVoteReponse = NombreVoteReponse + 1;
           }
       } 
       public enum Etat
       {
           Creer = 1,
           Desactiver = 2
       }*/
    }
}

