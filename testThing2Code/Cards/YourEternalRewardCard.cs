using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using testThing2.testThing2Code.Cards;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Cards;

[Pool(typeof(ColorlessCardPool))]
public class YourEternalRewardCard() : testThing2Card(1,
    CardType.Attack, CardRarity.Ancient,
    TargetType.AnyAlly)
{


    protected override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            return (IEnumerable<DynamicVar>)new List<DynamicVar>([
                new EnergyVar(1), new CardsVar(1), new DamageVar(7M, ValueProp.Move), new PowerVar<StrengthPower>(2M)
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
        ArgumentNullException.ThrowIfNull((object) cardPlay.Target, "cardPlay.Target");
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, cardPlay).Targeting(cardPlay.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        for (int i = 0; i < DynamicVars.Energy.IntValue; i++)
        {
            await CardPileCmd.Draw(choiceContext, Owner);
        }
        await PlayerCmd.GainEnergy((Decimal) DynamicVars.Energy.IntValue, Owner);
        await PowerCmd.Apply<StrengthPower>(choiceContext, Owner.Creature, DynamicVars.Power<StrengthPower>().IntValue, null,(CardModel) this);
    }

    protected override void OnUpgrade()
    { 
        DynamicVars.Damage.UpgradeValueBy(6);
        DynamicVars.Cards.UpgradeValueBy(1);
        DynamicVars.Energy.UpgradeValueBy(1);
        DynamicVars.Power<StrengthPower>().UpgradeValueBy(1);
    }
}