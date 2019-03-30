using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrawPoll.Models
{
    public class CreationSondage
    {
        public Sondage NouveauSondage { get; private set; }
        public List<Reponse> ReponseAuNouveauSondage { get; private set; }

        public CreationSondage (Sondage nouveauSondage, List<Reponse> reponseAuNouveauSondage)
        {
            NouveauSondage = nouveauSondage;
            ReponseAuNouveauSondage = reponseAuNouveauSondage;
        }

    }
}