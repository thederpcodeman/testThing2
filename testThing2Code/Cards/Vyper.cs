using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;
using testThing2.testThing2Code.Cards;

namespace testThing2.testThing2Code.Cards;

[Pool(typeof(ColorlessCardPool))]
public class Vyper() : testThing2Card( 2,
    CardType.Skill, CardRarity.Ancient,
    TargetType.AnyEnemy)
{
    
   
    protected override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            return (IEnumerable<DynamicVar>) new List<DynamicVar>([new PowerVar<PoisonPower>(10)]);
        }
    }
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new List<IHoverTip>([HoverTipFactory.FromPower<PoisonPower>()]);
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
        await PowerCmd.Apply<PoisonPower>(choiceContext, CurrentTarget, DynamicVars.Poison.BaseValue, Owner.Creature, (CardModel) this);
        if (CombatState == null)
            return;
        foreach (Creature creature in CombatState.GetTeammatesOf(Owner.Creature).Where<Creature>((Func<Creature, bool>) (c => c is
                 {
                     IsAlive: true, IsPlayer: true
                 })))
            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(CreateCloneForPlayer(creature.Player), PileType.Discard, Owner), 2.2f);
    }

    protected override void OnUpgrade() => this.DynamicVars.Poison.UpgradeValueBy(5M);
}