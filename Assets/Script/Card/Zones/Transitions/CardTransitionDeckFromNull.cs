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
        newView.gameObject.SetActive(false);
    }
}
