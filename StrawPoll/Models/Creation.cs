using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StrawPoll.Controllers;

namespace StrawPoll.Models
{
    
    public class Creation
    {
        private const int REPONSE_PAR_DEFAULT = 4;
        public int NombreReponseMaximum { set; get; }
        public int IdSondage { get; private set; }
        public string NumSecurite { get; private set; }
        public Creation() : this(REPONSE_PAR_DEFAULT)
        {

        }
        public Creation(int nombreReponseMaximum) 
        {
            NombreReponseMaximum = nombreReponseMaximum;
        }
       
        public Creation(int idSondage, string numSecurite)
        {
            IdSondage = idSondage;
            NumSecurite = numSecurite;
        }
    }
}