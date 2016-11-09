using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BaseClasses;

namespace BjjElo
{
    public partial class Form1 : Form
    {
        public Form1(HashSet<Fighter> fighters, HashSet<MatchWithoutId> matches)
        {
            InitializeComponent();


            var datagridInfo =
                fighters
                .Select(f => new
                {
                    Fighter = f.FirstName + " " + f.LastName,
                    Rating = (int)f.EloRanking,
                    Matches = matches.Count(m => m.Fighter1 == f || m.Fighter2 == f),
                    Victories = matches.Count(m => (m.Fighter1 == f && m.Result == Result.Win) || (m.Fighter2 == f && m.Result == Result.Loss))
                })
                .OrderByDescending(f => f.Rating)
                .ToList();


            this.dataGridView1.DataSource = datagridInfo;



        }
    }
}
