using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using StrawPoll.Controllers;

namespace StrawPoll.Models
{
    public class DataAccess
    {
        const string SqlConnectionString = @"Server=.\SQLExpress;Database=StrawPoll;Trusted_Connection=Yes";
        public static int CreationSondage(Sondage creationSondage)
        {
            int recupIdSondage = 0;
            SqlConnection connection = new SqlConnection(SqlConnectionString);
            connection.Open();
            SqlCommand InsertSondage =
                    new SqlCommand("INSERT INTO Sondage  (NomSondage, MultiSondage, EtatSondage, NumSecurite) " +
                    "OUTPUT INSERTED.IdSondage  VALUES (@nomSondage, @multiSondage, @etatSondage, @numSecurite)", connection);
            InsertSondage.Parameters.AddWithValue("@nomSondage", creationSondage.NomSondage);
            InsertSondage.Parameters.AddWithValue("@multiSondage", creationSondage.MultiSondage);
            InsertSondage.Parameters.AddWithValue("@etatSondage", creationSondage.EtatSondage);
            InsertSondage.Parameters.AddWithValue("@numSecurite", creationSondage.NumSecurite);            
            recupIdSondage= (int)InsertSondage.ExecuteScalar();
            connection.Close();
            return recupIdSondage;
        }
       
        public static bool RecupererSondage(int idSondage, out Sondage detailSondage)        {

            using (SqlConnection SelectSondage = new SqlConnection(SqlConnectionString))
            {
                SelectSondage.Open();
                SqlCommand selectSondage =
                    new SqlCommand("SELECT NomSondage,EtatSondage, MultiSondage,NumSecurite FROM Sondage where IdSondage = @idSondage", SelectSondage);
                selectSondage.Parameters.AddWithValue("@idSondage", idSondage);
                SqlDataReader dataReader = selectSondage.ExecuteReader();

                if (dataReader.Read())
                {
                    string nomSondage = (string)dataReader["NomSondage"];
                    bool etatSondage = (bool)dataReader["EtatSondage"];
                    bool multiSondage = (bool)dataReader["MultiSondage"];
                    string numSecurite = (string)dataReader["NumSecurite"];
                    detailSondage = Sondage.RecupererSondageComplet(nomSondage, multiSondage, etatSondage, idSondage, numSecurite);
                    return true;
                }
                else
                {
                    detailSondage = null;
                    return false;
                }
            }
        }
        public static bool RecupererSondagePourDesactiver(int idSondage, string numSecurite , out Sondage detailSondage)
        {
            using (SqlConnection SelectSondage = new SqlConnection(SqlConnectionString))
            {
                SelectSondage.Open();
                SqlCommand selectSondage =
                    new SqlCommand("SELECT NomSondage, MultiSondage, EtatSondage, NumSecurite FROM Sondage where IdSondage = @idSondage AND NumSecurite = @numSecurite", SelectSondage);
                selectSondage.Parameters.AddWithValue("@idSondage", idSondage);
                selectSondage.Parameters.AddWithValue("@numSecurite", numSecurite);
                SqlDataReader dataReader = selectSondage.ExecuteReader();

                if (dataReader.Read())
                {
                    string nomSondage = (string)dataReader["NomSondage"];
                    bool etatSondage = (bool)dataReader["EtatSondage"];
                    bool multiSondage = (bool)dataReader["MultiSondage"];
                    detailSondage = Sondage.RecupererSondageComplet(nomSondage, multiSondage, etatSondage, idSondage, numSecurite);

                    return true;
                }
                else
                {
                    detailSondage = null;
                    return false;
                }
            }
        }
        public static int DesactiverVoteSondage(Sondage desactiverSondage)
        {
            try
            {
                SqlConnection connection = new SqlConnection(SqlConnectionString);
                connection.Open();
                SqlCommand updateCommand = connection.CreateCommand();
                updateCommand.CommandText = String.Format(
                    "UPDATE Sondage SET EtatSondage =  @etatSondage WHERE IdSondage =  @idSondage");
                updateCommand.Parameters.AddWithValue("@idSondage", desactiverSondage.IdSondage);
                updateCommand.Parameters.AddWithValue("@etatSondage", desactiverSondage.EtatSondage);
                int nombreSondageModifiees = updateCommand.ExecuteNonQuery();
                connection.Close();
                return nombreSondageModifiees;
            }
            catch
            {
                throw new Exception("Problème base de donnée table Sondage !!!");
            }
        }


        public static int CreationReponse(Reponse reponseSaisie)
        {
            int recupIdReponse = 0;
            SqlConnection connection = new SqlConnection(SqlConnectionString);
            connection.Open();
            SqlCommand InsertSondage =
                    new SqlCommand("INSERT INTO Reponse  (NomReponse, NombreVoteReponse, FKIdSondage) " +
                    " OUTPUT INSERTED.IdReponse  VALUES (@nomReponse, @nombreVoteReponse, @fKIdSondage )", connection);
            InsertSondage.Parameters.AddWithValue("@nomReponse", reponseSaisie.NomReponse);
            InsertSondage.Parameters.AddWithValue("@nombreVoteReponse", reponseSaisie.NombreVoteReponse);
            InsertSondage.Parameters.AddWithValue("@fKIdSondage", reponseSaisie.FKIdSondage);           
            recupIdReponse = (int)InsertSondage.ExecuteScalar();
            connection.Close();
            return recupIdReponse;
        }
        public static bool RecupererReponse( int idReponse,int fKIdSondage, out Reponse detailReponse)
        {
            using (SqlConnection SelectReponse = new SqlConnection(SqlConnectionString))
            {

                SelectReponse.Open();
                SqlCommand selectSondage =
                    new SqlCommand("SELECT NombreVoteReponse, NomReponse FROM Reponse WHERE IdReponse = @idReponse AND FKIdSondage = fKIdSondage", SelectReponse);
                selectSondage.Parameters.AddWithValue("@idReponse", idReponse);
                selectSondage.Parameters.AddWithValue("@fKIdSondage", fKIdSondage);
                SqlDataReader dataReader = selectSondage.ExecuteReader();

                if (dataReader.Read())
                {
                    int nombreVoteReponse = (int)dataReader["NombreVoteReponse"];
                    string nomReponse = (string)dataReader["NomReponse"];

                    detailReponse = Reponse.RecuperationDansLaBDD(nomReponse, fKIdSondage, idReponse, nombreVoteReponse);
                    return true;
                }
                else
                {
                    detailReponse = null;
                    return false;
                }
            }
        }
        public static int AjoutNombreVoteReponse(Reponse ajoutNombreSondage)
        {
            try
            {
                SqlConnection connection = new SqlConnection(SqlConnectionString);
                connection.Open();
                SqlCommand updateCommand = connection.CreateCommand();
                updateCommand.CommandText = String.Format(
                    "UPDATE Reponse " +
                    "SET NombreVoteReponse = NombreVoteReponse + 1" +
                    "WHERE IdReponse =  @idReponse AND FKIdSondage = @fKIdSondage");
                updateCommand.Parameters.AddWithValue("@idReponse", ajoutNombreSondage.IdReponse);
                updateCommand.Parameters.AddWithValue("@fKIdSondage", ajoutNombreSondage.FKIdSondage);
          

                int nombreSondageModifiees = updateCommand.ExecuteNonQuery();
                connection.Close();
                return nombreSondageModifiees;
            }

            catch
            {
                throw new Exception("Problème base de donnée table Réponse !!!");
            }
        }
        public static List<Reponse> RecupererToutLesReponsesDuSondage(Sondage model)
        {
            List<Reponse> resultats = new List<Reponse>();
            int compteur = 0;
            SqlConnection sondageEnCours = new SqlConnection(SqlConnectionString);
            sondageEnCours.Open();
            SqlCommand selectSondage =
               new SqlCommand("SELECT IdReponse, NomReponse, NombreVoteReponse FROM Reponse WHERE FKIdSondage = @fKIdSondage ", sondageEnCours);
            selectSondage.Parameters.AddWithValue("@fKIdSondage", model.IdSondage);

            SqlDataReader dataReader = selectSondage.ExecuteReader();
            while (dataReader.Read())
            {
                compteur = compteur + 1;
                int idReponse = (int)dataReader["IdReponse"];
                string nomReponse = (string)dataReader["NomReponse"];
                int nombreVoteReponse = (int)dataReader["NombreVoteReponse"];
                Reponse detailList = new Reponse(nomReponse, model.IdSondage, idReponse,nombreVoteReponse,compteur);              
                resultats.Add(detailList);
            }
            sondageEnCours.Close();

            return resultats;
        }
        public static List<Reponse> RecupererToutLesReponsesDuSondagePourResultatTrierParNombreVote(Sondage modelAvecTotalVote)
        {
            List<Reponse> resultats = new List<Reponse>();
            int compteur = 0;
            SqlConnection sondageEnCours = new SqlConnection(SqlConnectionString);
            sondageEnCours.Open();
            SqlCommand selectSondage =
               new SqlCommand("SELECT IdReponse, NomReponse, NombreVoteReponse FROM Reponse WHERE FKIdSondage = @fKIdSondage ORDER BY NombreVoteReponse DESC", sondageEnCours);
            selectSondage.Parameters.AddWithValue("@fKIdSondage", modelAvecTotalVote.IdSondage);

            SqlDataReader dataReader = selectSondage.ExecuteReader();
            while (dataReader.Read())
            {
                compteur = compteur + 1;
                int idReponse = (int)dataReader["IdReponse"];
                string nomReponse = (string)dataReader["NomReponse"];
                int nombreVoteReponse = (int)dataReader["NombreVoteReponse"];  
                Reponse detailList = new Reponse(nomReponse,modelAvecTotalVote.IdSondage,idReponse, nombreVoteReponse,compteur);
                detailList.GetPourcentageVote(modelAvecTotalVote.NombreVoteTotal);
                resultats.Add(detailList);
            }
            sondageEnCours.Close();

            return resultats;
        }

      
    }
}