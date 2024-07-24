using System.Collections;

public class CardTransitionDiscardFromAnywhere : CardTransition<ICardZoneView, DiscardView>
{
    protected override IEnumerator ExecuteTransition(CardInstance card, ICardZoneView from, DiscardView to)
    {
        from.RemoveCard(card, true);
        CardAnchor newAnchor = to.GetAnchorForCard(card);
        CardView newView = to.GetViewForCard(card);
        newView.ResetCurrentPos();
        newView.gameObject.SetActive(false);
        yield break;
    }
}