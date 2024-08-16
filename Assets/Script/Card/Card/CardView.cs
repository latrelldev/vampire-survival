using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardView : MonoBehaviour
{
    public CardInstance Card { get; private set; }
    public CardState State { get; private set; } = CardState.Idle;
    public CardAnchor Anchor { get; private set; }
    public ICardZoneView ZoneView { get; private set; }

    [SerializeField] private bool synced = false;
    [SerializeField] private float followSpeed;
    
    private Vector3 targetPos;

    public void Setup(CardInstance card)
    {
        Card = card;
    }

    public void SetViewAnchor(CardAnchor anchor, ICardZoneView zone)
    {
        ReleaseAnchor();

        ZoneView = zone;
        Anchor = anchor;
        Anchor.OnCardAnchorChanged += UpdateCardView;
        ResetTargetPos();
    }

    public void SetState(CardState state)
    {
        State = state;
        if(state == CardState.Idle)
        {
            ResetTargetPos();
        }
    }

    private void ReleaseAnchor()
    {
        if (Anchor != null)
        {
            Anchor.OnCardAnchorChanged -= UpdateCardView;
        }
    }

    private void UpdateCardView()
    {
        SetUnsynced();
        if(State != CardState.Idle)
        {
            return;
        }
        ResetTargetPos();
    }

    public void ResetTargetPos()
    {
        var pos = Anchor.transform.position;
        pos.z = Anchor.transform.GetSiblingIndex();
        SetTarget(pos);
    }

    public void ResetCurrentPos()
    {
        ResetTargetPos();
        transform.position = targetPos;
    }

    public void SetUnsynced()
    {
        synced = false;
    }

    public void SetTarget(Vector3 target)
    {
        targetPos = target;
        SetUnsynced();
    }

    private void Update()
    {
        if (State == CardState.Animating)
        {
            return;
        }

        if (synced)
        {
            return;
        }

        if (Mathf.Approximately(Vector3.Distance(targetPos, transform.position), 0))
        {
            synced = true;
            return;
        }

        var direction = targetPos - transform.position;
        var moveVector = followSpeed * Time.deltaTime * direction.normalized;
        moveVector = Vector3.ClampMagnitude(moveVector, direction.magnitude);

        transform.position += moveVector;
    }


    private void OnDestroy()
    {
        ReleaseAnchor();
    }

}

