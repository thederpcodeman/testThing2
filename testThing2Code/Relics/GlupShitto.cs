using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Runs.History;
using testThing2.testThing2Code.Cards;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]
public class GlupShitto() : testThing2Relic
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;
    
    public override bool HasUponPickupEffect => true;

    public override List<(string, string)> Localization => new PowerLoc(
        "Glup Shitto",
        "Add 1 of 2 obscure star wars characters to your deck.",
        "Add 1 of 2 obscure star wars characters to your deck.");
    
    
    public override async Task AfterObtained()
    {
        List<CardModel> options = [ModelDb.Card<DexterJexter>(), ModelDb.Card<LukeButTwoTaller>()];
        var chosenCard = await CardSelectCmd.FromChooseACardScreen((PlayerChoiceContext) new BlockingPlayerChoiceContext(), (IReadOnlyList<CardModel>) options, Owner, true);
        if (chosenCard != null)
        {
            await CardPileCmd.Add(chosenCard, Owner.Deck);
        }
    }

    
}