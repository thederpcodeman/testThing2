using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Ancients;

public class KaizoVakuuAct3 : CustomAncientModel {
    public override IEnumerable<EventOption> AllPossibleOptions
    {
        get => this.TotalPool();
    }
    protected override OptionPools MakeOptionPools => new OptionPools(
        [
            AncientOption<BloodSoakedRose>(),
            AncientOption<WhisperingEarring>(),
            AncientOption<Fiddle>()
        ], [
            AncientOption<PreservedFog>(),
            AncientOption<SereTalon>(),
            AncientOption<DistinguishedCape>()
        ], [
            AncientOption<ChoicesParadox>(),
            AncientOption<MusicBox>(),
            AncientOption<LordsParasol>(),
            AncientOption<JeweledMask>()
        ]
    );
    
    protected IEnumerable<EventOption> TotalPool()
    {
        List<EventOption> pool = new List<EventOption>();
        pool.Add(RelicOption<BiiigHug>());              // From Tezcatara
        pool.Add(RelicOption<CallingBell>());           // From Darv
        pool.Add(RelicOption<BiigHug>());               // From Kaizo Vakuu
        pool.Add(RelicOption<FakeStoneHumidifier>());   // From Kaizo Vakuu
        pool.Add(RelicOption<WardOfProtection>());            // From Kaizo Vakuu
        pool.Add(RelicOption<BottomlessBox>());            // From Kaizo Vakuu
        pool.Add(RelicOption<FlyingCarpetStrangler>()); // From Kaizo Vakuu
        pool.Add(RelicOption<Reject>());                // From Kaizo Vakuu
        pool.Add(RelicOption<BingBong>());              // From Events
        pool.Add(RelicOption<Circlet>());               // From Events
        pool.Add(RelicOption<FragrantMushroom>());      // From Events
        pool.Add(RelicOption<RoyalPoison>());           // From Events
        pool.Add(RelicOption<FakeSneckoEye>());         // From Events
        pool.Add(RelicOption<TeaOfDiscourtesy>());      // From Events
        pool.Add(RelicOption<Brimstone>());             // From Ironclad Shop Pool
        pool.Add(RelicOption<PaelsTooth>());            // From Pael
        pool.Add(RelicOption<GlupShitto>());            // From Kaizo Vakuu
        pool.Add(RelicOption<VoidVial>());              // From Kaizo Vakuu
        pool.Add(RelicOption<CursedBlade>());           // From Kaizo Vakuu
        pool.Add(RelicOption<UncountablyInfiniteBlades>());     // From Kaizo Vakuu
        pool.Add(RelicOption<EyeOfStars>());            // From Kaizo Vakuu
        pool.Add(RelicOption<BlackestOmen>());          // From Kaizo Vakuu
        pool.Add(RelicOption<Diffusion>());             // From Kaizo Vakuu
        pool.Add(RelicOption<VillainyRelicIronclad>()); // From Kaizo Vakuu
        pool.Add(RelicOption<VillainyRelicSilent>());   // From Kaizo Vakuu
        pool.Add(RelicOption<VillainyRelicRegent>());   // From Kaizo Vakuu
        pool.Add(RelicOption<VillainyRelicNecrobinder>()); // From Kaizo Vakuu
        pool.Add(RelicOption<VillainyRelicDefect>());   // From Kaizo Vakuu
        pool.Add(RelicOption<YourEternalRewardRelic>());// From Kaizo Vakuu
        pool.Add(RelicOption<SixSeven>());              // From Kaizo Vakuu
        pool.Add(RelicOption<Fanfiction>());            // From Kaizo Vakuu
        
        
        return pool;
    }

    public override bool IsValidForAct(ActModel act)
    {
        return act.Index == 2;
    }

    protected override IReadOnlyList<EventOption> GenerateInitialOptions()
    {
        List<EventOption> offerings = new List<EventOption>();
        List<EventOption> finalOptions = new List<EventOption>();
        List<EventOption> mainOptions = this.MainPool().ToList<EventOption>();
        List<EventOption> villainyOptions = this.VillainyPool().ToList<EventOption>();
        List<EventOption> validVillainyOptions = this.MainPool().ToList<EventOption>();
        foreach (var option in mainOptions)
        {
            if (option.Relic == null || Owner == null || option.Relic.IsAllowed(Owner.RunState))
            {
                finalOptions.Add(option);
            }
        }
        foreach (var option in villainyOptions)
        {
            if (option.Relic == null || Owner == null || option.Relic.IsAllowed(Owner.RunState))
            {
                validVillainyOptions.Add(option);
            }
        }

        if (validVillainyOptions.Count > 0)
        {
            validVillainyOptions.UnstableShuffle<EventOption>(this.Rng);
            finalOptions.Add(validVillainyOptions[0]);
        }
        finalOptions.UnstableShuffle<EventOption>(this.Rng);
        if (Rng.NextInt(0, 12) == 0)
        {
            offerings.Add(finalOptions[0]);      
            offerings.Add(finalOptions[0]);
            offerings.Add(finalOptions[0]);
        }
        else
        {
            offerings.Add(finalOptions[0]);      
            offerings.Add(finalOptions[1]);
            offerings.Add(RelicOption<Reject>());
        }
        return offerings;
    }

    protected List<EventOption> MainPool()
    {
        List<EventOption> pool = new List<EventOption>();
        pool.Add(RelicOption<BiiigHug>());              // From Tezcatara
        pool.Add(RelicOption<CallingBell>());           // From Darv
        pool.Add(RelicOption<BiigHug>());               // From Kaizo Vakuu
        pool.Add(RelicOption<FakeStoneHumidifier>());   // From Kaizo Vakuu
        pool.Add(RelicOption<WardOfProtection>());      // From Kaizo Vakuu
        pool.Add(RelicOption<BottomlessBox>());            // From Kaizo Vakuu
        pool.Add(RelicOption<BingBong>());              // From Events
        pool.Add(RelicOption<Circlet>());               // From Events
        pool.Add(RelicOption<FragrantMushroom>());      // From Events
        pool.Add(RelicOption<RoyalPoison>());           // From Events
        pool.Add(RelicOption<FakeSneckoEye>());         // From Events
        pool.Add(RelicOption<TeaOfDiscourtesy>());      // From Events
        pool.Add(RelicOption<Brimstone>());             // From Ironclad Shop Pool
        pool.Add(RelicOption<PaelsTooth>());            // From Pael
        pool.Add(RelicOption<GlupShitto>());            // From Kaizo Vakuu
        pool.Add(RelicOption<VoidVial>());              // From Kaizo Vakuu
        pool.Add(RelicOption<CursedBlade>());           // From Kaizo Vakuu
        pool.Add(RelicOption<UncountablyInfiniteBlades>());     // From Kaizo Vakuu
        pool.Add(RelicOption<EyeOfStars>());            // From Kaizo Vakuu
        pool.Add(RelicOption<BlackestOmen>());          // From Kaizo Vakuu
        pool.Add(RelicOption<Diffusion>());             // From Kaizo Vakuu
        pool.Add(RelicOption<YourEternalRewardRelic>());// From Kaizo Vakuu
        pool.Add(RelicOption<SixSeven>());              // From Kaizo Vakuu
        pool.Add(RelicOption<Fanfiction>());            // From Kaizo Vakuu
        return pool;
    }
    protected List<EventOption> VillainyPool()
    {
        List<EventOption> pool = new List<EventOption>();
    
        pool.Add(RelicOption<VillainyRelicIronclad>()); // From Kaizo Vakuu
        pool.Add(RelicOption<VillainyRelicSilent>());   // From Kaizo Vakuu
        pool.Add(RelicOption<VillainyRelicRegent>());   // From Kaizo Vakuu
        pool.Add(RelicOption<VillainyRelicNecrobinder>()); // From Kaizo Vakuu
        pool.Add(RelicOption<VillainyRelicDefect>());   // From Kaizo Vakuu
        return pool;
        
    }

    public override string? CustomScenePath => "scenes/events/background_scenes/testthingy-vakuuplusone.tscn";
    public override string? CustomMapIconPath => "testThing2/images/packed/map/ancient/ancient_node_testthingy2-vakuu_plus_one.png";

    public override string? CustomMapIconOutlinePath => "testThing2/images/packed/map/ancient/ancient_node_testthingy2-vakuu_plus_one_outline.png";

    public override string? CustomRunHistoryIconPath => "testThing2/images/ui/run_history/testhingy2-vakuu_plus_one.png";

    public override string? CustomRunHistoryIconOutlinePath => "testThing2/images/ui/run_history/testthingy2-vakuu_plus_one_outline.png";
    
    
    
}