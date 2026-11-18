using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.RestSite;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using testThing2.testThing2Code.Cards;
using testThing2.testThing2Code.Extensions;

namespace testThing2.testThing2Code.Cards;
[Pool(typeof(QuestCardPool))]
public class InvokeTinky() : testThing2Card(-1,
    CardType.Quest, CardRarity.Quest,
    TargetType.None)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];
    
    public override int MaxUpgradeLevel => 0;
    
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            return (IEnumerable<CardKeyword>) new List<CardKeyword>([CardKeyword.Unplayable]);
        }
    }
    /**
    public override bool TryModifyRestSiteOptions(Player player, ICollection<RestSiteOption> options)
    {
        if (player != this.Owner || options.Any<RestSiteOption>((Func<RestSiteOption, bool>) (o => o is InvokeRestSiteOption)))
            return false;
        options.Add((RestSiteOption) new InvokeRestSiteOption(player));
        return true;
    }
    */
}