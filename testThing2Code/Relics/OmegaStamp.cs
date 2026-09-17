using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.ValueProps;
using testThing2.testThing2Code.Cards;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]
public class OmegaStamp() : testThing2Relic
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;
    
    public override bool HasUponPickupEffect => true;

    public override List<(string, string)> Localization => new PowerLoc(
        "The Omega Stamp",
        "Add a regret to your deck, Enchant your deck with royally approved, draw 3 fewer cards at the start of your turn.",
        "Enchant your deck with royally approved, draw [blue]{Cards}[/blue] fewer {Cards:plural:card|cards} at the start of your turn.");
    
    protected override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            return (IEnumerable<DynamicVar>) new List<DynamicVar>([(DynamicVar) new CardsVar(3)]);
        }
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new List<IHoverTip>(HoverTipFactory.FromEnchantment<RoyallyApproved>());
    }

    public override Decimal ModifyHandDraw(Player player, Decimal count)
    {
        return player != this.Owner ? count : count - (Decimal) this.DynamicVars.Cards.IntValue;
    }
    public override async Task AfterObtained()
    {
        await CardPileCmd.AddCurseToDeck<Regret>(Owner);
        Flash();
        RoyallyApproved aproved = ModelDb.Enchantment<RoyallyApproved>();
        for (int i = 0; i < Owner.Deck.Cards.Count; ++i)
        {
            CardModel card = Owner.Deck.Cards[i];
            if (aproved.CanEnchant(card))
            {
                CardCmd.Enchant<RoyallyApproved>(card, 1M);
            }
        }
    }
}