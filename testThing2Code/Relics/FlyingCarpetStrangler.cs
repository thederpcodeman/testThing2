using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Runs;
using testThing2.testThing2Code.Relics;
using Array = Godot.Collections.Array;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]
public class FlyingCarpetStrangler() : testThing2Relic
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;

    public override List<(string, string)> Localization => new PowerLoc(
        "Flying Carpet",
        "Cards from the Merchant are [purple]Modified[/purple].",
        "Cards from the Merchant are [purple]Modified[/purple].");


    public override void ModifyMerchantCardCreationResults(Player player, List<CardCreationResult> cards)
    {
        foreach (var card in cards)
        {
            int rand = Owner.RunState.Rng.Niche.NextInt(0, 15);
            if (rand == 0)
            {
                CardCmd.Enchant<Sown>(card.Card, 1);
            }else if (rand == 1)
            {
                CardCmd.Enchant<Swift>(card.Card, 1);
            }else if (rand == 2)
            {
                CardCmd.Enchant<Swift>(card.Card, 2);
            }else if (rand == 3)
            {
                CardCmd.Enchant<Adroit>(card.Card, 2);
            }else if (rand == 4)
            {
                CardCmd.Enchant<Slither>(card.Card, 1);
            }else if (rand == 5)
            {
                CardCmd.Enchant<PerfectFit>(card.Card, 1);
            }else if (rand == 6)
            {
                CardCmd.Enchant<Glam>(card.Card, 1);
            }else if (rand == 7)
            {
                CardCmd.Upgrade(card.Card);
            }else if (rand == 8)
            {
                CardCmd.ApplyKeyword(card.Card, CardKeyword.Retain);
            }else if (rand == 9)
            {
                CardCmd.ApplyKeyword(card.Card, CardKeyword.Ethereal);
            }else if (rand == 10)
            {
                CardCmd.ApplyKeyword(card.Card, CardKeyword.Exhaust);
            }else if (rand == 11)
            {
                CardCmd.ApplyKeyword(card.Card, CardKeyword.Eternal);
            }else if (rand == 12)
            {
                CardCmd.ApplyKeyword(card.Card, CardKeyword.Innate);
            }else if (rand == 13)
            {
                CardCmd.ApplyKeyword(card.Card, CardKeyword.Sly);
            }else if (rand == 14)
            {
                CardCmd.ApplyKeyword(card.Card, CardKeyword.Unplayable);
            }
            
            
        }
    }
}