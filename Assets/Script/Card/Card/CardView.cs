using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public CardAnchor Anchor { get; private set; }
    private Vector3 targetPos;
    private bool synced = false;

    [SerializeField] private float followSpeed;
    public CardState State { get; private set; } = CardState.Idle;

    public TextMeshPro Text;

    public void Setup(CardInstance card)
    {
    }

    public void SetViewAnchor(CardAnchor anchor)
    {
        ReleaseAnchor();

        Anchor = anchor;
        Anchor.OnCardAnchorChanged += UpdateCardView;
        ResetTargetPos();
    }

    public void SetState(CardState state)
    {
        State = state;
    }

    private void ReleaseAnchor()
    {
        if (Anchor != null)
        {
            Anchor.OnCardAnchorChanged -= UpdateCardView;
        }
    }

    private void OnDestroy()
    {
        ReleaseAnchor();
    }

    private void Update()
    {
        if (synced)
        {
            return;
        }

        if(Mathf.Approximately(Vector3.Distance(targetPos, transform.position), 0))
        {
            synced = true;
            return;
        }

        var direction = targetPos - transform.position;
        var moveVector = followSpeed * Time.deltaTime * direction.normalized;
        moveVector = Vector3.ClampMagnitude(moveVector, direction.magnitude);

        transform.position += moveVector;
    }

    private void UpdateCardView()
    {
        synced = false;
        if(State != CardState.Idle)
        {
            return;
        }
        ResetTargetPos();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        State = CardState.Controlled;
        synced = false;
        var pos = Camera.main.ScreenToWorldPoint(eventData.position);
        pos.z = 0;
        targetPos = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        State = CardState.Idle;
        ResetTargetPos();
    }

    private void ResetTargetPos()
    {
        synced = false;
        var pos = Anchor.transform.position;
        pos.z = 0;
        targetPos = pos;
    }
}

