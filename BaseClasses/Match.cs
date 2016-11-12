using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClasses
{
  public class Match
  {
    public int? MatchId { get; }
    public int FighterId1 { get; }
    public int FighterId2 { get; }
    public MatchResult Result { get; }

    public Match(int matchId, int fighterId1, int fighterId2, MatchResult result)
    {
      this.MatchId = matchId;
      this.FighterId1 = fighterId1;
      this.FighterId2 = fighterId2;
      this.Result = result;
    }
  }
}
