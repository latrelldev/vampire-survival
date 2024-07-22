using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private CardView view;

    private Vector3 offset;
    private bool dragging = false;


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (view.State != CardState.Idle)
        {
            dragging = false;
            return;
        }

        dragging = true;
        view.SetState(CardState.Controlled);

        offset = Camera.main.ScreenToWorldPoint(eventData.position) - transform.position;
        offset.z = 0;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!dragging)
        {
            return;
        }

        var pos = Camera.main.ScreenToWorldPoint(eventData.position) - offset;
        pos.z = transform.position.z; //maintain current Z, as that is setup by anchor or custom
        view.SetTarget(pos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!dragging)
        {
            return;
        }

        dragging = false;
        view.SetState(CardState.Idle);
        view.ResetTargetPos();
    }
}
