using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Runs;
using testThing2.testThing2Code.Cards;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]
public class YourEternalRewardRelic() : testThing2Relic
{

    public override RelicRarity Rarity =>
        RelicRarity.Ancient;
    
    public override bool IsAllowed(IRunState runState)
    {
        return runState.Players.Count > 1;
    }
    
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
        CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>) new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<YourEternalRewardCard>(Owner), PileType.Deck)]), 2f);
    }
}