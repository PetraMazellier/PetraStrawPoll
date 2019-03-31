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

        public Reponse(string nomReponse, int fKIdSondage) : this(nomReponse, fKIdSondage, NOMBRE_DE_VOTE_PAR_DEFAULT)
        {
            NomReponse = nomReponse;
            FKIdSondage = fKIdSondage;
        }
        public Reponse(string nomReponse, int fKIdSondage, int nombreVote)
        {
            NombreVoteReponse = nombreVote;
            NomReponse = nomReponse;
            FKIdSondage = fKIdSondage;
        }
        public Reponse(string nomReponse, int fKIdSondage, int nombreVote, int idReponse)
        {
            NombreVoteReponse = nombreVote;
            NomReponse = nomReponse;
            FKIdSondage = fKIdSondage;
            IdReponse = idReponse;
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

