using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace testThing2.testThing2Code.Extensions;


public class AfflictedWithSilly : AfflictionModel
{
    
    private bool _wasUsed = false;
    
    public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
    {
        if (card == this.Card && !_wasUsed)
        {
            await CardCmd.AutoPlay(choiceContext, card, null);
            _wasUsed = true;
        }
    }
    
    public bool WasUsed
    {
        get => this._wasUsed;
        set
        {
            this.AssertMutable();
            this._wasUsed = value;
        }
    }
    
    
    public override bool HasExtraCardText => !_wasUsed;
    
}