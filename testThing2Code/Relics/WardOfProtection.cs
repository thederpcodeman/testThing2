using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Models.RelicPools;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]
public class WardOfProtection() : testThing2Relic
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;
    
    public override bool HasUponPickupEffect => true;

    public override List<(string, string)> Localization => new PowerLoc(
        "Ward of Protection",
        "Your entire deck becomes Eternal",
        "Your entire deck becomes Eternal");
    
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new List<IHoverTip>([HoverTipFactory.FromKeyword(CardKeyword.Eternal)]);
    }

    
    public override async Task AfterObtained()
    {
        Flash();
        
        for (int i = 0; i < Owner.Deck.Cards.Count; ++i)
        {
            CardModel card = Owner.Deck.Cards[i];
            card.AddKeyword(CardKeyword.Eternal);
        }
    }
}