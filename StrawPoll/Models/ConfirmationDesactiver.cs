using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrawPoll.Models
{
    public class ConfirmationDesactiver
    {
        public int IdSondage { get; private set; }
        public ConfirmationDesactiver(int idSondage)
        {
            IdSondage = idSondage;
        }
    }
}