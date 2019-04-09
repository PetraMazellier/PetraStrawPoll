using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrawPoll.Models
{
    public class ConfirmationVote
    {
        public Sondage SondageConfirmationVote { get; private set; }
        public ConfirmationVote(Sondage sondageConfirmationVote)
        {
            SondageConfirmationVote = sondageConfirmationVote;
        }
    }
}