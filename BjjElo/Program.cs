using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BjjElo;
using DataImport;
using MySql.Data.MySqlClient;

namespace BjjElo
{
    class Program
    {
        static void Main(string[] args)
        {
            MatchProcessor.LoadAllData();
            var fighters = MatchProcessor.Fighters;
            var matches = MatchProcessor.Matches;

            foreach (var fighter in fighters)
                fighter.EloRanking = 2000;

            // Test to see how the initial ranking affects the final ranking. Hint: It has almost no influence ;)
            var almeida = fighters.Single(f => f.LastName.Equals("Almeida") && f.FirstName.Equals("Marcus"));
            almeida.EloRanking = 2000;

            var kFactor = 32;
            var difference = 400;

            foreach (var match in matches.OrderBy(m => m.Year))
            {
                var fighter1 = match.Fighter1;
                var fighter2 = match.Fighter2;

                var expectedResult = 1 / (1 + Math.Pow(10, (double)(fighter2.EloRanking - fighter1.EloRanking) / difference));

                var eloDifference = kFactor * ((int)match.Result  / 2.0 - expectedResult);

                fighter1.EloRanking += eloDifference;
                fighter2.EloRanking -= eloDifference;
            }

            Console.WriteLine($"Anzahl Kämpfe: {matches.Count}");

            foreach (var fighter in fighters.OrderByDescending(f => f.EloRanking))
                Console.WriteLine(fighter + " Anzahl Kämpfe: " + matches.Count(m => m.Fighter1 == fighter || m.Fighter2 == fighter).ToString());

            Console.ReadLine();
        }
    }
}
