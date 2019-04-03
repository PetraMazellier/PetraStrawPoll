using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrawPoll.Models
{
    public class ConfirmationCreation
    {
        public Sondage NouveauSondage { get; private set; }       

        public ConfirmationCreation(Sondage nouveauSondage)
        {
            NouveauSondage = nouveauSondage;
      
        }
      
    }
}