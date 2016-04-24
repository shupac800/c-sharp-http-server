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
				  s.CommonName, 
				  h.Name as Habitat
				FROM Animal a
				LEFT JOIN Species s ON a.IdSpecies = s.IdSpecies
				LEFT JOIN Habitat h ON a.IdHabitat = h.IdHabitat
				";

      using (SqlConnection connection = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\Steve\\Documents\\Visual Studio 2015\\Projects\\c-sharp-http-server\\SimpleServer\\Zoolandia.mdf\"; Integrated Security = True"))
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
              response += "<div style=\"margin:10px 0\" class=\"animal animal-type-" + reader[0] + "\">";
              response += reader[1] + " the " + reader[2];
              response += " lives in the " + reader[3] + " habitat";
              response += "</div>";
            }
          }
        }
      }

			return response;
		}
	}
}

