using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BaseClasses;

namespace DataImport
{
    public class MatchInformation
    {
        public string Fighter1 { get; set; } = null;
        public string Fighter2 { get; set; } = null;
        public string Result { get; set; } = null;
        public string Year { get; set; } = null;
        public string Round { get; set; } = null;
        public string Competition { get; set; } = null;
        public string Method { get; set; } = null;
        public string WeightClass { get; set; } = null;
    }



    public static class MatchProcessor
    {
        public static HashSet<Fighter> Fighters { get; } = new HashSet<Fighter>();
        public static HashSet<MatchWithoutId> Matches { get; } = new HashSet<MatchWithoutId>();



        public static void LoadAllData()
        {
            var filepath = "..\\..\\..\\Data\\";
            var bjjHeroes = new List<string>()
            {
                "Roger Gracie",
                "Alexandre Ribeiro",
                "Felipe Pena",
                "Rodolfo Vieira",
                "Marcus Almeida",
                "Leonardo Nogueira",
                "Dimitrius Souza",
                "Braga Neto",
                "Rodrigo Comprido",
                "Saulo Ribeiro"
            };

            foreach(var hero in bjjHeroes)
            {
                var filename = filepath + Regex.Replace(hero, @"\s+", "") + ".csv";
                ProcessMatchInformations(BjjHeroesBio.LoadFile(filename, hero));
            }

            ProcessMatchInformations(Worlds2016.LoadFile());
        }




        public static void ProcessMatchInformations(IList<MatchInformation> matchInformations)
        {
            foreach (var matchInfo in matchInformations)
            {
                var fighter1 = GetOrCreateFighter(matchInfo.Fighter1);
                var fighter2 = GetOrCreateFighter(matchInfo.Fighter2);

                if (fighter1 == null || fighter2 == null)
                    continue;

                Result result;
                switch (matchInfo.Result)
                {
                    case ("W"):
                        result = Result.Win;
                        break;
                    case ("L"):
                        result = Result.Loss;
                        break;
                    case ("D"):
                        result = Result.Draw;
                        break;
                    default:
                        throw new ArgumentException("Result could not be analyzed!");
                }

                int year;
                if (!Int32.TryParse(matchInfo.Year, out year))
                    year = 0;

                var match = new MatchWithoutId()
                {
                    Fighter1 = fighter1,
                    Fighter2 = fighter2,
                    Result = result,
                    Year = year
                };


                Matches.Add(match);
            }
        }



        public static Fighter GetOrCreateFighter(string fullName)
        {
            if (fullName.Equals("Unknown"))
                return null;

            var index = fullName.LastIndexOf(' ');
            var firstname = fullName.Substring(0, index);
            var lastname = fullName.Substring(index + 1);

            var fighter
                = Fighters
                .SingleOrDefault(f => f.LastName.Equals(lastname) && (f.FirstName.Contains(firstname) || firstname.Contains(f.FirstName)));

            if (fighter == null)
            {
                fighter = new Fighter(null, firstname, lastname, null);
                Fighters.Add(fighter);
            }

            return fighter;
        }
    }
}
