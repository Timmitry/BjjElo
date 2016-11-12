using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClasses
{
    public class MatchWithoutId
    {
        public int? MatchId { get; set; }
        public Fighter Fighter1 { get; set; }
        public Fighter Fighter2 { get; set; }
        public MatchResult Result { get; set; }
        public int Year { get; set; }


        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var match = obj as MatchWithoutId;
            if (match == null)
                return false;

            if (this.Year != match.Year)
                return false;

            if (this.Fighter1.Equals(match.Fighter1) && this.Fighter2.Equals(match.Fighter2))
            {
                if (this.Result != match.Result)
                    return false;
            }
            else if (this.Fighter1.Equals(match.Fighter2) && this.Fighter2.Equals(match.Fighter1))
            {
                if (this.Result == MatchResult.Draw && match.Result != MatchResult.Draw)
                    return false;

                if ((this.Result == MatchResult.WinBySubmission && match.Result != MatchResult.LossBySubmission) || (this.Result == MatchResult.LossBySubmission && match.Result != MatchResult.WinBySubmission))
                    return false;
            }
            else
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return 31 + 47 * this.Fighter1.GetHashCode() + 47 * this.Fighter2.GetHashCode() + 13 * this.Year.GetHashCode() + 17 * this.Result.GetHashCode();
        }

        public static bool operator ==(MatchWithoutId match1, MatchWithoutId match2)
        {
            if (ReferenceEquals(match1, null) && ReferenceEquals(match2, null))
                return true;

            if (ReferenceEquals(match1, null))
                return false;

            return match1.Equals(match2);
        }

        public static bool operator !=(MatchWithoutId match1, MatchWithoutId match2)
        {
            return !(match1 == match2);
        }



        /// <summary>
        /// Inverts the result of a match, so that win becomes a loss and vice versa.
        /// </summary>
        /// <param name="result">The result of the match.</param>
        /// <returns>The inverted result. A win becomes a loss, a loss a win, and a draw stays a draw.</returns>
        /// <remarks>
        /// Match results are always saved by the perspective of fighter 1 of the match.
        /// This function can be used to convert the result to the perspective of fighter 2.
        /// </remarks>
        public static MatchResult InvertMatchResult(MatchResult result)
        {
            switch (result)
            {
                case (MatchResult.WinBySubmission):
                    return MatchResult.LossBySubmission;

                case (MatchResult.WinByPoints):
                    return MatchResult.LossByPoints;

                case (MatchResult.Draw):
                    return MatchResult.Draw;

                case (MatchResult.LossByPoints):
                    return MatchResult.WinByPoints;

                case (MatchResult.LossBySubmission):
                    return MatchResult.WinBySubmission;

                default:
                    throw new ArgumentException("The input result is invalid!");
            }
        }
    }
}
