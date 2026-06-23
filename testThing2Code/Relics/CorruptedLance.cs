using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Runs;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(IroncladRelicPool))]
public class CorruptedLance() : testThing2Relic
{
    public override List<(string, string)> Localization => new PowerLoc(
        "Corrupted Lance",
        "Whenever you add an attack to your deck, enchant it with corrupted",
        "Whenever you add an attack to your deck, enchant it with corrupted");
    
    public override RelicRarity Rarity =>
        RelicRarity.Common; 
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => HoverTipFactory.FromEnchantment<Corrupted>(1);
    }
    
    public override bool TryModifyCardRewardOptionsLate(Player player, List<CardCreationResult> cardRewards, CardCreationOptions options)
    {
        if (player != Owner)
            return false;
        EnchantValidCards(cardRewards);
        return true;
    }

    public override void ModifyMerchantCardCreationResults(Player player, List<CardCreationResult> cards)
    {
        if (player != Owner)
            return;
        EnchantValidCards(cards);
    }

    public override bool TryModifyCardBeingAddedToDeck(CardModel card, out CardModel? newCard)
    {
        newCard = (CardModel) null;
        if (card.Owner != this.Owner || !ModelDb.Enchantment<Nimble>().CanEnchant(card))
            return false;
        newCard = this.EnchantCard(card);
        return true;
    }

    private void EnchantValidCards(List<CardCreationResult> options)
    {
        Corrupted corrupted = ModelDb.Enchantment<Corrupted>();
        foreach (CardCreationResult option in options)
        {
            CardModel card = option.Card;
            if (corrupted.CanEnchant(card))
            {
                option.ModifyCard(this.EnchantCard(card), (RelicModel) this);
            }
        }
    }
    private CardModel EnchantCard(CardModel card)
    {
        CardModel card1 = this.Owner.RunState.CloneCard(card);
        CardCmd.Enchant<Corrupted>(card1, 1);
        return card1;
    }
    
}