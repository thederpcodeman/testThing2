using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Saves.Runs;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]
public class BottomlessTankard() : testThing2Relic
{

    private int _uses;
    
    private int _usesDelay;
    public override List<(string, string)> Localization => new PowerLoc(
        "Bottomless Tankard",
        "When you would die, revive with half your max hp, and add a decay to your deck",
        "When you would die, revive with half your max hp, and add a decay to your deck");
    
    public override RelicRarity Rarity =>
        RelicRarity.Ancient; 

    public override bool ShouldDieLate(Creature creature)
    {
        return creature != this.Owner.Creature || Owner.Creature.MaxHp <= 1;
    }
    
    public override async Task AfterPreventingDeath(Creature creature)
    {
        Flash();
        Owner.Creature.SetMaxHpInternal(Owner.Creature.MaxHp / 2);
        await CreatureCmd.Heal(creature, Math.Max(1M, (Decimal) creature.MaxHp));
        await CardPileCmd.AddCursesToDeck(Enumerable.Repeat(ModelDb.Card<Decay>(), 1), Owner);
        Uses++;
    }

    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        foreach (var creature in participants)
        {
            if (creature == this.Owner.Creature)
            {
                if (Owner.PlayerCombatState.TurnNumber != 1)
                {
                    if (Uses > UsesDelay)
                    {
                        for (int i = 0; i < Uses - UsesDelay; i++)
                        {
                            await CardPileCmd.AddToCombatAndPreview<Decay>(Owner.Creature, PileType.Draw, 1, Owner);
                        }

                        UsesDelay = Uses;
                    }
                }
            }
        }
    }
    
    [SavedProperty]
    public int Uses
    {
        get => this._uses;
        set
        {
            this.AssertMutable();
            this._uses = value;
        }
    }
    [SavedProperty]
    public int UsesDelay
    {
        get => this._usesDelay;
        set
        {
            this.AssertMutable();
            this._usesDelay = value;
        }
    }
}