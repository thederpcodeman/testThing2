using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Runs;
using testThing2.testThing2Code.Powers;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]
public class VillainyRelicRegent() : testThing2Relic
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;
    
    public override List<(string, string)> Localization => new PowerLoc(
        "Villainy",
        "#At the start of each turn, Gain [E] and draw [blue]1[/blue] additional card.\n Whenever you play a card, add a [purple]dazed[/purple] to [orange]The Regent[/orange]'s [gold]Draw Pile[/gold].",
        "#At the start of each turn, Gain [E] and draw [blue]1[/blue] additional card.\n Whenever you play a card, add a [purple]dazed[/purple] to [orange]The Regent[/orange]'s [gold]Draw Pile[/gold].");

    public override bool IsAllowed(IRunState runState)
    {
        if (runState.Players.Count <= 1)
        {
            return false;
        }
        bool necro = false;
        foreach (var player in runState.Players)
        {
            necro = necro || player.Character is Regent;
        }

        return necro;
    }

    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (!participants.Contains<Creature>(Owner.Creature) || Owner.PlayerCombatState == null)
        {
            return;
        }
        
        await PlayerCmd.GainEnergy(1M, Owner);
        if (Owner.PlayerCombatState.TurnNumber == 1)
        {
            foreach (var creature in participants)
            {
                if (creature.Player != null && creature.Player.Character is Ironclad)
                {
                    await PowerCmd.Apply<VillainyPower>(new ThrowingPlayerChoiceContext(), creature, 1M, Owner.Creature,
                        null, false);
                }
            }
        }
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            return (IEnumerable<DynamicVar>) new List<DynamicVar>([(DynamicVar) new CardsVar(1)]);
        }
    }
    
    public override Decimal ModifyHandDraw(Player player, Decimal count)
    {
        return player != this.Owner ? count : count + (Decimal) this.DynamicVars.Cards.IntValue;
    }
}
