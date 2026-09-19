using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Rooms;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]
public class FakeStoneHumidifier() : testThing2Relic
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;

    public override List<(string, string)> Localization => new PowerLoc(
        "Stone Humidifier",
        "Whenever you defeat an [gold]Elite[/gold], the other [gold]Elites[/gold] in the act will gain [blue]{StrengthPower}[/blue] [gold]Strength[/gold] at the start of combat.",
        "Whenever you defeat an [gold]Elite[/gold], the other [gold]Elites[/gold] in the act will gain [blue]{StrengthPower}[/blue] [gold]Strength[/gold] at the start of combat.");

    protected override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            return (IEnumerable<DynamicVar>) new List<DynamicVar>(new DynamicVar[1]
            {
                (DynamicVar) new PowerVar<StrengthPower>("EnemyStrength", 0M)
            });
        }
    }
    
    public override async Task AfterSideTurnStart(
        CombatSide side,
        IReadOnlyList<Creature> participants,
        ICombatState combatState)
    {
        if (!participants.Contains<Creature>(Owner.Creature) || Owner.PlayerCombatState == null || Owner.PlayerCombatState.TurnNumber > 1)
            return;
        if (combatState.RunState.CurrentRoom == null)
        {
            return;
        }
        AbstractRoom currentRoom = combatState.RunState.CurrentRoom;
        if (currentRoom.RoomType != RoomType.Elite)
            return;
        Flash();
        foreach (var creature in combatState.Enemies)
        {
            if (creature.Side != Owner.Creature.Side)
            {
                await PowerCmd.Apply<StrengthPower>(new ThrowingPlayerChoiceContext(), creature, DynamicVars["EnemyStrength"].BaseValue, Owner.Creature,null,  false);
                
            }
        }
        this.DynamicVars["EnemyStrength"].UpgradeValueBy(3M);
        
    }

    public override async Task AfterActEntered()
    {
        DynamicVars["EnemyStrength"].UpgradeValueBy(DynamicVars["EnemyStrength"].BaseValue * -1M);
    }
    
}