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

            foreach (var fighter in fighters)
                fighter.EloRating = 2000;

            // Test to see how the initial ranking affects the final ranking. Hint: It has almost no influence ;)
            var almeida = fighters.Single(f => f.LastName.Equals("Almeida") && f.FirstName.Equals("Marcus"));
            almeida.EloRating = 2000;

            var kFactor = 32;
            var difference = 400;

            foreach (var match in matches.OrderBy(m => m.Year))
            {
                var fighter1 = match.Fighter1;
                var fighter2 = match.Fighter2;

                var expectedResult = 1 / (1 + Math.Pow(10, (double)(fighter2.EloRating - fighter1.EloRating) / difference));

                var eloDifference = kFactor * ((int)match.Result / 2.0 - expectedResult);

                fighter1.EloRating += eloDifference;
                fighter2.EloRating -= eloDifference;
            }


            var datagridInfo =
                fighters
                .Select(f => new
                {
                    Fighter = f.FirstName + " " + f.LastName,
                    Rating = (int)f.EloRating,
                    Matches = matches.Count(m => m.Fighter1 == f || m.Fighter2 == f),
                    Victories = matches.Count(m => (m.Fighter1 == f && m.Result == Result.Win) || (m.Fighter2 == f && m.Result == Result.Loss))
                })
                .OrderByDescending(f => f.Rating)
                .ToList();

            //dataGrid.ItemsSource = datagridInfo;

            dataGrid.ItemsSource = fighters;
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
           .Select(m => new { Opponent = m.Fighter1, Rating = (int)m.Fighter1.EloRating, Result = MatchWithoutId.InvertResult(m.Result), Year = m.Year });


            dataGrid1.ItemsSource = matchesOfSelectedFighter1.Concat(matchesOfSelectedFighter2).ToList();

        }
    }
}
