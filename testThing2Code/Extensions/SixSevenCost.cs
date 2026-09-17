using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.TestSupport;

namespace testThing2.testThing2Code.Extensions;

public sealed class SixSevenCost : EnchantmentModel
{
    private int _testEnergyCostOverride = -1;

    public int TestEnergyCostOverride
    {
        get => this._testEnergyCostOverride;
        set
        {
            if (TestMode.IsOff)
                throw new InvalidOperationException("Only set this value in test mode.");
            this.AssertMutable();
            this._testEnergyCostOverride = value;
        }
    }

    public override bool CanEnchant(CardModel card)
    {
        return base.CanEnchant(card) && !card.Keywords.Contains(CardKeyword.Unplayable) && !card.EnergyCost.CostsX;
    }

    public override Task AfterCardDrawn(
        PlayerChoiceContext choiceContext,
        CardModel card,
        bool fromHandDraw)
    {
        if (card != this.Card || this.Card.Pile.Type != PileType.Hand)
            return Task.CompletedTask;
        this.Card.EnergyCost.SetThisCombat(this.NextEnergyCost());
        NCard.FindOnTable(card)?.PlayRandomizeCostAnim();
        return Task.CompletedTask;
    }

    private int NextEnergyCost()
    {
        return this.TestEnergyCostOverride >= 0 ? this.TestEnergyCostOverride : 67;
    }
}