using System;
using System.Data.SqlClient;
using InitialSetup;

namespace RemoveVillain
{
    class Program
    {
        static void Main(string[] args)
        {
            int inputVillainId = int.Parse(Console.ReadLine());
            
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                int villainId = GetVillain(inputVillainId, connection);

                if (villainId == 0)
                {
                    Console.WriteLine($"No such villain was found");
                }
                else
                {
                    int affectedRows = ReleaseMinions(villainId, connection);


                    string villainName = GetVillainName(villainId, connection);
                    DeleteVillain(villainId, connection);

                    Console.WriteLine($"{villainName} was deleted.");
                    Console.WriteLine($"{affectedRows} minions were released.");
                }
                
                connection.Close();
            }
        }

        private static void DeleteVillain(int villainId, SqlConnection connection)
        {
            string deleteVillain = "DELETE FROM Villains WHere Id =@Id";
            
            using (SqlCommand command = new SqlCommand(deleteVillain, connection))
            {
                command.Parameters.AddWithValue("@Id", villainId);

                command.ExecuteNonQuery();
            }
        }

        private static string GetVillainName(int villainId, SqlConnection connection)
        {
            string nameSql = "SELECT Name FROM Villains WHERE Id = @Id";

            using (SqlCommand command = new SqlCommand(nameSql, connection))
            {
                command.Parameters.AddWithValue("@Id", villainId);

                return (string) command.ExecuteScalar();
            }
        }

        private static int ReleaseMinions(int villainId, SqlConnection connection)
        {
            string releaseMinions = "DELETE FROM MinionsVillains WHERE VillainId = @villainId";

            using (SqlCommand command = new SqlCommand(releaseMinions, connection))
            {
                command.Parameters.AddWithValue("@villainId", villainId);

                return (int) command.ExecuteNonQuery();
            }
        }

        private static int GetVillain(int inputVillainId, SqlConnection connection)
        {
            string villainInfo = "SELECT Id FROM Villains WHERE Id = @Id";
            
            using (SqlCommand command = new SqlCommand(villainInfo, connection))
            {
                command.Parameters.AddWithValue("@Id", inputVillainId);

                if (command.ExecuteScalar() == null)
                {
                    return 0;
                }
                return (int) command.ExecuteScalar();
            }
        }
    }
}