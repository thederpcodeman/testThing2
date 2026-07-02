using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]
public class Shatter() : testThing2Relic
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;
    
    public override bool HasUponPickupEffect => true;

    public override List<(string, string)> Localization => new PowerLoc(
        "Shatter",
        "Duplicate your entire Deck. Add Bad Luck to your Deck.",
        "Duplicate your entire Deck. Add Bad Luck to your Deck.");
    
    public override async Task AfterObtained()
    {
        int originalDeckSize = Owner.Deck.Cards.Count;
        for (int i = 0; i < originalDeckSize; ++i)
        {
            CardCmd.PreviewCardPileAdd(await CardPileCmd.Add(Owner.RunState.CloneCard(Owner.Deck.Cards[i]), PileType.Deck), style: CardPreviewStyle.MessyLayout);
            await Cmd.CustomScaledWait(0.1f, 0.2f);
        }
        await Cmd.CustomScaledWait(0.6f, 1.2f);
        await CardPileCmd.AddCurseToDeck<BadLuck>(Owner);
    }
}