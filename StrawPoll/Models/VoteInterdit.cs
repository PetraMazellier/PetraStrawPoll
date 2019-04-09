using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrawPoll.Models
{
    public class VoteInterdit
    {
        public Sondage SondageVoteInterdit { get; private set; }
        public VoteInterdit(Sondage sondageVoteInterdit)
        {
            SondageVoteInterdit = sondageVoteInterdit;
        }
    }
}