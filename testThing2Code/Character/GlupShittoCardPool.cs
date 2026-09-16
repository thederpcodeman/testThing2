using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Models;
using testThing2.testThing2Code.Cards;
using MegaCrit.Sts2.Core.Models.Cards;

#nullable enable
namespace testThing2.testThing2Code.Character;

public class GlupShittoCardPool : CustomCardPoolModel
{
  public override string Title => "glup shitto";

  public override string EnergyColorName => "glup shitto";

  public override string CardFrameMaterialPath => "card_frame_colorless";

  public override Color DeckEntryCardColor => new Color("A3A3A3FF");

  public override Color EnergyOutlineColor => new Color("A3A3A3FF");

  public override bool IsColorless => true;

  protected override CardModel[] GenerateAllCards()
  {
    return new CardModel[3]
    {
      (CardModel)ModelDb.Card<DexterJexter>(),
      (CardModel)ModelDb.Card<LukeButTwoTaller>(),
      (CardModel)ModelDb.Card<ForeshadowCloud>()
    };
  }
  
  
}