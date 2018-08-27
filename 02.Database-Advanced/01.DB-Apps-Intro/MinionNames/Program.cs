using System;
using System.Data.SqlClient;
using InitialSetup;


namespace MinionNames
{
    class Program
    {
        static void Main(string[] args)
        {
            int villainId = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string vilainName = GetVillainName(villainId, connection);
                
                if (vilainName == null)
                {
                    Console.WriteLine($"No villain with ID {villainId} exists in the database.");  
                }
                else
                {
                    Console.WriteLine($"{vilainName}");
                    PrintNames(villainId, connection);
                }

                connection.Close();
            }
        }

        private static void PrintNames(int villainId, SqlConnection connection)
        {
            string minionsSql =
                "SELECT Name, Age FROM master.dbo.Minions AS M2 JOIN MinionsVillains MV on M2.Id = MV.MinionId WHERE MV.VillainId = @Id";

            
            using (SqlCommand command = new SqlCommand(minionsSql, connection))
            {
                command.Parameters.AddWithValue("Id", villainId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("(no minians)");
                    }
                    else
                    {
                        int row = 1;
                        while (reader.Read())
                        {
                            Console.WriteLine($"{row++}. {reader[0]} {reader[1]}");    
                        }
                        
                    }
                }
            }
        }

        private static string GetVillainName(int villainId, SqlConnection connection)
        {
           string nameSql = "SELECT Name FROM master.dbo.Villains WHERE Id = @id";
            
            using (SqlCommand command = new SqlCommand(nameSql, connection))
            {
                command.Parameters.AddWithValue("@id", villainId);

                return (string) command.ExecuteScalar();
            }
        }
    }
}