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
    public class BjjHeroesBio
    {
        public static IList<MatchInformation> LoadFile(string filename, string nameFighter1)
        {
            var lines = File.ReadAllLines(filename);

            var matchInfos = new List<MatchInformation>();

            foreach (var line in lines.Skip(1))
            {
                var values = line.Split(';');

                if (values.Length < 7)
                    continue;

                var matchInfo = new MatchInformation()
                {
                    Fighter1 = nameFighter1,
                    Fighter2 = values[0],
                    Result = values[1],
                    Method = values[2],
                    Competition = values[3],
                    WeightClass = values[4],
                    Round = values[5],
                    Year = values[6],
                };

                matchInfos.Add(matchInfo);
            }

            return matchInfos;
        }
    }
}
