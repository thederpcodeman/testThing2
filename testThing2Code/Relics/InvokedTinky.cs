using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.RelicPools;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;



[Pool(typeof(EventRelicPool))]
public class InvokedTinky() : testThing2Relic
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;
    
    public override List<(string, string)> Localization => new PowerLoc(
        "Invocation",
        "[yellow]Draw an extra card on the fifth turn of combat.[/yellow]",
        "[yellow]Draw an extra card on the fifth turn of combat.[/yellow]");
    
    protected override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            return (IEnumerable<DynamicVar>) new List<DynamicVar>([(DynamicVar) new CardsVar(1)]);
        }
    }
    
    
    public override Decimal ModifyHandDraw(Player player, Decimal count)
    {
        if (player.PlayerCombatState != null && player.PlayerCombatState.TurnNumber == 5)
        {
            return player != this.Owner ? count : count + (Decimal) this.DynamicVars.Cards.IntValue;
        }
        return base.ModifyHandDraw(player, count);
    }
}
