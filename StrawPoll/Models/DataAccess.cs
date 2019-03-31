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

                detailModel = new Sondage(model.NomSondage, model.MultiSondage, model.NumSecurite, model.EtatSondage, idSondage);

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
                new SqlCommand("SELECT NomSondage, MultiSondage, EtatSondage, NumSecurite FROM Sondage where IdSondage = @idSondage", SelectSondage);
            selectSondage.Parameters.AddWithValue("@idSondage", idSondage);
            SqlDataReader dataReader = selectSondage.ExecuteReader();

            if (dataReader.Read())
            {
                string nomSondage = (string)dataReader["NomSondage"];
                bool multiSondage = (bool)dataReader["MultiSondage"];
                bool etatSondage = (bool)dataReader["EtatSondage"];
                int numSecurite = (int)dataReader["NumSecurite"];
               
                detailSondage = new Sondage(nomSondage, multiSondage, numSecurite, etatSondage);
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
                    "UPDATE Sondage " +
                    "SET EtatSondage =  true " +
                    "WHERE IdSondage =  @idSondage");
                updateCommand.Parameters.AddWithValue("@idSondage", desactiverSondage.IdSondage);
                int nombreSondageModifiees = updateCommand.ExecuteNonQuery();
                return nombreSondageModifiees;
            }
            catch
            {
                throw new Exception("Problème base de donnée table Sondage !!!");
            }
        }
        public static void CreationReponse( CreationSondage reponseSaisie)
        {
            foreach (Reponse reponseDetail in reponseSaisie.ReponseAuNouveauSondage)
            {
                SqlConnection connection = new SqlConnection(SqlConnectionString);
                connection.Open();
                SqlCommand InsertSondage =
                        new SqlCommand("INSERT INTO Reponse (NomReponse, NombreVoteReponse, FKIdSondage) " +
                        " VALUES (@nomReponse, @nombreVoteReponse, @fKIdSondage )", connection);
                InsertSondage.Parameters.AddWithValue("@nomReponse", reponseDetail.NomReponse);
                InsertSondage.Parameters.AddWithValue("@nombreVoteReponse", reponseDetail.NombreVoteReponse);
                InsertSondage.Parameters.AddWithValue("@fKIdSondage", reponseDetail.FKIdSondage);

                InsertSondage.ExecuteNonQuery();

                connection.Close();
            }
        }
        public static int AjoutNombreVoteReponse( Reponse ajoutNombreSondage)
        {
            try
            {
                SqlConnection connection = new SqlConnection(SqlConnectionString);
                connection.Open();
                SqlCommand updateCommand = connection.CreateCommand();
                updateCommand.CommandText = String.Format(
                    "UPDATE Reponse " +
                    "SET NombreVoteReponse =  NombreVoteReponse + 1 " +
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
        public static List<Reponse> RecupererToutLesReponsesDuSondage(int idSondage)
        {
            List<Reponse> resultats = new List<Reponse>();
            SqlConnection sondageEnCours = new SqlConnection(SqlConnectionString);
            sondageEnCours.Open();
            SqlCommand selectSondage =
               new SqlCommand("SELECT IdReponse, NomReponse, NombreVoteReponse FROM Reponse WHERE FKIdSondage = @fKIdSondage ", sondageEnCours);
            selectSondage.Parameters.AddWithValue("@fKIdSondage", idSondage);
           
            SqlDataReader dataReader = selectSondage.ExecuteReader();
            while (dataReader.Read())
            {
                int idReponse = (int)dataReader["IdReponse"];
                string nomReponse = (string)dataReader["NomReponse"];
                int nombreVoteReponse = (int)dataReader["NombreVoteReponse"];
               


                Reponse detailList = new Reponse(nomReponse, idSondage, nombreVoteReponse,idReponse);

                resultats.Add(detailList);
            }
            sondageEnCours.Close();

            return resultats;

        }
       

    }
}
