using System.Collections;

public class CardTransitionFromNull: CardTransition<ICardZoneView, ICardZoneView>
{
    public override bool ValidateTransition(ICardZoneView from, ICardZoneView to)
    {
        return from == null && to != null;
    }

    protected override IEnumerator ExecuteTransition(CardInstance card, ICardZoneView from, ICardZoneView to)
    {
        CardAnchor newAnchor = to.GetAnchorForCard(card);
        yield return null; //wait a frame so layout accounts for anchor
        CardView newView = to.GetViewForCard(card);
        newView.transform.SetParent(holder, true);

        newView.transform.position = newAnchor.transform.position;
        newView.transform.rotation = newAnchor.transform.rotation;
        newView.transform.localScale = newAnchor.transform.localScale;
        newView.SetState(CardState.Idle);
    }
}
