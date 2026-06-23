using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.ValueProps;
using testThing2.testThing2Code.Relics;
using Void = MegaCrit.Sts2.Core.Models.Cards.Void;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(IroncladRelicPool))]
public class TwinDice() : testThing2Relic
{
    public override List<(string, string)> Localization => new PowerLoc(
        "Twin Dice",
        "Roll the Golden Die for a boon and the Cursed Die for a bane each turn.",
        "Roll the Golden Die for a boon and the Cursed Die for a bane each turn.");
    
    public override RelicRarity Rarity =>
        RelicRarity.Common;
    

    public override async Task BeforeSideTurnStart(
        PlayerChoiceContext choiceContext,
        CombatSide side,
        IReadOnlyList<Creature> participants,
        ICombatState combatState)
    {
        Random rand = new Random();
        int GoldRoll = rand.Next(1, 7);
        Flash();
        int CursedRoll = rand.Next(1, 7);
        Flash();
        MainFile.Logger.Info("TwinDice Rolled: " + GoldRoll + " | " + CursedRoll);

        // Curse 1: Void
        if (CursedRoll == 1)
        {
            await CardPileCmd.Add(new Void(), PileType.Discard, CardPilePosition.Random, this);
        } 
        // Curse 2: Dazed
        else if (CursedRoll == 2)
        {
            await CardPileCmd.Add(new Dazed(), PileType.Discard, CardPilePosition.Random, this);
        }
        // Curse 3: Weak
        else if (CursedRoll == 3)
        {
            await PowerCmd.Apply<WeakPower>(choiceContext, this.Owner.Creature, 1.0m, this.Owner.Creature, null, false);
        }
        // Curse 4: Vulnerable
        else if (CursedRoll == 4)
        {
            await PowerCmd.Apply<VulnerablePower>(choiceContext, this.Owner.Creature, 1.0m, this.Owner.Creature, null, false);
        }
        // Curse 5: Frail
        else if (CursedRoll == 5)
        {
            await PowerCmd.Apply<FrailPower>(choiceContext, this.Owner.Creature, 1.0m, this.Owner.Creature, null, false);
        }

        if (GoldRoll == 1)
        {
            BlockVar block = new BlockVar(10, ValueProp.Move);
            await CreatureCmd.GainBlock(Owner.Creature, block, null, false);
        }
        else if (GoldRoll == 2)
        {
            await PowerCmd.Apply<StrengthPower>(choiceContext, this.Owner.Creature, 1.0m, this.Owner.Creature, null, false);
        }
        else if (GoldRoll == 3)
        {
            await PowerCmd.Apply<DexterityPower>(choiceContext, this.Owner.Creature, 1.0m, this.Owner.Creature, null, false);
        }
        else if (GoldRoll == 4)
        {
            foreach (Creature c in  combatState.Enemies)
            {
                await PowerCmd.Apply<WeakPower>(choiceContext, this.Owner.Creature, 1.0m, this.Owner.Creature, null, false);
                await PowerCmd.Apply<VulnerablePower>(choiceContext, this.Owner.Creature, 1.0m, this.Owner.Creature, null, false);
            }
        }
        else if (GoldRoll == 5)
        {
            IReadOnlyList<CardPileAddResult> combat = await CardPileCmd.AddGeneratedCardsToCombat(ModelDb.CardPool<ColorlessCardPool>().GetUnlockedCards(Owner.UnlockState, Owner.RunState.CardMultiplayerConstraint), PileType.Hand, Owner);
            
        }
        else if (GoldRoll == 6)
        {
            await CardPileCmd.Draw(choiceContext, 1, Owner);
            await PlayerCmd.GainEnergy(1, Owner);
        }
        
    }
}