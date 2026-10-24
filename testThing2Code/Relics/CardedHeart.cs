using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Runs;
using testThing2.testThing2Code.Cards;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]
public class CardedHeart() : testThing2Relic
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
        "Card Heart",
        "Add a [purple]random[/purple] card that is granted by an [gold]ancient[/gold] to your to your [gold]Deck[/gold].",
        "Add a [purple]random[/purple] card that is granted by an [gold]ancient[/gold] to your to your [gold]Deck[/gold].");
    
    public override bool HasUponPickupEffect => true;

    public override async Task AfterObtained()
    {
        int max = 42;
        if (Owner.RunState.Players.Count > 1)
        {
            max++;
        }
        int choice = Owner.RunState.Rng.CombatCardGeneration.NextInt(0, max);
        
        // Glup Shittos
        if (choice == 0)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<DexterJexter>(Owner), PileType.Deck)]), 2f); 
        } else if (choice == 1)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<LukeButTwoTaller>(Owner), PileType.Deck)]), 2f); 
        } else if (choice == 2)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<ForeshadowCloud>(Owner), PileType.Deck)]), 2f); 
        } else if (choice == 3)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Glonk>(Owner), PileType.Deck)]), 2f); 
        } else if (choice == 4)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<TionMedon>(Owner), PileType.Deck)]), 2f); 
        } else if (choice == 5)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<NeinNunb>(Owner), PileType.Deck)]), 2f); 
        } else if (choice == 6)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Zuckuss>(Owner), PileType.Deck)]), 2f); 
        } else if (choice == 7)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Paodok>(Owner), PileType.Deck)]), 2f); 
        }else if (choice == 8)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<BenQuadinaros>(Owner), PileType.Deck)]), 2f); 
        }else if (choice == 9)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<GonkDroid>(Owner), PileType.Deck)]), 2f); 
        }else if (choice == 10)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Vyper>(Owner), PileType.Deck)]), 2f); 
        }
        
        // Kaizo Vakuu
        else if (choice == 11)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Yaoi>(Owner), PileType.Deck)]), 2f); 
        }
        else if (choice == 12)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Yuri>(Owner), PileType.Deck)]), 2f); 
        }
        else if (choice == 13)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Heteroslop>(Owner), PileType.Deck)]), 2f); 
        }
        
        // Vanilla Game Ancient Cards
        else if (choice == 14)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Break>(Owner), PileType.Deck)]), 2f); 
        }else if (choice == 15)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Corruption>(Owner), PileType.Deck)]), 2f); 
        }else if (choice == 16)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Suppress>(Owner), PileType.Deck)]), 2f); 
        }else if (choice == 17)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<WraithForm>(Owner), PileType.Deck)]), 2f); 
        }else if (choice == 18)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<MeteorShower>(Owner), PileType.Deck)]), 2f); 
        }else if (choice == 19)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<TheSealedThrone>(Owner), PileType.Deck)]), 2f); 
        }else if (choice == 20)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<ForbiddenGrimoire>(Owner), PileType.Deck)]), 2f); 
        }else if (choice == 21)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Protector>(Owner), PileType.Deck)]), 2f); 
        }else if (choice == 22)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<BiasedCognition>(Owner), PileType.Deck)]), 2f); 
        }else if (choice == 23)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Quadcast>(Owner), PileType.Deck)]), 2f); 
        }
        
        // non-character vanilla ancient cards
        else if (choice == 24)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Apotheosis>(Owner), PileType.Deck)]), 2f); 
        }else if (choice == 25)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Apparition>(Owner), PileType.Deck)]), 2f); 
        }else if (choice == 26)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<BrightestFlame>(Owner), PileType.Deck)]), 2f); 
        }else if (choice == 27)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Maul>(Owner), PileType.Deck)]), 2f); 
        }else if (choice == 28)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<NeowsFury>(Owner), PileType.Deck)]), 2f); 
        }else if (choice == 29)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Relax>(Owner), PileType.Deck)]), 2f); 
        }else if (choice == 30)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Whistle>(Owner), PileType.Deck)]), 2f); 
        }else if (choice == 31)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Wish>(Owner), PileType.Deck)]), 2f); 
        }
        
        // other cards from vanilla ancients
        else if (choice == 32)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Greed>(Owner), PileType.Deck)]), 2f); 
        }else if (choice == 33)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Dowsing>(Owner), PileType.Deck)]), 2f); 
        }else if (choice == 34)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Injury>(Owner), PileType.Deck)]), 2f); 
        }else if (choice == 35)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Guilty>(Owner), PileType.Deck)]), 2f); 
        }else if (choice == 36)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Luminesce>(Owner), PileType.Deck)]), 2f); 
        }else if (choice == 37)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Soot>(Owner), PileType.Deck)]), 2f); 
        }else if (choice == 38)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Dazed>(Owner), PileType.Deck)]), 2f); 
        }else if (choice == 39)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Enthralled>(Owner), PileType.Deck)]), 2f); 
        }else if (choice == 40)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<Folly>(Owner), PileType.Deck)]), 2f); 
        }else if (choice == 41)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<CurseOfTheBell>(Owner), PileType.Deck)]), 2f); 
        }
        else if (choice == 42)
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)
                new List<CardPileAddResult>([await CardPileCmd.Add((CardModel) Owner.RunState.CreateCard<YourEternalRewardCard>(Owner), PileType.Deck)]), 2f); 
        }
    }
}