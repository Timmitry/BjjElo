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
        public double? EloRating { get; set; }

        /// <summary>
        /// Elo points that the fighter has won or lost, but who are not yet added to his rating.
        /// </summary>
        /// <remarks>
        /// If a fighter has multiple fights in one tournament,
        /// his elo ranking is only updated once after the tournament,
        /// and not after every single match.
        /// </remarks>
        public double EloRatingDifference { get; set; } = 0;

        public string FullName => FirstName + " " + LastName;

        public Fighter(int? fighterId, string firstName, string lastName, double? eloRating)
        {
            this.FighterId = fighterId;
            this.LastName = lastName;
            this.FirstName = firstName;
            this.EloRating = eloRating;
        }

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName}, Rating: {this.EloRating:N0}";
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

        public void UpdateEloRating()
        {
            this.EloRating += this.EloRatingDifference;
            this.EloRatingDifference = 0;
        }

        public static bool operator !=(Fighter fighter1, Fighter fighter2)
        {
            return !(fighter1 == fighter2);
        }
    }
}
