using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.RestSite;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.Models;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Extensions;

public class InvokeRestSiteOption(Player owner) : CustomRestSiteOption(owner)
{
    
    public override string OptionId => "INVOKETINKY";
    
    public override async Task<bool> OnSelect()
    {
        await RelicCmd.Obtain(ModelDb.Relic<InvokedTinky>().ToMutable(), Owner);
        return true;
    }

    
}