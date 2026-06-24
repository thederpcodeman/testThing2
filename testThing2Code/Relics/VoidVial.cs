using System.Collections;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Enchantments;
using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(IroncladRelicPool))]
public class VoidVial() : testThing2Relic
{
    public override List<(string, string)> Localization => new PowerLoc(
        "VoidVial",
        "Enchant up to 4 cards with Inky",
        "Enchant up to 4 cards with Inky");

    public override RelicRarity Rarity => RelicRarity.Common;

    public override bool HasUponPickupEffect => true;
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => HoverTipFactory.FromEnchantment<Inky>();
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            List<DynamicVar> vars = [ (DynamicVar) new CardsVar(4)];
                    return (IEnumerable<DynamicVar>) vars;
        }
        
    }
    
    public override async Task AfterObtained()
    {
        CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 0, 4)
        {
            Cancelable = false,
            RequireManualConfirmation = true
        };
        CardSelectorPrefs setup = new CardSelectorPrefs(new LocString("relics", "TESTTHING2-VOID_VIAL.selectionScreenPrompt"), 4);
        foreach (CardModel card in await CardSelectCmd.FromDeckGeneric(Owner, prefs, c => c.Enchantment == null && c.Type == CardType.Attack))
        {
            CardCmd.Enchant<Inky>(card, 1M);
        }
    }
    
}