using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrawPoll.Models
{


    public class Sondage
    {
        private const bool ETAT_PAR_DEFAULT = false;

        private const int NOMBRE_DE_VOTE_PAR_DEFAULT = 0;

        public int IdSondage { get; private set; }
        public string NomSondage { get; private set; }

        //  public List<ResultatReponse> Reponse { get; private set; }

        public bool MultiSondage { get; private set; }
        public bool EtatSondage { get; private set; }
        public int NumSecurite { get; private set; }
        public int NombreVoteSondage { get; private set; }

        public Sondage(string nomSondage, bool multiSondage, int numSecurite) : this(nomSondage, multiSondage, numSecurite, ETAT_PAR_DEFAULT, NOMBRE_DE_VOTE_PAR_DEFAULT)
        {
            NomSondage = nomSondage;
            MultiSondage = multiSondage;
            NumSecurite = numSecurite;

        }
        public Sondage(string nomSondage, bool multiSondage, int numSecurite, bool etatSondage, int nombreSondage)
        {
            NomSondage = nomSondage;
            MultiSondage = multiSondage;
            NumSecurite = numSecurite;
            EtatSondage = etatSondage;
            NombreVoteSondage = nombreSondage;
        }
        public void AjoutVoteSondage()
        {
            this.NombreVoteSondage = NombreVoteSondage + 1;
        }
        public void DesactiverSondage()
        {
            this.EtatSondage = true;
        }
    }
}