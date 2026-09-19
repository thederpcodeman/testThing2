using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using testThing2.testThing2Code.Cards;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Cards;

[Pool(typeof(ColorlessCardPool))]
public class LukeButTwoTaller() : testThing2Card( 1,
    CardType.Power, CardRarity.Ancient,
    TargetType.AnyEnemy)
{
    
   
    protected override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            return (IEnumerable<DynamicVar>) new List<DynamicVar>([new DynamicVar("Shrink", 2M)]);
        }
    }
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => HoverTipFactory.FromPowerWithPowerHoverTips<ShrinkPower>(DynamicVars["Shrink"].IntValue);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        if (CombatState == null || CurrentTarget == null)
        {
            return;
        }
        await PowerCmd.Apply<ShrinkPower>(choiceContext, CurrentTarget, DynamicVars["Shrink"].BaseValue, Owner.Creature, (CardModel) this);
    }

    protected override void OnUpgrade() => this.DynamicVars["Shrink"].UpgradeValueBy(1M);
}