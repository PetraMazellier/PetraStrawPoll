using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StrawPoll.Controllers;

namespace StrawPoll.Models
{
    public class VoteSondage
    {
        public Sondage SondageAVoter { get; private set; }
        public List<Reponse> ReponseAVoterSondage { get; private set; }    

        public VoteSondage(Sondage sondageAVoter, List<Reponse> reponseAVoterSondage)
        {
            SondageAVoter = sondageAVoter;
            ReponseAVoterSondage = reponseAVoterSondage;           
        }
    }
}