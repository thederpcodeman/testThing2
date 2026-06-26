using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]
public class CorruptedLance() : testThing2Relic
{
    public override List<(string, string)> Localization => new PowerLoc(
        "Corrupted Lance",
        "Whenever you add an attack to your deck, enchant it with corrupted, card rewards have an additional attack",
        "Whenever you add an attack to your deck, enchant it with corrupted, card rewards have an additional attack");
    
    public override RelicRarity Rarity =>
        RelicRarity.Ancient; 
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => HoverTipFactory.FromEnchantment<Corrupted>(1);
    }
    
    public override bool TryModifyCardRewardOptions(
        Player player,
        List<CardCreationResult> options,
        CardCreationOptions creationOptions)
    {
        if (this.Owner != player || creationOptions.Source != CardCreationSource.Encounter)
            return false;
        IEnumerable<CardModel> cardModels = creationOptions.GetPossibleCards(player).Where<CardModel>((Func<CardModel, bool>) (c => c.Type == CardType.Attack && options.TrueForAll((Predicate<CardCreationResult>) (o => o.originalCard.Id != c.Id))));
        if (!cardModels.Any<CardModel>())
            cardModels = creationOptions.GetPossibleCards(player).Where<CardModel>((Func<CardModel, bool>) (c => c.Type == CardType.Attack));
        if (!cardModels.Any<CardModel>())
            return false;
        CardModel card = CardFactory.CreateForReward(this.Owner, 1, new CardCreationOptions(cardModels, CardCreationSource.Other, creationOptions.RarityOdds).WithFlags(CardCreationFlags.NoModifyHooks | CardCreationFlags.NoCardPoolModifications)).FirstOrDefault<CardCreationResult>()?.Card;
        if (card != null)
        {
            CardCreationResult cardCreationResult = new CardCreationResult(card);
            cardCreationResult.ModifyCard(card, (RelicModel) this);
            options.Add(cardCreationResult);
        }
        return card != null;
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
        if (card.Owner != this.Owner || !ModelDb.Enchantment<Corrupted>().CanEnchant(card))
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