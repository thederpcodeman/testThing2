using Godot;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace testThing2.testThing2Code.Cards;

public class GlupShittoCardPool : CardPoolModel
{
  public override string Title => "glup shitto";

  public override string EnergyColorName => "glup shitto";

  public override string CardFrameMaterialPath => "card_frame_colorless";

  public override Color DeckEntryCardColor => new Color("A3A3A3FF");

  public override Color EnergyOutlineColor => new Color("A3A3A3FF");

  public override bool IsColorless => true;

  protected override CardModel[] GenerateAllCards()
  {
    return new CardModel[2]
    {
      (CardModel)ModelDb.Card<DexterJexter>(),
      (CardModel)ModelDb.Card<LukeButTwoTaller>()
    };
  }
  
  
}