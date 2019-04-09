using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrawPoll.Models
{
    public class DesactiverInterdit
    {
        public Sondage SondageDesactiverInterdit { get; private set; }

        public DesactiverInterdit(Sondage sondageDesactiverInterdit)
        {
            SondageDesactiverInterdit = sondageDesactiverInterdit;

        }
    }
}