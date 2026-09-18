using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Ancients;

public class KaizoVakuuAct1 : CustomAncientModel
{
    public override IEnumerable<EventOption> AllPossibleOptions
    {
        get => this.TotalPool();
    }

    protected override OptionPools MakeOptionPools => new OptionPools(
        [
            AncientOption<OcdRelic>(), // From Kaizo Vakuu
            AncientOption<WardOfProtection>(), // From Kaizo Vakuu
            AncientOption<ShatterRelic>(), // From Kaizo Vakuu
            AncientOption<BigMushroom>(), // From Events
            AncientOption<SixSeven>(), // From Kaizo Vakuu
            AncientOption<CursedPearl>(), // From Neow
            AncientOption<HeftyTablet>(), // From Neow
            AncientOption<LavaRock>(), // From Neow
            AncientOption<NeowsSacrifice>(), // From Neow
            AncientOption<PrecariousShears>(), // From Neow
            AncientOption<SilverCrucible>(), // From Neow
            AncientOption<EmberTea>(),      // From Events
        ], [
            AncientOption<NeowsBones>(), // From Neow
            AncientOption<NeowsBones>(), // From Neow
            AncientOption<Circlet>(), // From Neow
            AncientOption<TeaOfDiscourtesy>(),   // From Events
            AncientOption<FragrantMushroom>(), // From Events
        ]
    );

    protected IEnumerable<EventOption> TotalPool()
    {
        List<EventOption> pool = new List<EventOption>();
        
        pool.Add(RelicOption<OcdRelic>()); // From Kaizo Vakuu
        pool.Add(RelicOption<WardOfProtection>()); // From Kaizo Vakuu
        pool.Add(RelicOption<ShatterRelic>()); // From Kaizo Vakuu
        pool.Add(RelicOption<Circlet>()); // From Events
        pool.Add(RelicOption<FragrantMushroom>()); // From Events
        pool.Add(RelicOption<NeowsBones>()); // From Neow
        pool.Add(RelicOption<SixSeven>()); // From Kaizo Vakuu


        return pool;
    }

    public override bool IsValidForAct(ActModel act)
    {
        return act.Index == 0;
    }

    public override string? CustomScenePath => "scenes/events/background_scenes/testthingy-vakuuplusone.tscn";

    public override string? CustomMapIconPath =>
        "testThing2/images/packed/map/ancient/ancient_node_testthingy2-vakuu_plus_one.png";

    public override string? CustomMapIconOutlinePath =>
        "testThing2/images/packed/map/ancient/ancient_node_testthingy2-vakuu_plus_one_outline.png";

    public override string? CustomRunHistoryIconPath =>
        "testThing2/images/ui/run_history/testhingy2-vakuu_plus_one.png";

    public override string? CustomRunHistoryIconOutlinePath =>
        "testThing2/images/ui/run_history/testthingy2-vakuu_plus_one_outline.png";
}