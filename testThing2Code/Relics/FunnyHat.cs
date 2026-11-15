using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.RelicPools;
using testThing2.testThing2Code.Powers;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]
public class FunnyHat() : testThing2Relic
{

    public override RelicRarity Rarity =>
        RelicRarity.Ancient;
    

    public override List<(string, string)> Localization => new PowerLoc(
        "Funny Hat",
        "Each turn, play the [yellow]cheapest[/yellow] card in [yellow]someone[/yellow]'s hand.",
        "Each turn, play the [yellow]cheapest[/yellow] card in [yellow]someone[/yellow]'s hand.");

    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        bool succedded = false;
        while (!succedded)
        {
            int r = Owner.RunState.Rng.Niche.NextInt(0, participants.Count);
            if (participants[r].IsPlayer)
            {
                succedded = true;
                await PowerCmd.Apply<ChaosPower>((PlayerChoiceContext) new ThrowingPlayerChoiceContext(), participants[r], 1, Owner.Creature, null);
            }
        }
    }
}