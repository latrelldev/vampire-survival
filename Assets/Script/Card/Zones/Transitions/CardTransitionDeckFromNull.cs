using System.Collections;
using UnityEngine;

public class CardTransitionDeckFromNull: CardTransition<ICardZoneView, DeckView>
{
    public override bool ValidateTransition(ICardZoneView from, ICardZoneView to)
    {
        return from == null && to is DeckView;
    }

    protected override IEnumerator ExecuteTransition(CardInstance card, ICardZoneView from, DeckView to)
    {
        CardAnchor newAnchor = to.GetAnchorForCard(card);
        yield return null; //wait a frame so layout accounts for anchor
        CardView newView = to.GetViewForCard(card);
        newView.SetState(CardState.Animating);
        newView.transform.position = newAnchor.transform.position;
        newView.transform.rotation = newAnchor.transform.rotation;
        newView.transform.localScale = newAnchor.transform.localScale;

        Debug.Log(newView.transform.position);

        for (int i = 0; i < 100; i++)
        {
            newView.transform.localScale *= 1.01f;
            yield return null;
        }

        newView.transform.localScale = newAnchor.transform.localScale;
        newView.SetState(CardState.Idle);
        newView.gameObject.SetActive(false);
    }
}