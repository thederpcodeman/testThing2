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
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Runs;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]
public class CursedBlade() : testThing2Relic
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;
    
    public override List<(string, string)> Localization => new PowerLoc(
        "Cursed Blade",
        "At the start of your turn, [gold]Exhaust[/gold] a [purple]random[/purple] card of a [purple]random[/purple] player's [gold]Draw Pile[/gold] and gain [blue]1[/blue] [gold]Strength[/gold].",
        "At the start of your turn, [gold]Exhaust[/gold] a [purple]random[/purple] card of a [purple]random[/purple] player's [gold]Draw Pile[/gold] and gain [blue]1[/blue] [gold]Strength[/gold].");

    public override bool IsAllowed(IRunState runState)
    {
        bool necro = false;
        foreach (var player in runState.Players)
        {
            necro = necro || player.Character is Ironclad;
        }

        return necro;
    }

    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (!participants.Contains<Creature>(Owner.Creature) || Owner.PlayerCombatState == null)
        {
            return;
        }

       
        await PowerCmd.Apply<StrengthPower>(new ThrowingPlayerChoiceContext(), Owner.Creature, 1M, Owner.Creature, null, false);
        Player player = combatState.Players[Rng.Chaotic.NextInt(0, combatState.Players.Count)];
        if (player.PlayerCombatState is null || player.PlayerCombatState.DrawPile.Cards.Count == 0)
        {
            return;
        }
        CardModel card = player.PlayerCombatState.DrawPile.Cards[Rng.Chaotic.NextInt(0, player.PlayerCombatState.DrawPile.Cards.Count)];
        await CardCmd.Exhaust(new ThrowingPlayerChoiceContext(), card, false, false);
    }
    
}