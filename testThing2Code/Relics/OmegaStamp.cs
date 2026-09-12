using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.ValueProps;
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
        "Enchant your deck with royally aprroved.",
        "Enchant your deck with royally aprroved.");
    
    public override async Task AfterObtained()
    {
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