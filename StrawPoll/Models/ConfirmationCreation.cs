using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrawPoll.Models
{
    public class ConfirmationCreation
    {
        public int IdSondage { get; private set; }
        public ConfirmationCreation (int idSondage)
        {
            IdSondage = idSondage;
        }
    }
}