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
    public override IEnumerable<EventOption> AllPossibleOptions
    {
        get => this.TotalPool();
    }
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

    protected IEnumerable<EventOption> TotalPool()
    {
        List<EventOption> pool = new List<EventOption>();
        pool.Add(RelicOption<BloodSoakedRose>());
        pool.Add(RelicOption<WhisperingEarring>());
        pool.Add(RelicOption<Fiddle>());
        pool.Add(RelicOption<TwinDice>());
        pool.Add(RelicOption<PreservedFog>());
        pool.Add(RelicOption<SereTalon>());
        pool.Add(RelicOption<DistinguishedCape>());
        pool.Add(RelicOption<CorruptedLance>());
        pool.Add(RelicOption<EmptyGem>());
        pool.Add(RelicOption<ChoicesParadox>());
        pool.Add(RelicOption<MusicBox>());
        pool.Add(RelicOption<LordsParasol>());
        pool.Add(RelicOption<JeweledMask>());
        pool.Add(RelicOption<VoidVial>());
        pool.Add(RelicOption<BottomlessTankard>());
        return pool;
    }

    public override bool IsValidForAct(ActModel act)
    {
        return act.ActNumber() == 3;
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

    public override string? CustomScenePath => "scenes/events/background_scenes/testthingy-vakuuplusone.tscn";
    public override string? CustomMapIconPath => "testThing2/images/packed/map/ancient/ancient_node_testthingy2-vakuu_plus_one.png";

    public override string? CustomMapIconOutlinePath => "testThing2/images/packed/map/ancient/ancient_node_testthingy2-vakuu_plus_one_outline.png";

    public override string? CustomRunHistoryIconPath => "testThing2/images/ui/run_history/testhingy2-vakuu_plus_one.png";

    public override string? CustomRunHistoryIconOutlinePath => "testThing2/images/ui/run_history/testthingy2-vakuu_plus_one_outline.png";
    
}