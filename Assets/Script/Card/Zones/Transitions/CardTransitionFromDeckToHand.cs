using System.Collections;
using UnityEngine;

public class CardTransitionFromDeckToHand : CardTransition<DeckView, HandView>
{
    protected override IEnumerator ExecuteTransition(CardInstance card, DeckView from, HandView to)
    {
        CardView currentView = from.GetViewForCard(card);
        from.RemoveCard(card, false);
        CardAnchor newAnchor = to.GetAnchorForCard(card);
        yield return null; //wait a frame so layout accounts for anchor
        CardView newView = to.GetViewForCard(card);

        if (currentView != null)
        {
            GameObject.Destroy(currentView.gameObject);

            newView.transform.position = new Vector3(newAnchor.transform.position.x, newAnchor.transform.position.y, currentView.transform.position.z);
            newView.transform.rotation = currentView.transform.rotation;
            newView.transform.localScale = currentView.transform.localScale;
        }

        newView.transform.position -= Vector3.up * 3f;
        newView.SetState(CardState.Idle);
    }
}