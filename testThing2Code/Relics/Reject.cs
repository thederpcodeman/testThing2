using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.ValueProps;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]
public class Reject() : testThing2Relic
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;
    
    public override bool HasUponPickupEffect => true;

    public override List<(string, string)> Localization => new PowerLoc(
        "Reject",
        "[red]YOU CANNOT REFUSE!![/red]",
        "Those who reject this privilege are penalized with [red]DEATH[/red] Do you still defy!?");
    

    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (Status != RelicStatus.Disabled)
        {
            Flash();
            var damage = new DamageVar(Owner.Creature.MaxHp + 999, ValueProp.Unpowered);
            await CreatureCmd.Damage((PlayerChoiceContext) new ThrowingPlayerChoiceContext(), Owner.Creature, damage, Owner.Creature);
            this.Status = RelicStatus.Disabled;
        }
        
    }
}