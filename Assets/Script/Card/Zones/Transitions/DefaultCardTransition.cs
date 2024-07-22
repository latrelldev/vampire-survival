using System.Collections;
using UnityEngine;

public class DefaultCardTransition : CardTransition<ICardZoneView, ICardZoneView>
{
    public override bool ValidateTransition(ICardZoneView from, ICardZoneView to)
    {
        return from != null && to != null;
    }

    protected override IEnumerator ExecuteTransition(CardInstance card, ICardZoneView from, ICardZoneView to)
    {
        CardView currentView = from == null ? null : from.GetViewForCard(card);
        if (from != null)
        {
            from.RemoveCard(card, false);
        }

        if (to == null && currentView != null)
        {
            GameObject.Destroy(currentView.gameObject);
            yield break;
        }

        if (currentView != null)
        {
            currentView.SetState(CardState.Animating);
        }

        CardAnchor newAnchor = to.GetAnchorForCard(card);
        yield return null; //wait a frame so layout accounts for anchor
        CardView newView = to.GetViewForCard(card);

        if (currentView != null)
        {
            GameObject.Destroy(currentView.gameObject);

            newView.transform.position = currentView.transform.position;
            newView.transform.rotation = currentView.transform.rotation;
            newView.transform.localScale = currentView.transform.localScale;
        }

        newView.SetState(CardState.Idle);
    }
}
