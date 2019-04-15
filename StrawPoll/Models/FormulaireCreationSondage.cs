using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrawPoll.Models
{
    public class FormulaireCreationSondage
    {
       
        public string Question { get; private set; }
        public string[] Reponse { get; private set; }
        public string MultiSondageString { get; private set; }
        public FormulaireCreationSondage(string question, string[] reponse, string multiSondageString)
        {
            Question = question;
            Reponse = reponse;
            MultiSondageString = multiSondageString;
           
        }
    }
}