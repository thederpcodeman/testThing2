using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using testThing2.testThing2Code.Cards;

namespace testThing2.testThing2Code.Cards;


[Pool(typeof(ColorlessCardPool))]
public class Nibbly() : testThing2Card(0,
    CardType.Skill, CardRarity.Ancient,
    TargetType.None)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];
    
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
        
    }

    protected override void OnUpgrade()
    {

    }
}