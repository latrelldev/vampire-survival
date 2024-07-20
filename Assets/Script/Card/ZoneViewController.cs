using UnityEngine;

public class ZoneViewController: MonoBehaviour
{
    [SerializeField] private HandView hand;
    [SerializeField] private DeckView deck;
    [SerializeField] private PlayZoneView playZone;

    [SerializeField] private CardsManager cardsManager;

    private void Start()
    {
        RegisterZoneView(hand);
        RegisterZoneView(deck);
        RegisterZoneView(playZone);
    }

    public void RegisterZoneView<T>(CardZoneView<T> zoneView) where T: CardZone
    {
        T zone = cardsManager.GetZone<T>();
    }
}