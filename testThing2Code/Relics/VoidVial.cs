using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Enchantments;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(IroncladRelicPool))]
public class voidVial() : CustomRelicModel
{
    public override List<(string, string)> Localization => new PowerLoc(
        "Void Vial",
        "Enchant up to 4 cards with Inky",
        "Enchant up to 4 cards with Inky");

public override RelicRarity Rarity => RelicRarity.Common;

    public override bool HasUponPickupEffect => true;
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => HoverTipFactory.FromEnchantment<Inky>();
    }
    
    public override async Task AfterObtained()
    {
        voidVial voidVial = this;
        CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 0, voidVial.DynamicVars.Cards.IntValue)
        {
            Cancelable = false,
            RequireManualConfirmation = true
        };
        Inky canonicalEnchantment = new Inky();
        foreach (CardModel card in await CardSelectCmd.FromDeckForEnchantment(voidVial.Owner, canonicalEnchantment, 4, prefs))
        {
            CardCmd.Enchant<Inky>(card, 1M);
        }
    }
    
}