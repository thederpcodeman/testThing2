using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Afflictions;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;
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

    public override async Task AfterDamageGiven(
        PlayerChoiceContext choiceContext,
        Creature? dealer,
        DamageResult result,
        ValueProp props,
        Creature target,
        CardModel? cardSource)
    {
        if (dealer != Owner.Creature || !props.IsPoweredAttack() || result.UnblockedDamage <= 0)
            return;
        if (Owner.PlayerRng.Transformations.NextInt(0, 67) == 0)
        {
            Flash();
            await PowerCmd.Apply<DoomPower>(choiceContext, target, (Decimal)67, dealer, (CardModel)null);
        }
    }

    public override async Task AfterBlockGained(Creature creature, decimal amount, ValueProp props, CardModel? cardSource)
    {
        if (creature == Owner.Creature)
        {
            if (Owner.PlayerRng.Transformations.NextInt(0, 67) == 0)
            {
                Flash();
                await CreatureCmd.GainBlock(Owner.Creature, new BlockVar(67, ValueProp.Unpowered), null, false);
            }
        }
    }

    public override bool TryModifyRewards(Player player, List<Reward> rewards, AbstractRoom? room)
    {
        if (player != this.Owner || room == null || !room.RoomType.IsCombatRoom() || room.RoomType == RoomType.Boss && player.RunState.CurrentActIndex >= player.RunState.Acts.Count - 1)
            return false;
        if (player.RunState.TotalFloor % 7 == 0)
        {
            Flash();
            rewards.Add((Reward) new GoldReward(67, player));
            return true;
        }
        return false;
    }
}