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


[Pool(typeof(EventRelicPool))]
public class FakeStoneHumidifier() : testThing2Relic
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;

    public override List<(string, string)> Localization => new PowerLoc(
        "StoneHumidifier",
        "Whoever you defeat an Elite, other Elites will begin with 3 additional Strength.",
        "Whoever you defeat an Elite, other Elites will begin with 3 additional Strength.");

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
        AbstractRoom currentRoom = combatState.RunState.CurrentRoom;
        if ((currentRoom != null ? (currentRoom.RoomType != RoomType.Elite ? 1 : 0) : 1) != 0)
            return;
        Flash();
        await PowerCmd.Apply<StrengthPower>((PlayerChoiceContext) new ThrowingPlayerChoiceContext(), Owner.Creature, DynamicVars["SelfStrength"].BaseValue, Owner.Creature, (CardModel) null);
        DynamicVars["EnemyStrength"].BaseValue += 3;
    }
}