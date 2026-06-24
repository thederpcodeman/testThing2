using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Events;
using MegaCrit.Sts2.Core.Models.Relics;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code;

public class VakuuPlusOne : Vakuu {

    public override IEnumerable<EventOption> AllPossibleOptions
    {
        get => this.Pool1.Concat<EventOption>(this.Pool2).Concat<EventOption>(this.Pool3);
    }
    
    private IEnumerable<EventOption> Pool1
    {
        get
        {
            return (IEnumerable<EventOption>) new List<EventOption>(new EventOption[3]
            {
                this.RelicOption<BloodSoakedRose>(),
                this.RelicOption<WhisperingEarring>(),
                this.RelicOption<Fiddle>()
            });
        }
    }

    private IEnumerable<EventOption> Pool2
    {
        get
        {
            return (IEnumerable<EventOption>) new List<EventOption>(new EventOption[3]
            {
                this.RelicOption<PreservedFog>(),
                this.RelicOption<SereTalon>(),
                this.RelicOption<DistinguishedCape>().ThatDecreasesMaxHp(9M)
            });
        }
    }

    private IEnumerable<EventOption> Pool3
    {
        get
        {
            return (IEnumerable<EventOption>) new List<EventOption>(new EventOption[4]
            {
                this.RelicOption<ChoicesParadox>(),
                this.RelicOption<MusicBox>(),
                this.RelicOption<LordsParasol>(),
                this.RelicOption<JeweledMask>()
            });
        }
    }
    private IEnumerable<EventOption> Pool4
    {
        get
        {
            return (IEnumerable<EventOption>) new List<EventOption>(new EventOption[5]
            {
                this.RelicOption<VoidVial>(),
                this.RelicOption<TwinDice>(),
                this.RelicOption<EmptyGem>(),
                this.RelicOption<CorruptedLance>(),
                this.RelicOption<BottomlessTankard>()
            });
        }
    }
    
    protected override IReadOnlyList<EventOption> GenerateInitialOptions()
    {
        List<EventOption> list1 = this.Pool1.ToList<EventOption>();
        List<EventOption> list2 = this.Pool2.ToList<EventOption>();
        List<EventOption> list3 = this.Pool3.ToList<EventOption>();
        List<EventOption> list4 = this.Pool4.ToList<EventOption>();
        list1.UnstableShuffle<EventOption>(this.Rng);
        list2.UnstableShuffle<EventOption>(this.Rng);
        list3.UnstableShuffle<EventOption>(this.Rng);
        list4.UnstableShuffle<EventOption>(this.Rng);
        // ISSUE: object of a compiler-generated type is created
        return (IReadOnlyList<EventOption>) new List<EventOption>(new EventOption[4]
        {
            list1[0],
            list2[0],
            list3[0],
            list4[0]
        });
    }
}