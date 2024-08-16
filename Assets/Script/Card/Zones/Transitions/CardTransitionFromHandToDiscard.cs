using System.Collections;
using UnityEngine;

public class CardTransitionFromHandToDiscard : CardTransition<HandView, DiscardView>
{
    protected override IEnumerator ExecuteTransition(CardInstance card, HandView from, DiscardView to)
    {
        CardView currentView = from.GetViewForCard(card);
        
        if (currentView != null)
        {
            currentView.SetState(CardState.Animating);
        }

        if (from != null)
        {
            from.RemoveCard(card, false);
        }

        CardAnchor newAnchor = to.GetAnchorForCard(card);
        yield return null; //wait a frame so layout accounts for anchor
        CardView newView = to.GetViewForCard(card);

        Vector3 localScale = Vector3.one;
        if (currentView != null)
        {
            GameObject.Destroy(currentView.gameObject);

            localScale = currentView.transform.localScale;
            
            newView.transform.rotation = currentView.transform.rotation;
            newView.transform.localScale = currentView.transform.localScale;
            
            var pos = newAnchor.transform.position;
            pos.z = currentView.transform.position.z;
            newView.transform.position = pos;
        }
        
        newView.SetState(CardState.Animating);
        while (newView.transform.localScale.x > 0)
        {
            newView.transform.localScale -= 3 * Time.deltaTime * Vector3.one;
            yield return null;
        }

        newView.gameObject.SetActive(false);
        newView.transform.localScale = localScale;
        newView.SetState(CardState.Idle);
    }
}
