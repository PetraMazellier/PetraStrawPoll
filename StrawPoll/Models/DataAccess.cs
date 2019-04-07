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
        public static void CreationSondage(Sondage creationSondage)
        {
            SqlConnection connection = new SqlConnection(SqlConnectionString);
            connection.Open();
            SqlCommand InsertSondage =
                    new SqlCommand("INSERT INTO Sondage (NomSondage, MultiSondage, EtatSondage, NumSecurite) " +
                    " VALUES (@nomSondage, @multiSondage, @etatSondage, @numSecurite)", connection);
            InsertSondage.Parameters.AddWithValue("@nomSondage", creationSondage.NomSondage);
            InsertSondage.Parameters.AddWithValue("@multiSondage", creationSondage.MultiSondage);
            InsertSondage.Parameters.AddWithValue("@etatSondage", creationSondage.EtatSondage);
            InsertSondage.Parameters.AddWithValue("@numSecurite", creationSondage.NumSecurite);

            InsertSondage.ExecuteNonQuery();

            connection.Close();
        }
        public static bool RecupererIdSondage(Sondage model, out Sondage detailModel)
        {

            SqlConnection firstSelect = new SqlConnection(SqlConnectionString);
            firstSelect.Open();
            SqlCommand selectIdSondage =
                new SqlCommand("SELECT TOP 1 IdSondage FROM Sondage WHERE NomSondage = @nomSondage ORDER BY IdSondage DESC", firstSelect);
            selectIdSondage.Parameters.AddWithValue("@nomSondage", model.NomSondage);
            SqlDataReader dataReader = selectIdSondage.ExecuteReader();

            if (dataReader.Read())
            {
                int idSondage = (int)dataReader["IdSondage"];
                detailModel = new Sondage(idSondage);
                return true;
            }
            else
            {
                detailModel = null;
                return false;
            }
        }
        public static bool RecupererSondage(int idSondage, out Sondage detailSondage)
        {

            SqlConnection SelectSondage = new SqlConnection(SqlConnectionString);
            SelectSondage.Open();
            SqlCommand selectSondage =
                new SqlCommand("SELECT NomSondage,EtatSondage, MultiSondage FROM Sondage where IdSondage = @idSondage", SelectSondage);
            selectSondage.Parameters.AddWithValue("@idSondage", idSondage);
            SqlDataReader dataReader = selectSondage.ExecuteReader();

            if (dataReader.Read())
            {
                string nomSondage = (string)dataReader["NomSondage"];
                bool etatSondage = (bool)dataReader["EtatSondage"];

                bool multiSondage = (bool)dataReader["MultiSondage"];
                detailSondage = new Sondage(nomSondage, multiSondage, etatSondage, idSondage);
                return true;
            }
            else
            {
                detailSondage = null;
                return false;
            }
        }
        public static bool RecupererSondagePourDesactiver(int idSondage, int numSecurite , out Sondage detailSondage)
        {
            SqlConnection SelectSondage = new SqlConnection(SqlConnectionString);
            SelectSondage.Open();
            SqlCommand selectSondage =
                new SqlCommand("SELECT NomSondage,  EtatSondage, NumSecurite FROM Sondage where IdSondage = @idSondage AND NumSecurite = @numSecurite", SelectSondage);
            selectSondage.Parameters.AddWithValue("@idSondage", idSondage);
            selectSondage.Parameters.AddWithValue("@numSecurite", numSecurite);
            SqlDataReader dataReader = selectSondage.ExecuteReader();

            if (dataReader.Read())
            {
                string nomSondage = (string)dataReader["NomSondage"];               
                bool etatSondage = (bool)dataReader["EtatSondage"];  
                detailSondage = new Sondage(idSondage, nomSondage, etatSondage, numSecurite);
                return true;
            }
            else
            {
                detailSondage = null;
                return false;
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
                return nombreSondageModifiees;
            }
            catch
            {
                throw new Exception("Problème base de donnée table Sondage !!!");
            }
        }


        public static void CreationReponse(Reponse reponseSaisie)
        {
            SqlConnection connection = new SqlConnection(SqlConnectionString);
            connection.Open();
            SqlCommand InsertSondage =
                    new SqlCommand("INSERT INTO Reponse (NomReponse, NombreVoteReponse, FKIdSondage) " +
                    " VALUES (@nomReponse, @nombreVoteReponse, @fKIdSondage )", connection);
            InsertSondage.Parameters.AddWithValue("@nomReponse", reponseSaisie.NomReponse);
            InsertSondage.Parameters.AddWithValue("@nombreVoteReponse", reponseSaisie.NombreVoteReponse);
            InsertSondage.Parameters.AddWithValue("@fKIdSondage", reponseSaisie.FKIdSondage);

            InsertSondage.ExecuteNonQuery();

            connection.Close();
        }
        public static bool RecupererReponse( int idReponse,int fKIdSondage, out Reponse detailReponse)
        {

            SqlConnection SelectReponse = new SqlConnection(SqlConnectionString);
            SelectReponse.Open();
            SqlCommand selectSondage =
                new SqlCommand("SELECT NombreVoteReponse FROM Reponse WHERE IdReponse = @idReponse AND FKIdSondage = fKIdSondage", SelectReponse);
            selectSondage.Parameters.AddWithValue("@idReponse", idReponse);
            selectSondage.Parameters.AddWithValue("@fKIdSondage", fKIdSondage);
            SqlDataReader dataReader = selectSondage.ExecuteReader();

            if (dataReader.Read())
            {
                int nombreVoteReponse = (int)dataReader["NombreVoteReponse"];
                
                detailReponse = new Reponse(idReponse, nombreVoteReponse, fKIdSondage);
                return true;
            }
            else
            {
                detailReponse = null;
                return false;
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
                    "SET NombreVoteReponse =  @nombreVoteReponse " +
                    "WHERE IdReponse =  @idReponse AND FKIdSondage = @fKIdSondage");
                updateCommand.Parameters.AddWithValue("@idReponse", ajoutNombreSondage.IdReponse);
                updateCommand.Parameters.AddWithValue("@fKIdSondage", ajoutNombreSondage.FKIdSondage);
                updateCommand.Parameters.AddWithValue("@nombreVoteReponse", ajoutNombreSondage.NombreVoteReponse);

                int nombreSondageModifiees = updateCommand.ExecuteNonQuery();
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
               new SqlCommand("SELECT IdReponse, NomReponse FROM Reponse WHERE FKIdSondage = @fKIdSondage ", sondageEnCours);
            selectSondage.Parameters.AddWithValue("@fKIdSondage", model.IdSondage);

            SqlDataReader dataReader = selectSondage.ExecuteReader();
            while (dataReader.Read())
            {
                compteur = compteur + 1;
                int idReponse = (int)dataReader["IdReponse"];
                string nomReponse = (string)dataReader["NomReponse"];
                Reponse detailList = new Reponse(nomReponse, model.IdSondage, idReponse,compteur);
                resultats.Add(detailList);
            }
            sondageEnCours.Close();

            return resultats;

        }
        public static List<Reponse> RecupererToutLesReponsesDuSondageAvecNombreVote(Sondage modelAvecTotalVote)
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
                int pourcentageVote = modelAvecTotalVote.GetPourcentageVote( nombreVoteReponse);


                Reponse detailList = new Reponse(nomReponse,modelAvecTotalVote.IdSondage, nombreVoteReponse, idReponse,pourcentageVote,compteur);

                resultats.Add(detailList);
            }
            sondageEnCours.Close();

            return resultats;

        }

        public static bool CompteNombreVoteTotal(Sondage model, out Sondage modelAvecNombreTotal)
        {
            SqlConnection sondageEnCours = new SqlConnection(SqlConnectionString);
            sondageEnCours.Open();
            SqlCommand selectSondage =
                new SqlCommand("SELECT SUM(NombreVoteReponse) as 'NombreVoteTotal' FROM Reponse WHERE FKIdSondage = @fKIdSondage ", sondageEnCours);
            selectSondage.Parameters.AddWithValue("@fKIdSondage", model.IdSondage);

            SqlDataReader dataReader = selectSondage.ExecuteReader();
            if (dataReader.Read())
            {
                int nombreVoteTotal = (int)dataReader["NombreVoteTotal"];


                modelAvecNombreTotal = new Sondage(model.NomSondage, model.MultiSondage, model.IdSondage, nombreVoteTotal);
                return true;
            }
            else
            {
                modelAvecNombreTotal = null;
                return false;
            }
        }
    }
}