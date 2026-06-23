using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Rooms;
using testThing2.testThing2Code.Relics;

namespace testThing2.testThing2Code.Relics;

[Pool(typeof(IroncladRelicPool))]
public class EmptyGem() : testThing2Relic
{
    public override List<(string, string)> Localization => new PowerLoc(
        "Empty Gem",
        "At the start of each combat, half of the cards in your deck become ethereal",
        "At the start of each combat, half of the cards in your deck become ethereal");
    
    public override RelicRarity Rarity =>
        RelicRarity.Common;

    public override async Task AfterRoomEntered(AbstractRoom room)
    {
        //StoneCracker stoneCracker = this;
        if (!(room is CombatRoom))
            return;
        Flash();
        List<CardModel> list = PileType.Draw.GetPile(Owner).Cards.ToList<CardModel>().StableShuffle<CardModel>(Owner.RunState.Rng.CombatCardSelection).Take<CardModel>(PileType.Draw.GetPile(Owner).Cards.ToList<CardModel>().Count / 2).ToList<CardModel>();
        foreach (CardModel cardModel in list)
        {
            CardCmd.ApplyKeyword(cardModel, CardKeyword.Ethereal);
        }
        CardCmd.Preview((IReadOnlyList<CardModel>) list);
        await Cmd.CustomScaledWait(0.5f, 1f);
    }
}