using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrawPoll.Models
{
    public class DesactiverInterdit
    {
        public Sondage NouveauSondage { get; private set; }

        public DesactiverInterdit(Sondage nouveauSondage)
        {
            NouveauSondage = nouveauSondage;

        }
    }
}