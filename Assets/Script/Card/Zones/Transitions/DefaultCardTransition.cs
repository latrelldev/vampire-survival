using System.Collections;
using UnityEngine;

public class DefaultCardTransition : CardTransition<ICardZoneView, ICardZoneView>
{
    public override bool ValidateTransition(ICardZoneView from, ICardZoneView to)
    {
        return true;
    }

    protected override IEnumerator ExecuteTransition(ViewAnchorReference reference, ICardZoneView from, ICardZoneView to)
    {
        CardInstance card = reference.Card;

        if (reference.ViewInstance != null)
        {
            reference.ViewInstance.SetState(CardState.Animating);
        }

        if (from != null)
        {
            from.RemoveCard(card, false);
        }

        CardView currentView = from == null ? null : from.GetViewForCard(card);
        if (to == null && currentView != null)
        {
            GameObject.Destroy(currentView.gameObject);
            yield break;
        }

        CardAnchor newAnchor = to.GetAnchorForCard(card);
        yield return null; //wait a frame so layout accounts for anchor
        CardView newView = to.GetViewForCard(card);
        
        if (currentView != null)
        {
            GameObject.Destroy(currentView.gameObject);
        }
        newView.SetState(CardState.Idle);
    }
}
