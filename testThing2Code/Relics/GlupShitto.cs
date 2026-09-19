
using System.Diagnostics;
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
using MegaCrit.Sts2.Core.Rooms;
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
        "At the start of each combat, add a random [gold]Obscure Star Wars Character[/gold] to your [gold]draw pile[/gold].",
        "At the start of each combat, add a random [gold]Obscure Star Wars Character[/gold] to your [gold]draw pile[/gold].");


    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (!participants.Contains<Creature>(Owner.Creature) || Owner.PlayerCombatState == null ||
            Owner.PlayerCombatState.TurnNumber > 1)
        {
            return;
        }
        
        int choice = Owner.RunState.Rng.CombatCardGeneration.NextInt(0, 9);
        if (choice == 0)
        {
            await CardPileCmd.AddToCombatAndPreview<DexterJexter>(Owner.Creature, PileType.Draw, 1, Owner, CardPilePosition.Random);    
        } else if (choice == 1)
        {
            await CardPileCmd.AddToCombatAndPreview<LukeButTwoTaller>(Owner.Creature, PileType.Draw, 1, Owner, CardPilePosition.Random);
        } else if (choice == 2)
        {
            await CardPileCmd.AddToCombatAndPreview<ForeshadowCloud>(Owner.Creature, PileType.Draw, 1, Owner, CardPilePosition.Random);
        } else if (choice == 3)
        {
            await CardPileCmd.AddToCombatAndPreview<Glonk>(Owner.Creature, PileType.Draw, 1, Owner, CardPilePosition.Random);
        } else if (choice == 4)
        {
            await CardPileCmd.AddToCombatAndPreview<TionMedon>(Owner.Creature, PileType.Draw, 1, Owner, CardPilePosition.Random);
        } else if (choice == 5)
        {
            await CardPileCmd.AddToCombatAndPreview<NeinNunb>(Owner.Creature, PileType.Draw, 1, Owner, CardPilePosition.Random);
        } else if (choice == 6)
        {
            await CardPileCmd.AddToCombatAndPreview<Zuckuss>(Owner.Creature, PileType.Draw, 1, Owner, CardPilePosition.Random);
        } else if (choice == 7)
        {
            await CardPileCmd.AddToCombatAndPreview<Paodok>(Owner.Creature, PileType.Draw, 1, Owner, CardPilePosition.Random);
        }else if (choice == 8)
        {
            await CardPileCmd.AddToCombatAndPreview<BenQuadinaros>(Owner.Creature, PileType.Draw, 1, Owner, CardPilePosition.Random);
        }else if (choice == 9)
        {
            await CardPileCmd.AddToCombatAndPreview<GonkDroid>(Owner.Creature, PileType.Draw, 1, Owner, CardPilePosition.Random);
        }
        
    }

    
}