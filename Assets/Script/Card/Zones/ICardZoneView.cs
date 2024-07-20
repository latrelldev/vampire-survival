public interface ICardZoneView
{
    CardAnchor GetAnchorForCard(CardInstance card);
    CardView GetViewForCard(CardInstance card);
    void RemoveCard(CardInstance card, bool destroyView);
}