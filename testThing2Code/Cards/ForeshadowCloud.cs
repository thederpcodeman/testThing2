using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;
using testThing2.testThing2Code.Cards;
using testThing2.testThing2Code.Powers;

namespace testThing2.testThing2Code.Cards;
[Pool(typeof(ColorlessCardPool))]
public class ForeshadowCloud() : testThing2Card( 0,
    CardType.Power, CardRarity.Ancient,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            return (IEnumerable<DynamicVar>) new List<DynamicVar>([new DynamicVar("BeforeImage", 1M)]);
        }
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "PowerUp", Owner.Character.PowerUpAnimDelay);
        if (CombatState == null)
        {
            return;
        }
        foreach (Creature c in CombatState.Creatures)
        {
            await PowerCmd.Apply<Beforeimage>(choiceContext, c, DynamicVars["BeforeImage"].BaseValue, Owner.Creature,
                (CardModel)this);
        }
    }

    protected override void OnUpgrade() => this.DynamicVars["BeforeImage"].UpgradeValueBy(1M);
}