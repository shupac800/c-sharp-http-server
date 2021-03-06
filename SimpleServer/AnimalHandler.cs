﻿using System;
using System.Data;
using System.Data.SqlClient;

namespace SimpleServer
{
	public class AnimalHandler
	{
		public AnimalHandler ()
		{
		}

		public string getAnimal(string IdAnimal) {
			string response = "";

			string query = @"
				SELECT 
				  a.IdAnimal,
				  a.Name, 
				  h.Name HabitatName,
				  ht.Name HabitatType,
				  s.CommonName,
				  s.ScientificName,
                  s.Wiki
				FROM Animal a
				INNER JOIN Species s ON a.IdSpecies = s.IdSpecies
				INNER JOIN Habitat h ON h.IdHabitat = a.IdHabitat
				INNER JOIN HabitatType ht on ht.IdType = h.IdType
				WHERE a.IdAnimal = 
				" + IdAnimal;

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
                            response += "<div class=\"animal animal-id-" + reader[0] + "\">";
                            response += "<h2>" + reader[1] + "</h2>";
                            response += "<div>" + reader[5] + "</div>";
                            response += "<div><p>commonly known as the <a href='" + reader[6] + "'>" + reader[4] + "</a></p><div>";
                            response += "<div>Lives in the " + reader[2] + " (" + reader[3] + " type) habitat</div>";
                            response += "</div>";
                        }
                        Console.WriteLine(response);
                    }
                }
                connection.Close();
                return response;
            }
		}


		public string getAllAnimals() {
			string response = "";

			const string query = @"
				SELECT 
				  a.IdAnimal,
				  a.Name, 
				  h.Name HabitatName,
				  ht.Name HabitatType,
				  s.CommonName,
				  s.ScientificName,
                  s.Wiki
				FROM Animal a
				INNER JOIN Species s ON a.IdSpecies = s.IdSpecies
				INNER JOIN Habitat h ON h.IdHabitat = a.IdHabitat
				INNER JOIN HabitatType ht on ht.IdType = h.IdType
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
                            response += "<div class=\"animal animal-id-" + reader[0] + "\">";
                            response += "<h2>" + reader[1] + "</h2>";
                            response += "<div><a href='/animals/" + reader[0] + "'>" + reader[5] + "</a></div>";
                            response += "<div><p>commonly known as the <a href='" + reader[6] + "'>" + reader[4] + "</a></p><div>";
                            response += "<div>Lives in the " + reader[2] + " (" + reader[3] + " type) habitat</div>";
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

