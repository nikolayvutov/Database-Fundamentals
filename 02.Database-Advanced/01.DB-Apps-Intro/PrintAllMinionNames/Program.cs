using System;
using System.Collections.Generic;
using InitialSetup;
using System.Data.SqlClient;

class Program
{
    static void Main(string[] args)
    {
        List<string> minionsInitial = new List<string>();
        List<string> minionsArranged = new List<string>();

        using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT Name FROM Minions", connection);

            SqlDataReader reader = command.ExecuteReader();

            using (reader)
            {
                if (!reader.HasRows)
                {
                    reader.Close();
                    connection.Close();
                    return;
                }

                while (reader.Read())
                {
                    minionsInitial.Add((string)reader["Name"]);
                }
            }
        }

        while(minionsInitial.Count > 0)
        {
            minionsArranged.Add(minionsInitial[0]);
            minionsInitial.RemoveAt(0);

            if(minionsInitial.Count > 0)
            {
                minionsArranged.Add(minionsInitial[minionsInitial.Count - 1]);
                minionsInitial.RemoveAt(minionsInitial.Count - 1);
            }
        }

        minionsArranged.ForEach(m => Console.WriteLine(m));
    }
}