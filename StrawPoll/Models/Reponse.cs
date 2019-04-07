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
        public int CompteurReponse { get; private set; }

        public Reponse(string nomReponse, int fKIdSondage)
        {
            NomReponse = nomReponse;
            FKIdSondage = fKIdSondage;
            NombreVoteReponse = NOMBRE_DE_VOTE_PAR_DEFAULT;
        }

        public Reponse(int idReponse, int fKIdSondage)
        {
            IdReponse = idReponse;
            FKIdSondage = fKIdSondage;
        }
        public Reponse(int idReponse,int nombreVoteReponse, int fKIdSondage)
        {
            IdReponse = idReponse;
            NombreVoteReponse = nombreVoteReponse;
            FKIdSondage = fKIdSondage;
        }
        public Reponse(string nomReponse, int fKIdSondage, int idReponse, int compteurReponse)
        {
            NomReponse = nomReponse;
            FKIdSondage = fKIdSondage;
            IdReponse = idReponse;
            CompteurReponse = compteurReponse;
        }
        public Reponse(string nomReponse, int fKIdSondage, int nombreVote, int idReponse, int pourcentageVote ,int compteurReponse)
        {
            NombreVoteReponse = nombreVote;
            NomReponse = nomReponse;
            FKIdSondage = fKIdSondage;
            IdReponse = idReponse;
            PourcentageVote = pourcentageVote;
            CompteurReponse = compteurReponse;
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

