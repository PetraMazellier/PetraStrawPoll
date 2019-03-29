using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrawPoll.Models
{
    public class Reponse
    {
        
        private const int NOMBRE_DE_VOTE_PAR_DEFAULT = 0;
        public int IdReponse { get; private set; }
        public string NomReponse { get; private set; }
        public int NombreVoteReponse { get; private set; }
      

        public Reponse(string nomReponse) : this (nomReponse, NOMBRE_DE_VOTE_PAR_DEFAULT)
        {           
            NomReponse = nomReponse;
        }
        public Reponse(string nomReponse, int nombreVote) 
        {
            NombreVoteReponse = nombreVote;
            NomReponse = nomReponse;
        }
        public void AjoutVoteReponse()
        {
            this.NombreVoteReponse = NombreVoteReponse + 1;
        }
    }
    public enum Etat
    {
        Creer = 1,
        Desactiver = 2
    }
}

