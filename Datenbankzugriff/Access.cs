using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseClasses;
using MySql.Data.MySqlClient;

namespace BjjElo
{
  public static class Access
  {
    private static string serverName = "localhost";
    private static string databaseName = "bjjelo";
    private static string userName = "root";
    private static string password = "password";

    private static string ConnectionString { get; set; }

    static Access()
    {
      ConnectionString = $"Server={serverName};Database={databaseName};Uid={userName};Pwd={password};";
    }

    public static IList<Fighter> GetAllFighters()
    {
      var fighters = new List<Fighter>();

      string query = "SELECT id_fighter, first_name, last_name, elo_ranking FROM fighters";

      using (MySqlConnection connection = new MySqlConnection(ConnectionString))
      {
        connection.Open();

        MySqlCommand command = new MySqlCommand(query, connection);
        using (MySqlDataReader reader = command.ExecuteReader())
        {
          while (reader.Read())
            fighters.Add(new Fighter(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDouble(3)));
        }
      }

      return fighters;
    }


    public static IList<Match> GetAllMatches()
    {
      var matches = new List<Match>();

      using (MySqlConnection connection = new MySqlConnection(ConnectionString))
      {
        connection.Open();

        string query = "SELECT id_match, id_fighter1, id_fighter2, result FROM matches";

        MySqlCommand command = new MySqlCommand(query, connection);

        using (MySqlDataReader reader = command.ExecuteReader())
        {
          while (reader.Read())
            matches.Add(new Match(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3)));
        }

      }

      return matches;
    }



  }
}
