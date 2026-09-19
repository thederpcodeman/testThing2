using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Runs;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]
public class EyeOfStars() : testThing2Relic
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;
    
    public override List<(string, string)> Localization => new PowerLoc(
        "Eye of Stars",
        "#[red]Lose[/red] [E] at the start of each turn, but [green]all[/green] players gain [blue]1 star[/blue].",
        "#[red]Lose[/red] [E] at the start of each turn, but [green]all[/green] players gain [blue]1 star[/blue].");

    public override bool IsAllowed(IRunState runState)
    {
        bool necro = false;
        foreach (var player in runState.Players)
        {
            necro = necro || player.Character is Regent;
        }

        return necro;
    }

    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (!participants.Contains<Creature>(Owner.Creature) || Owner.PlayerCombatState == null)
        {
            return;
        }


        await PlayerCmd.LoseEnergy(1M, Owner);
        foreach (var player in combatState.Players)
        {
            await PlayerCmd.GainStars(1M, player);
        }
    }
}