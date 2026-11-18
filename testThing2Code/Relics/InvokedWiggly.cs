using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;


[Pool(typeof(EventRelicPool))]
public class InvokedWiggly() : testThing2Relic
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;
    
    public override List<(string, string)> Localization => new PowerLoc(
        "Invocation",
        "[green]Enemies drop 5 additional gold.[/green]",
        "[green]Enemies drop 5 additional gold.[/green]");
    
    public override bool TryModifyRewards(Player player, List<Reward> rewards, AbstractRoom? room)
    {
        if (player != this.Owner || room == null || !room.RoomType.IsCombatRoom() || room.RoomType == RoomType.Boss && player.RunState.CurrentActIndex >= player.RunState.Acts.Count - 1)
            return false;
        rewards.Add((Reward) new GoldReward(this.DynamicVars.Gold.IntValue, player));
        return true;
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            return (IEnumerable<DynamicVar>) new List<DynamicVar>([(DynamicVar) new GoldVar(5)]);
        }
    }
    
}