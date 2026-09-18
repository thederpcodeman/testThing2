using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using testThing2.testThing2Code.Cards;

namespace testThing2.testThing2Code.Cards;

[Pool(typeof(ColorlessCardPool))]
public class NeinNunb() : testThing2Card(1,
    CardType.Skill, CardRarity.Ancient,
    TargetType.Self)
{

    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get { return (IEnumerable<CardKeyword>)new List<CardKeyword>([CardKeyword.Exhaust]); }
    }

    protected override void OnUpgrade() => this.EnergyCost.UpgradeBy(-1);

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {

        List<CardModel> list = CardFactory.GetDistinctForCombat(Owner,
            Owner.Character.CardPool.GetUnlockedCards(Owner.UnlockState, Owner.RunState.CardMultiplayerConstraint)
                .Where<CardModel>((Func<CardModel, bool>)(c => c.Type == CardType.Power)), 3,
            Owner.RunState.Rng.CombatCardGeneration).ToList<CardModel>();
        var card1 =
            await CardSelectCmd.FromChooseACardScreen(choiceContext, (IReadOnlyList<CardModel>)list, Owner);
        if (card1 == null)
            return;
        card1.SetToFreeThisTurn();
        CardPileAddResult combat = await CardPileCmd.AddGeneratedCardToCombat(card1, PileType.Hand, Owner);
    }
}