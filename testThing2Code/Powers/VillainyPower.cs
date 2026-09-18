using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;
using testThing2.testThing2Code.Powers;

namespace testThing2.testThing2Code.Powers;


public class VillainyPower() : testThing2Power
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override PowerInstanceType InstanceType => PowerInstanceType.InstancedPerApplier;

    protected override object InitInternalData() => (object)new Data();

    public override Task BeforeCardPlayed(CardPlay cardPlay)
    {
        if (this.Applier?.Player == null || cardPlay.Card.Owner != this.Applier.Player)
            return Task.CompletedTask;
        this.GetInternalData<Data>().amountsForPlayedCards.Add(cardPlay.Card, this.Amount);
        return Task.CompletedTask;
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int amount;
        if (!GetInternalData<Data>().amountsForPlayedCards.Remove(cardPlay.Card, out amount))
            return;
        Flash();
        var block = new CardsVar(amount);
        
        Player? applier = null;
        if (Applier != null && Applier.Player != null)
        {
            applier = Applier.Player;
        }
        await CardPileCmd.AddToCombatAndPreview<Dazed>(Owner, PileType.Draw, amount, applier, CardPilePosition.Random);
    }

    private class Data
    {
        /// <summary>
        /// Keep track of the cards we've seen played and the power amount at the time they were played.
        /// This lets Oblivion avoid triggering on itself.
        /// </summary>
        public readonly Dictionary<CardModel, int> amountsForPlayedCards = new Dictionary<CardModel, int>();
    }
}