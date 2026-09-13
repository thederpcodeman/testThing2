using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using testThing2.testThing2Code.Cards;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Cards;
[Pool(typeof(GlupShittoCardPool))]
public class DexterJexter() : testThing2Card( 1,
    CardType.Power, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            return (IEnumerable<DynamicVar>) new List<DynamicVar>([new DynamicVar("Regen", 5M)]);
        }
    }
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            return (IEnumerable<CardKeyword>) new List<CardKeyword>([CardKeyword.Exhaust]);
        }
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "PowerUp", Owner.Character.PowerUpAnimDelay);
        if (play.Target != null)
        {
            await PowerCmd.Apply<RegenPower>(choiceContext, play.Target, DynamicVars["Regen"].BaseValue, Owner.Creature,
                (CardModel)this);
        }
    }

    protected override void OnUpgrade() => this.DynamicVars["Regen"].UpgradeValueBy(4M);
}