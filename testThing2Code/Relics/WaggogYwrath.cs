using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using testThing2.testThing2Code.Cards;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]
public class WaggogYwrath() : testThing2Relic
{

    public override RelicRarity Rarity =>
        RelicRarity.Ancient;

    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => HoverTipFactory.FromCardWithCardHoverTips<YourEternalRewardCard>();
    }

    public override List<(string, string)> Localization => new PowerLoc(
        "Your Reward",
        "Add [gold]Your Eternal Reward[/gold] to your [gold]Deck[/gold].",
        "Add [gold]Your Eternal Reward[/gold] to your [gold]Deck[/gold].");

    public override bool HasUponPickupEffect => true;

    public override async Task AfterObtained()
    {
        // ISSUE: object of a compiler-generated type is created
        CardCmd.PreviewCardPileAdd(
            (IReadOnlyList<CardPileAddResult>)new List<CardPileAddResult>([
                await CardPileCmd.Add((CardModel)Owner.RunState.CreateCard<YourEternalRewardCard>(Owner), PileType.Deck)
            ]), 2f);
    }
}