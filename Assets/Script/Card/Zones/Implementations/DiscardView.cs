using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardView : CardZoneView<Discard>
{
    [SerializeField]
    private DropZone dropZone;

    private void OnEnable()
    {
        dropZone.OnCardDropped += OnCardDropped;
    }

    private void OnDisable()
    {
        dropZone.OnCardDropped -= OnCardDropped;
    }

    private void OnCardDropped(CardInstance instance, ICardZoneView view)
    {
        if (view is not HandView)
        {
            return;
        }

        CardEvents.OnCardRecycled?.Invoke(instance);
    }
}

[Serializable]
public class Discard : CardZone
{

}