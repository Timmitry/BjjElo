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

    //public Match(int matchId, Fighter fighterId1, int fighterId2, int result)
    //{
    //  this.MatchId = matchId;
    //  this.FighterId1 = fighterId1;
    //  this.FighterId2 = fighterId2;
    //  this.Result = result;
    //}
  }
}
