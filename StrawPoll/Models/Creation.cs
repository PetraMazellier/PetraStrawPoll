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
        public Creation() : this(REPONSE_PAR_DEFAULT)
        {

        }
        public Creation(int nombreReponseMaximum) 
        {
            NombreReponseMaximum = nombreReponseMaximum;
        }
    }
}