using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Afflictions;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Runs;
using testThing2.testThing2Code.Cards;
using testThing2.testThing2Code.Extensions;

namespace testThing2.testThing2Code.Relics;
[Pool(typeof(EventRelicPool))]
public class SixSeven() : testThing2Relic
{

    public override RelicRarity Rarity =>
        RelicRarity.Ancient;
    

    public override List<(string, string)> Localization => new PowerLoc(
        "67",
        "[gold]67[/gold]",
        "[gold]67[/gold]");
    
    public override bool HasUponPickupEffect => true;

    public override async Task AfterObtained()
    {
        await CreatureCmd.SetMaxAndCurrentHp(Owner.Creature, 67);
        Owner.Gold = 67;
        int choice = Owner.RunState.Rng.Niche.NextInt(0, Owner.Deck.Cards.Count);
        CardModel card = Owner.Deck.Cards[choice];
        CardCmd.ClearEnchantment(card);
        CardCmd.Enchant<SixSevenCost>(card, 1);

    }
    
}