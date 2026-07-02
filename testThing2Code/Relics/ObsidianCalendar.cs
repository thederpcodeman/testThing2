using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]
public class ObsidianCalendar() : testThing2Relic
{
    private bool _isActivating;
    
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;
    
    public override List<(string, string)> Localization => new PowerLoc(
        "Obsidian Calendar",
        "Gain 1 energy, at the end of turn 7, deal 52 damage to EVERYONE",
        "Gain 1 energy, at the end of turn 7, deal 52 damage to EVERYONE");

    public override Decimal ModifyMaxEnergy(Player player, Decimal amount)
    {
        return player != this.Owner ? amount : amount + this.DynamicVars.Energy.BaseValue;
    }
  
    
    public override bool ShowCounter => this.DisplayAmount > -1;

    public override int DisplayAmount
    {
        get
        {
            if (!CombatManager.Instance.IsInProgress || this.IsCanonical)
                return -1;
            int intValue = this.DynamicVars["DamageTurn"].IntValue;
            if (this.IsActivating)
                return intValue;
            int turnNumber = this.Owner.PlayerCombatState.TurnNumber;
            return turnNumber >= intValue ? -1 : turnNumber;
        }
    }
    private bool IsActivating
    {
        get => this._isActivating;
        set
        {
            this.AssertMutable();
            this._isActivating = value;
            this.InvokeDisplayAmountChanged();
        }
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars
  {
    get
    {
      return (IEnumerable<DynamicVar>) new List<DynamicVar>(new DynamicVar[3]
      {
        (DynamicVar) new DamageVar(52M, ValueProp.Unpowered),
        new DynamicVar("DamageTurn", 7M),
        new EnergyVar(1)
      });
    }
  }

  public override Task AfterSideTurnStart(
    CombatSide side,
    IReadOnlyList<Creature> participants,
    ICombatState combatState)
  {
    if (!participants.Contains<Creature>(this.Owner.Creature))
      return Task.CompletedTask;
    if (this.Owner.PlayerCombatState.TurnNumber == this.DynamicVars["DamageTurn"].IntValue)
      this.Status = RelicStatus.Active;
    this.InvokeDisplayAmountChanged();
    return Task.CompletedTask;
  }

  public override async Task BeforeSideTurnEnd(
    PlayerChoiceContext choiceContext,
    CombatSide side,
    IEnumerable<Creature> participants)
  {
    if (!participants.Contains<Creature>(Owner.Creature))
      return;
    int intValue = DynamicVars["DamageTurn"].IntValue;
    int turnNumber = Owner.PlayerCombatState.TurnNumber;
    Status = RelicStatus.Normal;
    if (turnNumber != intValue)
      return;
    TaskHelper.RunSafely(DoActivateVisuals());
    IEnumerable<DamageResult> damageResults = await CreatureCmd.Damage(choiceContext, (IEnumerable<Creature>) Owner.Creature.CombatState.Creatures, DynamicVars.Damage, Owner.Creature);
    InvokeDisplayAmountChanged();
  }

  public override Task AfterCombatEnd(CombatRoom _)
  {
    this.Status = RelicStatus.Normal;
    this.InvokeDisplayAmountChanged();
    return Task.CompletedTask;
  }

  public override Task AfterRoomEntered(AbstractRoom room)
  {
    if (!(room is CombatRoom))
      return Task.CompletedTask;
    this.Status = RelicStatus.Normal;
    this.InvokeDisplayAmountChanged();
    return Task.CompletedTask;
  }

  private async Task DoActivateVisuals()
  {
    IsActivating = true;
    Flash();
    await Cmd.Wait(1f);
    IsActivating = false;
  }
    
    
}