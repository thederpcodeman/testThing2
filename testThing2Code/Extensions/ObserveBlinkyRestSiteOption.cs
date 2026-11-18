using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Extensions;
public class ObserveBlinkyRestSiteOption(Player owner) : CustomRestSiteOption(owner)
{

    public override string OptionId => "INVOKETINKY";

    public override async Task<bool> OnSelect()
    {
        List<Reward> rewards = new List<Reward>();
        rewards.Add((Reward) new CardReward(CardCreationOptions.ForNonCombatWithDefaultOdds([Owner.Character.CardPool], null), 5, Owner));
        rewards.Add((Reward) new CardReward(CardCreationOptions.ForNonCombatWithDefaultOdds([Owner.Character.CardPool], null), 5, Owner));
        await RewardsCmd.OfferCustom(Owner, rewards);
        return true;
    }

    public override LocString Description
    {
        get
        {
            LocString description = base.Description;
            description.Add("RelicName", ModelDb.Relic<InvokedBlinky>().Title.GetFormattedText());
            return description;
        }
    }
}