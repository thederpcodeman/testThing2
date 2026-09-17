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
        "At the start of each turn, gain 1 strength and exhaust a random card in a random player's hand.",
        "At the start of each turn, gain 1 strength and exhaust a random card in a random player's hand.");

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
        CardModel card = player.Deck.Cards[Rng.Chaotic.NextInt(0, player.Deck.Cards.Count)];
        await CardCmd.Exhaust(new ThrowingPlayerChoiceContext(), card, false, false);
    }
    
}