using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleZone : MonoBehaviour
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
