using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]

public class InvokedPokey() : testThing2Relic
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;

    private bool _usedThisCombat = false;
    
    public override List<(string, string)> Localization => new PowerLoc(
        "Invocation",
        "[blue]The first time you create a card each combat, gain 4 block.[/blue]",
        "[blue]The first time you create a card each combat, gain 4 block.[/blue]");

    
    protected override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            return (IEnumerable<DynamicVar>) new List<DynamicVar>([(DynamicVar) new BlockVar(4M, ValueProp.Unpowered)]);
        }
    }

    private bool UsedThisCombat
    {
        get => this._usedThisCombat;
        set
        {
            this.AssertMutable();
            this._usedThisCombat = value;
        }
    }

    public override async Task AfterCardGeneratedForCombat(CardModel card, Player? creator)
    {
        if (creator == null || creator != Owner || UsedThisCombat)
            return;
        UsedThisCombat = true;
        Flash();
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, (CardPlay) null, true);
    }

    public override Task AfterCombatEnd(CombatRoom _)
    {
        this.UsedThisCombat = false;
        return Task.CompletedTask;
    }
    
}