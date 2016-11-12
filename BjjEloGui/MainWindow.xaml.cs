using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BaseClasses;
using DataImport;

namespace BjjEloGui
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HashSet<Fighter> fighters;
        private HashSet<MatchWithoutId> matches;

        public MainWindow()
        {
            InitializeComponent();


            MatchProcessor.LoadAllData();
            this.fighters = MatchProcessor.Fighters;
            this.matches = MatchProcessor.Matches;


            // Set initial elo ratings for all fighters.
            foreach (var fighter in fighters)
                fighter.EloRating = 2000;


            //// Test to see how the initial ranking of a fighter affects his final ranking. Hint: It has almost no influence ;)
            //var almeida = fighters.Single(f => f.LastName.Equals("Almeida") && f.FirstName.Equals("Marcus"));
            //almeida.EloRating = 2000;



            // The matches are first sorted and grouped by year.
            // For each year, the elo rating difference for each fighter is calculated,
            // and added at the end of the year.
            foreach (var year in matches.GroupBy(m => m.Year).OrderBy(g => g.Key))
            {
                foreach (var match in year)
                {
                    // The points won or lost by fighter 1 in the match.
                    double pointsWonOrLost = CalculateEloPoints(match);

                    // The points are added to the score of fighter 1, and subtracted from the score of fighter 2.
                    match.Fighter1.EloRatingDifference += pointsWonOrLost;
                    match.Fighter2.EloRatingDifference -= pointsWonOrLost;
                }

                foreach (var fighter in fighters)
                    fighter.UpdateEloRating();
            }




            var datagridInfo =
                fighters
                .Select(f => new
                {
                    Fighter = f.FirstName + " " + f.LastName,
                    Rating = (int)f.EloRating,
                    Matches = matches.Count(m => m.Fighter1 == f || m.Fighter2 == f),
                    Victories = matches.Count(m => (m.Fighter1 == f && m.Result == MatchResult.WinBySubmission) || (m.Fighter2 == f && m.Result == MatchResult.LossBySubmission))
                })
                .OrderByDescending(f => f.Rating)
                .ToList();

            //dataGrid.ItemsSource = datagridInfo;

            dataGrid.ItemsSource = fighters;




            dataGridMatches.ItemsSource = MatchProcessor.RawMatchData;

        }





        /// <summary>
        /// Calculates the points won or lost by the fighters in the match.
        /// The result has to be added to the elo rating of fighter 1, and subtracted from the elo rating of fighter 2.
        /// </summary>
        private static double CalculateEloPoints(MatchWithoutId match)
        {
            // The expected result of the match. Between 0 (loss by submission) and 1 (win by submission).
            var expectedResult = 1 / (1 + Math.Pow(10, (double)(match.Fighter2.EloRating - match.Fighter1.EloRating) / Constants.EloDifference));

            // The actual result of the match.
            double actualResult;
            switch (match.Result)
            {
                case (MatchResult.WinBySubmission):
                    actualResult = 1.0;
                    break;

                case (MatchResult.WinByPoints):
                    actualResult = Constants.WinByPointsFactor;
                    break;

                case (MatchResult.Draw):
                    actualResult = 0.5;
                    break;

                case (MatchResult.LossByPoints):
                    actualResult = 1 - Constants.WinByPointsFactor;
                    break;

                case (MatchResult.LossBySubmission):
                    actualResult = 0;
                    break;

                default:
                    throw new ArgumentException("Invalid match result detected!");
            }

            // The points won or lost by the fighters.
            var eloDifference = Constants.EloFactor * (actualResult - expectedResult);
            return eloDifference;
        }




        private void dataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var fighter = dataGrid.SelectedItem as Fighter;

            var matchesOfSelectedFighter1 =
                matches
                .Where(m => m.Fighter1 == fighter)
                .Select(m => new { Opponent = m.Fighter2, Rating = (int)m.Fighter2.EloRating, Result = m.Result, Year = m.Year });

            var matchesOfSelectedFighter2 =
           matches
           .Where(m => m.Fighter2 == fighter)
           .Select(m => new { Opponent = m.Fighter1, Rating = (int)m.Fighter1.EloRating, Result = MatchWithoutId.InvertMatchResult(m.Result), Year = m.Year });


            dataGrid1.ItemsSource = matchesOfSelectedFighter1.Concat(matchesOfSelectedFighter2).ToList();

        }
    }
}
