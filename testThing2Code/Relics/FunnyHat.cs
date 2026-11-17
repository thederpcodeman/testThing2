using System.Collections;
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
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Runs;
using testThing2.testThing2Code.Extensions;
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
        "Each turn, a card in someones draw pile is afflicted with [yellow]Fun![/yellow].",
        "Each turn, a card in someones draw pile is afflicted with [yellow]fun[/yellow].");
    
    public new bool IsValidForRun(IRunState runState)
    {
        bool necro = false;
        foreach (var player in runState.Players)
        {
            necro = necro || player.Character is Fool;
        }

        return necro;
    }

    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (!participants.Contains<Creature>(Owner.Creature))
        {
            return;
        }

        var cards = new List<CardModel>();
        foreach (var creature in participants)
        {
            if (creature.IsPlayer)
            {
                foreach (var card in PileType.Draw.GetPile(creature.Player).Cards)
                {
                    cards.Add(card);
                }
            }
        }

        await CardCmd.Afflict<AfflictedWithSilly>(cards[Owner.RunState.Rng.Niche.NextInt(0, cards.Count)], 1);

    }
    
}