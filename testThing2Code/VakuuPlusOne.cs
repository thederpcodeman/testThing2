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
        pool.Add(RelicOption<BloodSoakedRose>());       // From Vakuu
        pool.Add(RelicOption<WhisperingEarring>());     // From Vakuu
        pool.Add(RelicOption<Fiddle>());                // From Vakuu
        pool.Add(RelicOption<PreservedFog>());          // From Vakuu
        pool.Add(RelicOption<SereTalon>());             // From Vakuu
        pool.Add(RelicOption<DistinguishedCape>());     // From Vakuu
        pool.Add(RelicOption<ChoicesParadox>());        // From Vakuu
        pool.Add(RelicOption<MusicBox>());              // From Vakuu
        pool.Add(RelicOption<LordsParasol>());          // From Vakuu
        pool.Add(RelicOption<JeweledMask>());           // From Vakuu
        pool.Add(RelicOption<VoidVial>());              // From Vakuu Plus
        pool.Add(RelicOption<BottomlessTankard>());     // From Vakuu Plus
        pool.Add(RelicOption<TwinDice>());              // From Vakuu Plus
        pool.Add(RelicOption<EmptyGem>());              // From Vakuu Plus
        pool.Add(RelicOption<CorruptedLance>());        // From Vakuu Plus
        pool.Add(RelicOption<ToastyMittens>());         // From Tezcatara
        pool.Add(RelicOption<ToyBox>());                // From Tezcatara
        pool.Add(RelicOption<BiiigHug>());              // From Tezcatara
        pool.Add(RelicOption<BiigHug>());               // From Kaizo Vakuu
        
        
        return pool;
    }

    public override bool IsValidForAct(ActModel act)
    {
        return act.ActNumber() == 3;
    }
    
    protected override IReadOnlyList<EventOption> GenerateInitialOptions()
    {
        List<EventOption> Offerings = new List<EventOption>();
        List<EventOption> options = this.TotalPool().ToList<EventOption>();
        options.UnstableShuffle<EventOption>(this.Rng);
        Offerings.Add(options[0]);
        Offerings.Add(options[1]);
        Offerings.Add(options[2]);
        return Offerings;
    }

    public override string? CustomScenePath => "scenes/events/background_scenes/testthingy-vakuuplusone.tscn";
    public override string? CustomMapIconPath => "testThing2/images/packed/map/ancient/ancient_node_testthingy2-vakuu_plus_one.png";

    public override string? CustomMapIconOutlinePath => "testThing2/images/packed/map/ancient/ancient_node_testthingy2-vakuu_plus_one_outline.png";

    public override string? CustomRunHistoryIconPath => "testThing2/images/ui/run_history/testhingy2-vakuu_plus_one.png";

    public override string? CustomRunHistoryIconOutlinePath => "testThing2/images/ui/run_history/testthingy2-vakuu_plus_one_outline.png";
    
}