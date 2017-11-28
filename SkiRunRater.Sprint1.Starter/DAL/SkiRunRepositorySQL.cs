using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace SkiRunRater
{
    public class SkiRunRepositorySQL : ISkiRunRepository
    {
        #region FIELDS
        private IEnumerable<SkiRun> _skiRuns = new List<SkiRun>();
        #endregion

        #region CONSTRUCTORS
        public SkiRunRepositorySQL()
        {
            _skiRuns = ReadAllSkiRuns();
        }
        #endregion

        #region METHODS
        public IEnumerable<SkiRun> ReadAllSkiRuns()
        {
            IList<SkiRun> skiRuns = new List<SkiRun>();

            string connString = GetConnectionString();
            string sqlCommandString = "SELECT * from SkiRuns";

            using (SqlConnection sqlConn = new SqlConnection(connString))
            using (SqlCommand sqlCommand = new SqlCommand(sqlCommandString, sqlConn))
            {
                try
                {
                    sqlConn.Open();
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                SkiRun skiRun = new SkiRun();
                                skiRun.ID = Convert.ToInt32(reader["Id"]);
                                skiRun.Name = reader["Name"].ToString();
                                skiRun.Vertical = Convert.ToInt32(reader["Vertical"]);
                                skiRuns.Add(skiRun);
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("SQL Exception: {0}", sqlEx.Message);
                    Console.WriteLine(sqlCommandString);
                }
            }
            return skiRuns;
        }

        public SkiRun SelectById(int id)
        {
            return _skiRuns.Where(sr => sr.ID == id).FirstOrDefault();
        }

        public List<SkiRun> SelectAll()
        {
            return _skiRuns as List<SkiRun>;
        }

        public void Insert(SkiRun skiRun)
        {
            string connString = GetConnectionString();

            var sb = new StringBuilder("INSERT INTO SkiRuns");
            sb.Append(" ([Id],[Name],[Vertical])");
            sb.Append(" Values (");
            sb.Append("'").Append(skiRun.ID).Append("',");
            sb.Append("'").Append(skiRun.Name).Append("',");
            sb.Append("'").Append(skiRun.Vertical).Append("')");
            string sqlCommandString = sb.ToString();

            using (SqlConnection sqlConn = new SqlConnection(connString))
            using (SqlDataAdapter sqlAdapter = new SqlDataAdapter())
            {
                try
                {
                    sqlConn.Open();
                    sqlAdapter.InsertCommand = new SqlCommand(sqlCommandString, sqlConn);
                    sqlAdapter.InsertCommand.ExecuteNonQuery();
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine($"SQL Exception: {sqlEx.Message}");
                    Console.WriteLine(sqlCommandString);
                }
            }
        }

        public void Delete(int id)
        {
            string connString = GetConnectionString();

            var sb = new StringBuilder("DELETE FROM SkiRuns");
            sb.Append(" WHERE ID = ").Append(id);
            string sqlCommandString = sb.ToString();

            using (SqlConnection sqlConn = new SqlConnection(connString))
            using (SqlDataAdapter sqlAdapter = new SqlDataAdapter())
            {
                try
                {
                    sqlConn.Open();
                    sqlAdapter.DeleteCommand = new SqlCommand(sqlCommandString, sqlConn);
                    sqlAdapter.DeleteCommand.ExecuteNonQuery();
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine($"SQL Exception: {sqlEx.Message}");
                    Console.WriteLine(sqlCommandString);
                }
            }
        }

        public void Update(SkiRun skiRun)
        {
            string connString = GetConnectionString();

            var sb = new StringBuilder("UPDATE SkiRuns SET ");
            sb.Append("Name = '").Append(skiRun.Name).Append("', ");
            sb.Append("Vertical = ").Append(skiRun.Vertical).Append(" ");
            sb.Append("WHERE ");
            sb.Append("Id = ").Append(skiRun.ID);
            string sqlCommandString = sb.ToString();

            using (SqlConnection sqlConn = new SqlConnection(connString))
            using (SqlDataAdapter sqlAdapter = new SqlDataAdapter())
            {
                try
                {
                    sqlConn.Open();
                    sqlAdapter.UpdateCommand = new SqlCommand(sqlCommandString, sqlConn);
                    sqlAdapter.UpdateCommand.ExecuteNonQuery();
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine($"SQL Exception: {sqlEx.Message}");
                    Console.WriteLine(sqlCommandString);
                }
            }

        }

        public IEnumerable<SkiRun> QueryByVertical(int minimumVertical, int maximumVertical)
        {
            return _skiRuns.Where(sr => sr.Vertical >= minimumVertical && sr.Vertical <= maximumVertical);
        }

        private static string GetConnectionString()
        {
            string returnValue = null;

            var settings = ConfigurationManager.ConnectionStrings["SkiRunRater_Local"];
            if (settings != null)
            {
                returnValue = settings.ConnectionString;
            }
            return returnValue;
        }

        public void Dispose()
        {
            _skiRuns = null;
        }
        #endregion
    }
}
