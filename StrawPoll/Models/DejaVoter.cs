using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrawPoll.Models
{
    public class DejaVoter
    {
        public Sondage SondageDejaVoter { get; private set; }

        public DejaVoter(Sondage sondageDejaVoter)
        {
            SondageDejaVoter = sondageDejaVoter;

        }
    }
}