using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Runs;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]
public class BlackestOmen() : testThing2Relic
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;
    
    public override List<(string, string)> Localization => new PowerLoc(
        "Blackest Omen",
        "At the start of combat, gain 6 countdown and 1 neurosurge, all other players gain 2 countdown and 1 neurosurge.",
        "At the start of combat, gain 6 countdown and 1 neurosurge, all other players gain 2 countdown and 1 neurosurge.");

    public override bool IsAllowed(IRunState runState)
    {
        bool necro = false;
        foreach (var player in runState.Players)
        {
            necro = necro || player.Character is Necrobinder;
        }

        return necro;
    }

    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (!participants.Contains<Creature>(Owner.Creature) || Owner.PlayerCombatState == null ||
            Owner.PlayerCombatState.TurnNumber > 1)
        {
            return;
        }

        foreach (var creature in participants)
        {
            if (creature.IsPlayer)
            {
                await PowerCmd.Apply<CountdownPower>(new ThrowingPlayerChoiceContext(), creature, 2M, Owner.Creature, null, false);
                await PowerCmd.Apply<NeurosurgePower>(new ThrowingPlayerChoiceContext(), creature, 1M, Owner.Creature, null, false);
            }
        }
        await PowerCmd.Apply<CountdownPower>(new ThrowingPlayerChoiceContext(), Owner.Creature, 4M, Owner.Creature, null, true);
    }
}