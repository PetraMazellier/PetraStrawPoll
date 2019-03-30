using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StrawPoll.Controllers;

namespace StrawPoll.Models
{


    public class Sondage
    {
        private const bool ETAT_PAR_DEFAULT = false;

       
        public int IdSondage { get; private set; }
        public string NomSondage { get; private set; }

        //  public List<ResultatReponse> Reponse { get; private set; }

        public bool MultiSondage { get; private set; }
        public bool EtatSondage { get; private set; }
        public int NumSecurite { get; private set; }
       

        public Sondage(string nomSondage, bool multiSondage, int numSecurite) : this(nomSondage, multiSondage, numSecurite, ETAT_PAR_DEFAULT)
        {
            NomSondage = nomSondage;
            MultiSondage = multiSondage;
            NumSecurite = numSecurite;

        }

        public Sondage(string nomSondage, bool multiSondage, int numSecurite, bool etatSondage)
        {
            NomSondage = nomSondage;
            MultiSondage = multiSondage;
            NumSecurite = numSecurite;
            EtatSondage = etatSondage;           
        }
        public Sondage(string nomSondage, bool multiSondage, int numSecurite, bool etatSondage, int idSondage)
        {
            NomSondage = nomSondage;
            MultiSondage = multiSondage;
            NumSecurite = numSecurite;
            EtatSondage = etatSondage;
            IdSondage = idSondage;
        }

        public void DesactiverSondage()
        {
            this.EtatSondage = true;
        }
    }
}