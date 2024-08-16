using System.Collections;

public class CardTransitionFromDiscardToDeck : CardTransition<DiscardView, DeckView>
{
    protected override IEnumerator ExecuteTransition(CardInstance card, DiscardView from, DeckView to)
    {
        if (from != null)
        {
            from.RemoveCard(card, true);
        }
        CardAnchor newAnchor = to.GetAnchorForCard(card);
        yield return null; //wait a frame so layout accounts for anchor
        CardView newView = to.GetViewForCard(card);
        newView.gameObject.SetActive(false);
    }
}
