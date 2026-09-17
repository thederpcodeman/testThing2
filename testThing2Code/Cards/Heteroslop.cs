using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using testThing2.testThing2Code.Cards;

namespace testThing2.testThing2Code.Cards;

public class Heteroslop() : testThing2Card(-1,
    CardType.Curse, CardRarity.Curse,
    TargetType.None)
{
    private const int _numOfCardsPerTurn = 3;
    
    private const string _calculatedCardsKey = "CalculatedCards";
    
    protected override bool ShouldGlowRedInternal => this.ShouldPreventCardPlay;

    private bool ShouldPreventCardPlay => this.CardsPlayedThisTurn >= 3;

    public override int MaxUpgradeLevel => 0;
    
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            return (IEnumerable<CardKeyword>) new  List<CardKeyword>([CardKeyword.Unplayable, CardKeyword.Retain]);
        }
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            return (IEnumerable<DynamicVar>) new List<DynamicVar>(new DynamicVar[3]
            {
                (DynamicVar) new CalculationBaseVar(3M),
                (DynamicVar) new CalculationExtraVar(-1M),
                (DynamicVar) new CalculatedVar("CalculatedCards").WithMultiplier((Func<CardModel, Creature, Decimal>) ((card, _) => (Decimal) Math.Min(3, this.CardsPlayedThisTurn)))
            });
        }
    }

    public override bool ShouldPlay(CardModel card, AutoPlayType _)
    {
        if (card.Owner != this.Owner)
            return true;
        CardPile pile = this.Pile;
        return (pile != null ? (pile.Type != PileType.Hand ? 1 : 0) : 1) != 0 || !this.ShouldPreventCardPlay;
    }

    private int CardsPlayedThisTurn
    {
        get
        {
            return CombatManager.Instance.History.CardPlaysStarted.Count<CardPlayStartedEntry>((Func<CardPlayStartedEntry, bool>) (e => e.HappenedThisTurn(this.CombatState) && e.CardPlay.Player == this.Owner));
        }
    }
}