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
            public List<int?> PourcentageReponse { get; private set; }
            public int? NombreReponseTotal { get; private set; }
            public int? NombreVoteTotal { get; private set; }

            public ResultatSondage(Sondage sondageResultat, List<Reponse> reponseResultatSondage,List<int?> pourcentageReponse,int? nombreReponseTotal, int? nombreVoteTotal)
            {
                SondageResultat = sondageResultat;
                ReponseResultatSondage = reponseResultatSondage;
                PourcentageReponse = pourcentageReponse;
                NombreReponseTotal = nombreReponseTotal;
                NombreVoteTotal = nombreVoteTotal;
            }
       
    }
}