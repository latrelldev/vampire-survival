using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackgroundClick : MonoBehaviour, IPointerClickHandler
{
    public static Action<PointerEventData> OnGroundClicked = delegate { };
    
    public void OnPointerClick(PointerEventData eventData)
    {
        OnGroundClicked?.Invoke(eventData);
    }
}
