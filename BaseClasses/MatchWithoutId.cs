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
        public Result Result { get; set; }
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
                if (this.Result == Result.Draw && match.Result != Result.Draw)
                    return false;

                if ((this.Result == Result.Win && match.Result != Result.Loss) || (this.Result == Result.Loss && match.Result != Result.Win))
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

        public static Result InvertResult(Result result)
        {
            if (result == Result.Win)
                return Result.Loss;
            if (result == Result.Loss)
                return Result.Win;

            return Result.Draw;
        }
    }
}
