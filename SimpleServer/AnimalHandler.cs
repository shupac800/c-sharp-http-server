using System;
using System.Data.SqlClient;

namespace SimpleServer
{
	public class AnimalHandler
	{
		public AnimalHandler ()
		{
		}

		public string getAllAnimals() {
			string response = "";

			string query = @"
				SELECT 
				  a.IdAnimal, 
				  a.Name, 
				  s.Name, 
				  h.Name
				FROM Animals a
				LEFT JOIN Species s ON a.IdSpecies = s.IdSpecies
				LEFT JOIN Habitats h ON h.IdHabitat = s.IdHabitat
				";
			
			using (SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Steve\\documents\\visual studio 2015\\Projects\\Invoices\\Invoices\\Invoices.mdf\";Integrated Security=True"))
			using (SqlCommand cmd = new SqlCommand(query, connection))
			{
				connection.Open();
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					// Check is the reader has any rows at all before starting to read.
					if (reader.HasRows)
					{
						
						// Read advances to the next row.
						while (reader.Read())
						{
							response += "<div class=\"animal animal-type-"+reader[0]+"\">";
							response += "<h2>" + reader[1] + "</h2>";
							response += "<div>"+ reader[2] +"</div>";
							response += "<div>Lives in the "+ reader[3] +" habitat</div>";
							response += "</div>";
						}
						Console.WriteLine(response);
					}
				}
			}

			return response;
		}
	}
}

