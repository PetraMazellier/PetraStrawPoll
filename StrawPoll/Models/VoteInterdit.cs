using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrawPoll.Models
{
    public class VoteInterdit
    {
        public Sondage SondageDesactiver { get; private set; }

        public VoteInterdit(Sondage sondageDesactiver)
        {
            SondageDesactiver = sondageDesactiver;

        }
    }
}