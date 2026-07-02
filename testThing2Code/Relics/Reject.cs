using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.ValueProps;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(EventRelicPool))]
public class Reject() : testThing2Relic
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;
    
    public override bool HasUponPickupEffect => true;

    public override List<(string, string)> Localization => new PowerLoc(
        "Reject",
        "YOU CANNOT REFUSE!!",
        "Those who reject this privilege are penalized with DEATH Do you still defy!?");
    
    public override async Task AfterObtained()
    {
        await CreatureCmd.LoseMaxHp((PlayerChoiceContext) new ThrowingPlayerChoiceContext(), Owner.Creature, Owner.Creature.MaxHp, false);
        Flash();
        for (int i = 0; i < 20; i++)
        {
            var damage = new DamageVar(Owner.Creature.MaxHp, ValueProp.Unpowered);
            await CreatureCmd.Damage((PlayerChoiceContext) new ThrowingPlayerChoiceContext(), Owner.Creature, damage,  null, null);
            Flash();
            await CardPileCmd.AddCursesToDeck(Enumerable.Repeat(ModelDb.Card<BadLuck>(), 1), Owner);
            Flash();
        }
    }
}