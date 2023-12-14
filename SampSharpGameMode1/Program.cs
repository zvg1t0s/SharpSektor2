
using SampSharp.Core;
using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text;

namespace SampSharpGameMode1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=;database=Samp");

            connection.Open();
            
            
          
            
            Console.WriteLine(connection.State);
            new GameModeBuilder()
                .Use<GameMode>()
                .UseEncoding(encoding: Encoding.GetEncoding(1251))
                .Run();
            
        }

    }
}
