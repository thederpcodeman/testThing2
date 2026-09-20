
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using testThing2.testThing2Code.Cards;

namespace testThing2.testThing2Code.Cards;

[Pool(typeof(CurseCardPool))]
public class Heteroslop() : testThing2Card(-1,
    CardType.Curse, CardRarity.Curse,
    TargetType.None)
{
    public override bool CanBeGeneratedInCombat
    {
        get => false;
    }
    
    public override bool CanBeGeneratedByModifiers => false;
    protected override bool ShouldGlowRedInternal => this.ShouldPreventCardPlay;

    private bool ShouldPreventCardPlay => this.CardsPlayedThisTurn >= 3;

    public override int MaxUpgradeLevel => 0;
    
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            return (IEnumerable<CardKeyword>) new  List<CardKeyword>([CardKeyword.Unplayable, CardKeyword.Retain, CardKeyword.Eternal]);
        }
    }
    

    public override bool ShouldPlay(CardModel card, AutoPlayType _)
    {
        if (card.Owner != this.Owner)
            return true;
        CardPile pile = Pile;
        return (pile == null || pile.Type != PileType.Hand || !this.ShouldPreventCardPlay);
    }

    private int CardsPlayedThisTurn
    {
        get
        {
            return CombatManager.Instance.History.CardPlaysStarted.Count<CardPlayStartedEntry>((Func<CardPlayStartedEntry, bool>) (e => e.HappenedThisTurn(this.CombatState) && e.CardPlay.Player == this.Owner));
        }
    }
}