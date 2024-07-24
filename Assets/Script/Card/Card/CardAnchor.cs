using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardAnchor : MonoBehaviour
{
    private ICardZoneView currentZone;
    
    public event Action OnCardAnchorChanged = delegate { };

    public CardState State { get; private set; } = CardState.Idle;
    
    public void SetZone(ICardZoneView zone)
    {
        currentZone = zone;
    }

    public void RefreshAnchor()
    {
        OnCardAnchorChanged?.Invoke();
    }

    private void OnTransformParentChanged()
    {
        Debug.Log("Transform Changed");
    }
}
