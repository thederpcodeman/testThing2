using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Runs;
using testThing2.testThing2Code.Relics;
using Array = Godot.Collections.Array;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]
public class FlyingCarpetStrangler() : testThing2Relic
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;
    
    public override bool HasUponPickupEffect => true;

    public override List<(string, string)> Localization => new PowerLoc(
        "Flying Carpet",
        "Card Rewards can now contain Curses",
        "Card Rewards can now contain Curses");
    
    
    public override CardCreationOptions ModifyCardRewardCreationOptions(
        Player player,
        CardCreationOptions options)
    {
        // ISSUE: object of a compiler-generated type is created
        return this.Owner != player || options.Flags.HasFlag((Enum) CardCreationFlags.NoCardPoolModifications) || !options.Flags.HasFlag((Enum) CardCreationFlags.IsCardReward) ? options : options.WithCardPools(options.CardPools.Union<CardPoolModel>((IEnumerable<CardPoolModel>) new List<CardPoolModel>([(CardPoolModel) ModelDb.CardPool<CurseCardPool>()])));
    }
    
}