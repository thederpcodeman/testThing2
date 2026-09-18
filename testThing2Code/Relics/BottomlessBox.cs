using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Nodes.Screens;
using MegaCrit.Sts2.Core.Random;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]
public class BottomlessBox() : testThing2Relic
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;
    
    public override bool HasUponPickupEffect => true;

    public override List<(string, string)> Localization => new PowerLoc(
        "Bottomless Box",
        "Transform your entire deck.",
        "Transform your entire deck.");

    
    public override async Task AfterObtained()
    {
        List<CardModel> cards = new List<CardModel>();
        foreach (var card in Owner.Deck.Cards)
        {
            cards.Add(card);
        }
        foreach (var card in cards)
        {
            if (card.IsTransformable)
            {
                await CardCmd.TransformToRandom(card, Owner.RunState.Rng.Niche);
            }
        }
    }
}