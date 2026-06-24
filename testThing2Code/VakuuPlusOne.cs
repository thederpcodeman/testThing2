using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Events;
using MegaCrit.Sts2.Core.Models.Relics;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code;


public class VakuuPlusOne : CustomAncientModel {
    protected override OptionPools MakeOptionPools => new OptionPools(
        [
            AncientOption<BloodSoakedRose>(),
            AncientOption<WhisperingEarring>(),
            AncientOption<Fiddle>(),
            //AncientOption<TwinDice>()
            //more relic options
        ], [
            AncientOption<PreservedFog>(),
            AncientOption<SereTalon>(),
            AncientOption<DistinguishedCape>(),
            //AncientOption<CorruptedLance>(),
            //AncientOption<EmptyGem>()
            //more relic options
        ], [
            AncientOption<ChoicesParadox>(),
            AncientOption<MusicBox>(),
            AncientOption<LordsParasol>(),
            AncientOption<JeweledMask>(),
            //AncientOption<VoidVial>(),
            //AncientOption<BottomlessTankard>()
            //more relic options
        ]
    );
    public override bool IsValidForAct(ActModel act)
    {
        return act.ActNumber() == 3;
    }

    public override bool ShouldForceSpawn(ActModel act, AncientEventModel? rngChosenAncient)
    {
        return true;
    }
    
    protected override IReadOnlyList<EventOption> GenerateInitialOptions()
    {
        var base3pools = OptionPools.Roll(Rng, (AncientEventModel) this).Select<BaseLib.Utils.AncientOption, EventOption>((Func<BaseLib.Utils.AncientOption, EventOption>) (option => this.RelicOption(option.ModelForOption))).ToList<EventOption>();
        List<EventOption> list4 = this.Pool4.ToList<EventOption>();
        list4.UnstableShuffle<EventOption>(this.Rng);
        base3pools.Add(list4[0]);
        return base3pools;
    }
    
    private IEnumerable<EventOption> Pool4
    {
        get
        {
            return (IEnumerable<EventOption>) new List<EventOption>(new EventOption[5]
            {
                this.RelicOption<EmptyGem>(),
                this.RelicOption<BottomlessTankard>(),
                this.RelicOption<TwinDice>(),
                this.RelicOption<CorruptedLance>(),
                this.RelicOption<VoidVial>()
            });
        }
    }
    
}