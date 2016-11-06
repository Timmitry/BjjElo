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


        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var fighter = obj as Fighter;
            if (fighter == null)
                return false;

            return this.FirstName.Equals(fighter.FirstName) && this.LastName.Equals(fighter.LastName);
        }

        public override int GetHashCode()
        {
            return 31 + 17 * this.FirstName.GetHashCode() + 47 * this.LastName.GetHashCode();
        }

        public static bool operator ==(Fighter fighter1, Fighter fighter2)
        {
            if (ReferenceEquals(fighter1, null) && ReferenceEquals(fighter2, null))
                return true;

            if (ReferenceEquals(fighter1, null))
                return false;

            return fighter1.Equals(fighter2);
        }

        public static bool operator !=(Fighter fighter1, Fighter fighter2)
        {
            return !(fighter1 == fighter2);
        }
    }
}
