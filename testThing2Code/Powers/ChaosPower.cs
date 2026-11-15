using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using testThing2.testThing2Code.Powers;
using Chaos = testThing2.testThing2Code.Relics.FunnyHat;

namespace testThing2.testThing2Code.Powers;

public class ChaosPower : CustomTemporaryPowerModel
{
    public override AbstractModel OriginModel => ModelDb.Relic<Chaos>();
    public override PowerModel InternallyAppliedPower => ModelDb.Power<SimpletonPower>();
    protected override Func<PlayerChoiceContext, Creature, decimal, Creature?, CardModel?, bool, Task> ApplyPowerFunc => PowerCmd.Apply<SimpletonPower>;
}
