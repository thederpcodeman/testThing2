using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.RelicPools;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]
public class OcdRelic() : testThing2Relic
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;
    
    public override List<(string, string)> Localization => new PowerLoc(
        "OCD",
        "Shuffle your discard pile into your deck each turn.",
        "Shuffle your discard pile into your deck each turn.");

    public override Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        return CardPileCmd.Shuffle(choiceContext, Owner);
    }
}