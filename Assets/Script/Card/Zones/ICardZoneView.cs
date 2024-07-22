using UnityEngine.EventSystems;

public interface ICardZoneView
{
    CardAnchor GetAnchorForCard(CardInstance card);
    CardView GetViewForCard(CardInstance card);
    void RemoveCard(CardInstance card, bool destroyView);

    void OnBeginCardDrag(CardView view, PointerEventData pointerData);
    void OnCardDrag(CardView view, PointerEventData pointerData);
    void OnEndCardDrag(CardView view, PointerEventData pointerData);
}