using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;
using testThing2.testThing2Code.Cards;

namespace testThing2.testThing2Code.Cards;


[Pool(typeof(ColorlessCardPool))]
public class Tinky() : testThing2Card(0,
    CardType.Skill, CardRarity.Ancient,
    TargetType.None)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<DemesnePower>(0)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            return (IEnumerable<CardKeyword>) new List<CardKeyword>([CardKeyword.Innate, CardKeyword.Eternal, CardKeyword.Retain, CardKeyword.Exhaust]);
        }
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<ExtraTurnPower>(choiceContext, Owner.Creature, 1M, Owner.Creature, this);
        await PowerCmd.Apply<DemesnePower>(choiceContext, Owner.Creature, DynamicVars.Power<DemesnePower>().BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<DemesnePower>().UpgradeValueBy(2);
    }
}