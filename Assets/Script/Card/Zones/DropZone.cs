using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropZone
{
    public event Action<CardInstance, ICardZoneView> OnCardDropped = delegate { };

    public void OnCardDrop(CardInstance card, ICardZoneView from)
    {
        OnCardDropped?.Invoke(card, from);
    }
}