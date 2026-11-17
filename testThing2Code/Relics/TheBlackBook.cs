using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models.RelicPools;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]
public class TheBlackBook() : testThing2Relic
{

    public override RelicRarity Rarity =>
        RelicRarity.Ancient;
    

    public override List<(string, string)> Localization => new PowerLoc(
        "The Black Book",
        "[red]Invoke[/red] the names.",
        "[red]Invoke[/red] the names.");

    
}