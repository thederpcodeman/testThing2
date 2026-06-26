using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.ValueProps;
using testThing2.testThing2Code.Relics;
using Void = MegaCrit.Sts2.Core.Models.Cards.Void;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]
public class TwinDice() : testThing2Relic
{
    public override List<(string, string)> Localization => new PowerLoc(
        "Twin Dice",
        "Roll the Golden Die for a boon and the Cursed Die for a bane each turn.",
        "Roll the Golden Die for a boon and the Cursed Die for a bane each turn.");
    
    public override RelicRarity Rarity =>
        RelicRarity.Ancient; 
    
    
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        Random rand = new Random();
        int goldRoll = Rng.Chaotic.NextInt(1, 7);
        Flash();
        int cursedRoll = Rng.Chaotic.NextInt(1, 7);
        Flash();
        MainFile.Logger.Info("TwinDice Rolled: " + goldRoll + " | " + cursedRoll);

        // Curse 1: Void
        if (cursedRoll == 1)
        {
            await CardPileCmd.AddToCombatAndPreview<Void>(Owner.Creature, PileType.Discard, 1, Owner);
        } 
        // Curse 2: Dazed
        else if (cursedRoll == 2)
        {
            await CardPileCmd.AddToCombatAndPreview<Dazed>(Owner.Creature, PileType.Discard, 1, Owner);
        }
        // Curse 3: Weak
        else if (cursedRoll == 3)
        {
            await PowerCmd.Apply<WeakPower>(choiceContext, Owner.Creature, 1m, Owner.Creature, null, false);
        }
        // Curse 4: Vulnerable
        else if (cursedRoll == 4)
        {
            await PowerCmd.Apply<VulnerablePower>(choiceContext, Owner.Creature, 1m, Owner.Creature, null, false);
        }
        // Curse 5: Frail
        else if (cursedRoll == 5)
        {
            await PowerCmd.Apply<FrailPower>(choiceContext, Owner.Creature, 1m, Owner.Creature, null, false);
        }

        if (goldRoll == 1)
        {
            BlockVar block = new BlockVar(10, ValueProp.Move);
            await CreatureCmd.GainBlock(Owner.Creature, block, null, false);
        }
        else if (goldRoll == 2)
        {
            await PowerCmd.Apply<StrengthPower>(choiceContext, this.Owner.Creature, 1.0m, this.Owner.Creature, null, false);
        }
        else if (goldRoll == 3)
        {
            await PowerCmd.Apply<DexterityPower>(choiceContext, this.Owner.Creature, 1.0m, this.Owner.Creature, null, false);
        }
        else if (goldRoll == 4)
        {
            var prefs = new CardSelectorPrefs(SelectionScreenPrompt, 0, 2);
            var selected = (await CardSelectCmd.FromHand(choiceContext, Owner, prefs, c => c.IsUpgradable, this));
            foreach (var card in selected)
            {
                CardCmd.Upgrade(card);
            }
            
        }
        else if (goldRoll == 5)
        { 
            List<CardModel> list = CardFactory.GetDistinctForCombat(Owner, ModelDb.CardPool<ColorlessCardPool>().GetUnlockedCards(Owner.UnlockState, Owner.RunState.CardMultiplayerConstraint), 1, Owner.RunState.Rng.CombatCardGeneration).ToList<CardModel>();
            await CardPileCmd.AddGeneratedCardsToCombat((IEnumerable<CardModel>) list, PileType.Hand, Owner);
            
        }
        else if (goldRoll == 6)
        {
            await CardPileCmd.Draw(choiceContext, 1, Owner);
            await PlayerCmd.GainEnergy(1, Owner);
        }
        
    }
}