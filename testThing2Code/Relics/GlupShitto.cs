using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Runs.History;
using testThing2.testThing2Code.Cards;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]
public class GlupShitto() : testThing2Relic
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;
    

    public override List<(string, string)> Localization => new PowerLoc(
        "Glup Shitto",
        "At the start of each combat, add a random obscure star wars character to your draw pile.",
        "At the start of each combat, add a random obscure star wars character to your draw pile.");


    public override Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (!participants.Contains<Creature>(Owner.Creature) || Owner.PlayerCombatState == null || Owner.PlayerCombatState.TurnNumber > 1)
            return base.AfterSideTurnStart(side, participants, combatState);
        else
        {
            
            return CardPileCmd.AddGeneratedCardsToCombat(GlupGet(), PileType.Draw, Owner, CardPilePosition.Random);
        }
    }

    public IEnumerable<CardModel> GlupGet()
    {
        List<CardModel> card = new List<CardModel>([]);
        List<CardModel> options = new List<CardModel>([]);
        
        options.Add(ModelDb.Card<LukeButTwoTaller>());
        options.Add(ModelDb.Card<DexterJexter>());
        
        card.Add(options[Rng.Chaotic.NextInt(0, options.Count)]);
        return card;
    }

    
}