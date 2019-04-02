using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StrawPoll.Controllers;

namespace StrawPoll.Models
{
    public class VoteDesactiver
    {
        public Sondage SondageADesactiver { get; private set; }
        public List<Reponse> ReponseADesactiver { get; private set; }

        public VoteDesactiver(Sondage sondageADesactiver, List<Reponse> reponseADesactiver)
        {
            SondageADesactiver = sondageADesactiver;
            ReponseADesactiver = reponseADesactiver;
        }
    }
}