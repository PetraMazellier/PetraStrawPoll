using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

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
                    new SqlCommand("INSERT INTO Sondage (NomSondage, MultiSondage, EtatSondage, NumSecurite, NombreVoteSondage) " +
                    " VALUES (@nomSondage, @multiSondage, @etatSondage, @numSecurite, @nombreVoteSondage )", connection);
            InsertSondage.Parameters.AddWithValue("@nomSondage", creationSondage.NomSondage);
            InsertSondage.Parameters.AddWithValue("@multiSondage", creationSondage.MultiSondage);
            InsertSondage.Parameters.AddWithValue("@etatSondage", creationSondage.EtatSondage);
            InsertSondage.Parameters.AddWithValue("@numSecurite", creationSondage.NumSecurite);
            InsertSondage.Parameters.AddWithValue("@nombreVoteSondage", creationSondage.NombreVoteSondage);

            InsertSondage.ExecuteNonQuery();

            connection.Close();
        }
        public static bool RecupererSondage(int idSondage, out Sondage detailSondage)
        {

            SqlConnection SelectSondage = new SqlConnection(SqlConnectionString);
            SelectSondage.Open();
            SqlCommand selectLecteur =
                new SqlCommand("SELECT NomSondage, MultiSondage, EtatSondage, NumSecurite, NombreVoteSondage FROM Sondage where IdSondage = @idSondage", SelectSondage);
            selectLecteur.Parameters.AddWithValue("@idSondage", idSondage);
            SqlDataReader dataReader = selectLecteur.ExecuteReader();

            if (dataReader.Read())
            {
                string nomSondage = (string)dataReader["NomSondage"];
                bool multiSondage = (bool)dataReader["MultiSondage"];
                bool etatSondage = (bool)dataReader["EtatSondage"];
                int numSecurite = (int)dataReader["NumSecurite"];
                int nombreVoteSondage = (int)dataReader["NombreVoteSondage"];
                detailSondage = new Sondage(nomSondage, multiSondage, numSecurite, etatSondage, nombreVoteSondage);
                return true;
            }
            else
            {
                detailSondage = null;
                return false;
            }
        }
        public static int AjoutNombreVoteSondage(Sondage ajoutNombreSondage)
        {
            try
            {
                SqlConnection connection = new SqlConnection(SqlConnectionString);
                connection.Open();
                SqlCommand updateCommand = connection.CreateCommand();
                updateCommand.CommandText = String.Format(
                    "UPDATE Sondage " +
                    "SET NombreVoteSondage =  @nombreVoteSondage " +
                    "WHERE IdSondage =  @idSondage");

                updateCommand.Parameters.AddWithValue("@idSondage", ajoutNombreSondage.IdSondage);
                updateCommand.Parameters.AddWithValue("@nombreVoteSondage", ajoutNombreSondage.NombreVoteSondage);

                int nombreSondageModifiees = updateCommand.ExecuteNonQuery();
                return nombreSondageModifiees;
            }

            catch
            {
                throw new Exception("Problème base de donnée table Sondage !!!");
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
        public static void CreationReponse(Sondage sondageCorrespondant, Reponse creationReponse)
        {
            SqlConnection connection = new SqlConnection(SqlConnectionString);
            connection.Open();
            SqlCommand InsertSondage =
                    new SqlCommand("INSERT INTO Reponse (NomReponse, NombreVoteReponse, FKIdSondage) " +
                    " VALUES (@nomReponse, @nombreVoteReponse, @fKIdSondage )", connection);
            InsertSondage.Parameters.AddWithValue("@nomReponse", creationReponse.NomReponse);           
            InsertSondage.Parameters.AddWithValue("@nombreVoteSondage", creationReponse.NombreVoteReponse);
            InsertSondage.Parameters.AddWithValue("@fKIdSondage", sondageCorrespondant.IdSondage);

            InsertSondage.ExecuteNonQuery();

            connection.Close();
        }
        public static int AjoutNombreVoteReponse(Sondage sondageCorrespondant, Reponse ajoutNombreSondage)
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
                updateCommand.Parameters.AddWithValue("@fKIdSondage", sondageCorrespondant.IdSondage);
                updateCommand.Parameters.AddWithValue("@nombreVoteSondage", ajoutNombreSondage.NombreVoteReponse);

                int nombreSondageModifiees = updateCommand.ExecuteNonQuery();
                return nombreSondageModifiees;
            }

            catch
            {
                throw new Exception("Problème base de donnée table Sondage !!!");
            }

        }
    }
}
