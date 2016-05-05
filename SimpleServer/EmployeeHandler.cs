using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SimpleServer
{
    public class EmployeeHandler
    {
        public EmployeeHandler()
        {
        }

        public string getEmployee(string IdEmployee)
        {
            string response = "";

            string query = @"
				SELECT  
                    e.IdEmployee,
                    e.FName,
                    e.LName,
                    e.JobTitle
				FROM Employee e
				WHERE e.IdEmployee =
				" + IdEmployee;

            const string connectionString =
                "Data Source =(LocalDB)\\MSSQLLocalDB; AttachDbFilename=\"C:\\Users\\shu\\AppData\\Local\\Microsoft\\Microsoft SQL Server Local DB\\Instances\\MSSQLLocalDB\\Zoolandia.mdf\"; Integrated Security = True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Check if the reader has any rows at all before starting to read.
                    if (reader.HasRows)
                    {
                        // Read advances to the next row.
                        while (reader.Read())
                        {
                            response += "<div class=\"habitat habitat-id-" + reader[0] + "\">";
                            response += "<h2>" + reader[1] + " " + reader[2] + "</h2>";
                            response += "<div>" + reader[3] + "</div>";
                            response += "</div>";
                        }
                        Console.WriteLine(response);
                    }
                }
                connection.Close();
                return response;
            }
        }


        public string getAllEmployees()
        {
            string response = "";

            const string query = @"
				SELECT  
                  e.IdEmployee,
                  e.FName,
                  e.LName,
                  e.JobTitle
				FROM Employee e
				";

            const string connectionString =
                "Data Source =(LocalDB)\\MSSQLLocalDB; AttachDbFilename=\"C:\\Users\\shu\\AppData\\Local\\Microsoft\\Microsoft SQL Server Local DB\\Instances\\MSSQLLocalDB\\Zoolandia.mdf\"; Integrated Security = True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Check if the reader has any rows at all before starting to read.
                    if (reader.HasRows)
                    {
                        // Read advances to the next row.
                        while (reader.Read())
                        {
                            response += "<div class=\"employee employee-id-" + reader[0] + "\">";
                            response += "<h2>" + reader[1] + " " + reader[2] + "</h2>";
                            response += "<div>" + reader[3] + "</div>";
                            response += "</div>";
                        }
                        Console.WriteLine(response);
                    }
                }
                connection.Close();
                return response;
            }
        }
    }
}