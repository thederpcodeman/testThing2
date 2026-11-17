using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Rooms;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]

public class InvokedNibbly() : testThing2Relic
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;
    
    private const string _hpThresholdKey = "HpThreshold";
    
    public override List<(string, string)> Localization => new PowerLoc(
        "Invocation",
        "[pink]If your HP is at or below 50% at the end of combat, heal 5 HP.[/pink]",
        "[pink]If your HP is at or below 50% at the end of combat, heal 5 HP.[/pink]");
    
    protected override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            return (IEnumerable<DynamicVar>) new List<DynamicVar>(new DynamicVar[2]
            {
                new DynamicVar("HpThreshold", 50M),
                (DynamicVar) new HealVar(5M)
            });
        }
    }

    public override Task BeforeCombatStart()
    {
        if (this.WillHealOnCombatFinished())
            this.Status = RelicStatus.Active;
        return Task.CompletedTask;
    }

    public override Task AfterCurrentHpChanged(Creature creature, Decimal delta)
    {
        if (creature != this.Owner.Creature || !CombatManager.Instance.IsInProgress)
            return Task.CompletedTask;
        this.Status = this.WillHealOnCombatFinished() ? RelicStatus.Active : RelicStatus.Normal;
        return Task.CompletedTask;
    }

    public override async Task AfterCombatVictoryEarly(CombatRoom _)
    {
        if (Owner.Creature.IsDead)
            return;
        Creature creature = Owner.Creature;
        if (WillHealOnCombatFinished())
            return;
        Status = RelicStatus.Normal;
        await CreatureCmd.Heal(creature, DynamicVars.Heal.BaseValue);
    }

    private bool WillHealOnCombatFinished()
    {
        Creature creature = this.Owner.Creature;
        int num = (int) ((Decimal) creature.MaxHp * (this.DynamicVars["HpThreshold"].BaseValue / 100M));
        return creature.CurrentHp <= num;
    }
}