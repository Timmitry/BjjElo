using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseClasses;

namespace DataImport
{
    public static class Worlds2016
    {
        public static List<Fighter> Fighters { get; } = new List<Fighter>();
        public static List<MatchWithoutId> Matches { get; } = new List<MatchWithoutId>();


        public static IList<MatchInformation> LoadFile()
        {
            //var fighters = new List<Fighter>();
            //var matches = new List<Match>();

            var filename = "..\\..\\..\\Data\\Worlds2016.csv";

            var lines = File.ReadAllLines(filename);

            var matchInfos = new List<MatchInformation>();

            foreach (var line in lines.Skip(1))
            {
                var values = line.Split(',');


                var matchInfo = new MatchInformation()
                {
                    Fighter1 = values[0],
                    Fighter2 = values[1],
                    Result = "W",
                    Method = values[2],
                    WeightClass = values[3],
                    Round = values[4],
                    Competition = "World Championships",
                    Year = "2016",
                };

                matchInfos.Add(matchInfo);
            }

            //MatchProcessor.ProcessMatches(matchInfos);
            //Fighters.AddRange(MatchProcessor.Fighters);
            //Matches.AddRange(MatchProcessor.Matches);

            return matchInfos;
        }
    }
}
