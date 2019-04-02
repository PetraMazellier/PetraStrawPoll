using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrawPoll.Models
{
    public class DesactiverInterdit
    {
        public int IdSondage { get; private set; }
        public DesactiverInterdit(int idSondage)
        {
            IdSondage = idSondage;
        }
    }
}