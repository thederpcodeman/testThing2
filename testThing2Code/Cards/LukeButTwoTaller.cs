using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using testThing2.testThing2Code.Cards;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Cards;

[Pool(typeof(GlupShittoCardPool))]
public class LukeButTwoTaller() : testThing2Card( 0,
    CardType.Power, CardRarity.Rare,
    TargetType.AllEnemies)
{
    
   
    protected override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            return (IEnumerable<DynamicVar>) new List<DynamicVar>([new DynamicVar("Shrink", 2M)]);
        }
    }
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            return (IEnumerable<CardKeyword>) new List<CardKeyword>([CardKeyword.Innate]);
        }
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        foreach (Creature hittableEnemy in (IEnumerable<Creature>) this.CombatState.HittableEnemies)
        {
            await PowerCmd.Apply<ShrinkPower>(choiceContext, hittableEnemy, DynamicVars["Shrink"].BaseValue, Owner.Creature, (CardModel) this);
        }
    }

    protected override void OnUpgrade() => this.DynamicVars["Shrink"].UpgradeValueBy(4M);
}