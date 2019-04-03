using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StrawPoll.Controllers;

namespace StrawPoll.Models
{
    public class ResultatSondage
    {
       
            public Sondage SondageResultat { get; private set; }
            public List<Reponse> ReponseResultatSondage { get; private set; }
            

            public ResultatSondage(Sondage sondageResultat, List<Reponse> reponseResultatSondage)
            {
                SondageResultat = sondageResultat;
                ReponseResultatSondage = reponseResultatSondage;
                
            }
       
    }
}