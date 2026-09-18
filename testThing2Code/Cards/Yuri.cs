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
public class Yuri() : testThing2Card(0,
    CardType.Power, CardRarity.Ancient,
    TargetType.Self)
{
   
    protected override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            return (IEnumerable<DynamicVar>)new List<DynamicVar>([
                new EnergyVar(1), new CardsVar(1), new PowerVar<DexterityPower>(2M)
            ]);
        }
    }
    
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            return (IEnumerable<CardKeyword>) new List<CardKeyword>([CardKeyword.Innate, CardKeyword.Retain, CardKeyword.Eternal]);
        }
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        for (int i = 0; i < DynamicVars.Energy.IntValue; i++)
        {
            await CardPileCmd.Draw(choiceContext, Owner);
        }
        await PlayerCmd.GainEnergy((Decimal) DynamicVars.Energy.IntValue, Owner);
        await PowerCmd.Apply<DexterityPower>(choiceContext, Owner.Creature, DynamicVars.Power<DexterityPower>().IntValue, null,(CardModel) this);
    }

    protected override void OnUpgrade()
    { 
        DynamicVars.Power<DexterityPower>().UpgradeValueBy(2);
    }
}