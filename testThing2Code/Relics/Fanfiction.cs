using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Runs;
using testThing2.testThing2Code.Cards;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]
public class Fanfiction() : testThing2Relic
{

    public override RelicRarity Rarity =>
        RelicRarity.Ancient;
    
    public override bool IsAllowed(IRunState runState)
    {
        return runState.Players.Count > 1;
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new List<IHoverTip>([HoverTipFactory.FromCard<Yaoi>(), HoverTipFactory.FromCard<Yuri>(), HoverTipFactory.FromCard<Heteroslop>()]);
    }

    public override List<(string, string)> Localization => new PowerLoc(
        "Fanfiction",
        "Add either Yaoi, Yuri, or Heteroslop to your deck",
        "Add [gold]Your Eternal Reward[/gold] to your [blue]deck[/blue].");
    
    public override bool HasUponPickupEffect => true;

    public override async Task AfterObtained()
    {
        int rand = Rng.Chaotic.NextInt(0, 3);
        if (rand == 0)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>) new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Yaoi>(Owner), PileType.Deck)]), 2f);
        }else if (rand == 1)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>) new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Yuri>(Owner), PileType.Deck)]), 2f);
        }
        else if (rand == 2)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>) new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Heteroslop>(Owner), PileType.Deck)]), 2f);
        }
        
    }

    
}