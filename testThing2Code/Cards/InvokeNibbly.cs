using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using testThing2.testThing2Code.Cards;

namespace testThing2.testThing2Code.Cards;

public class InvokeNibbly() : testThing2Card(-1,
    CardType.Quest, CardRarity.Quest,
    TargetType.None)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];
    
    public override int MaxUpgradeLevel => 0;
    
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            return (IEnumerable<CardKeyword>) new List<CardKeyword>([CardKeyword.Unplayable]);
        }
    }
}