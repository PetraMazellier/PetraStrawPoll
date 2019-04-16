using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StrawPoll.Controllers;

namespace StrawPoll.Models
{
    public class Reponse
    {

        private const int NOMBRE_DE_VOTE_PAR_DEFAULT = 0;
        private const int NOMBRE_COMPTEUR_PAR_DEFAULT = 0;
        public int IdReponse { get; private set; }
        public string NomReponse { get; private set; }
        public int NombreVoteReponse { get; private set; }
        public int FKIdSondage { get; private set; }
        public int PourcentageVote { get; private set; }
        public int CompteurReponse { get; private set; }

        public Reponse(string nomReponse, int fKIdSondage, int idReponse, int nombreVoteReponse)
        {

            NombreVoteReponse = nombreVoteReponse;
            NomReponse = nomReponse;
            FKIdSondage = fKIdSondage;
            IdReponse = idReponse;
           
        }
        
        public static Reponse AvantTestSaisieValide(string nomReponse)
        {
            return new Reponse(nomReponse, 0, 0, 0);
        }
      
        public static Reponse AvantInsertionEnBDD(string nomReponse, int fKIdSondage)
        {
            
            return new Reponse(nomReponse, fKIdSondage, 0, 0);
        }
       
        
        public bool IsValide()
        {
            switch (NomReponse)
            {
                case "":
                    return false;
                case null:
                    return false;
                default:
                    bool caractereTrouve = false;
                    foreach (char c in NomReponse)
                    {
                        if (c != ' ')
                        {
                            caractereTrouve = true;
                        }
                    }
                    if (caractereTrouve == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
        }
        public  void GetPourcentageVote(int nombreVoteTotal)
        {            
            if (nombreVoteTotal > 0)
            {
                 PourcentageVote = NombreVoteReponse * 100 / nombreVoteTotal;
            }
            else
            {
                 PourcentageVote = 0;
            }
        }
        public void GetCompteurReponse(int compteurReponse)
        {
            CompteurReponse = compteurReponse;
        }

        #region Création des enregistrements reponse pour chaque reponse valide
        public static int CreationNouveauReponseDuSondage(string[] reponse, int idSondageCreation)
        {
            int nombreTotalCreer = 0;
            for (int i = 0; i < reponse.Length; i++)
            {
                Reponse reponseDetail = Reponse.AvantInsertionEnBDD(reponse[i], idSondageCreation);
                if (reponseDetail.IsValide())
                {
                    int idReponseCreation = DataAccess.CreationReponse(reponseDetail);
                    nombreTotalCreer = nombreTotalCreer + 1;
                }
            }
            return nombreTotalCreer;
        }
        #endregion
        
       
    }
}

