using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Entities.RestSite;
using MegaCrit.Sts2.Core.Models.RelicPools;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]

public class InvokedBlinky() : testThing2Relic
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;
    
    public override List<(string, string)> Localization => new PowerLoc(
        "Invocation",
        "Invocation of [purple]Bliklotep[/purple].",
        "Invocation of [purple]Bliklotep[/purple].");

    public override bool TryModifyRestSiteOptions(Player player, ICollection<RestSiteOption> options)
    {
        if (player != this.Owner)
            return false;
        options.Add((RestSiteOption) new DigRestSiteOption(player));
        return true;
    }
    
}