using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Entities.RestSite;
using MegaCrit.Sts2.Core.Models.RelicPools;
using testThing2.testThing2Code.Extensions;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;
[Pool(typeof(EventRelicPool))]

public class InvokedBlinky() : testThing2Relic
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;
    
    public override List<(string, string)> Localization => new PowerLoc(
        "Invocation",
        "[purple]You can Observe at rest sites, giving you 2 card rewards with 5 options each.[/purple]",
        "[purple]You can Observe at rest sites, giving you 2 card rewards with 5 options each.[/purple]");
    


    public override bool TryModifyRestSiteOptions(Player player, ICollection<RestSiteOption> options)
    {
        if (player != this.Owner)
            return false;
        options.Add((RestSiteOption) new ObserveBlinkyRestSiteOption(player));
        return true;
    }
    
}