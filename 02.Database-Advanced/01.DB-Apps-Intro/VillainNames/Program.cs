using System;
using System.Data.SqlClient;
using InitialSetup;

namespace VillainNames
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();
                
                string vilainInfo = "SELECT V.Name, COUNT(MV.MinionId) AS MinionsCount FROM master.dbo.Villains AS V JOIN MinionsVillains AS MV on V.Id = MV.VillainId GROUP BY Name HAVING COUNT(MV.MinionId) >= 3 ORDER BY MinionsCount DESC";

                using (SqlCommand command = new SqlCommand(vilainInfo, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0]} -> {reader[1]}");
                        }
                    }
                }
                
                connection.Close();
            }
        }
    }
}