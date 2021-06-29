using System.Collections.Generic;

namespace PhantomAbyssServer.Models
{
    public class RelicConversionRewards
    {
        public List<RelicConversionReward> Rewards { get; set; }
    }

    public class RelicConversionReward
    {
        public int Essence { get; set; }
        public List<int> DungeonKeys { get; set; }
    }

    public class GlobalValues
    {
        public RelicConversionRewards RelicConversionRewards { get; set; }
    }
}