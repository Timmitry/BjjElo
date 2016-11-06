using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClasses
{
  public class Fighter
  {
    public int? FighterId { get; set; }
    public string LastName { get; }
    public string FirstName { get; }
    public double? EloRanking { get; set; }

    public Fighter(int? fighterId, string firstName, string lastName, double? eloRanking)
    {
      this.FighterId = fighterId;
      this.LastName = lastName;
      this.FirstName = firstName;
      this.EloRanking = eloRanking;
    }

    public override string ToString()
    {
      return $"{this.FirstName} {this.LastName}, Rating: {this.EloRanking:N0}";
    }
  }
}
