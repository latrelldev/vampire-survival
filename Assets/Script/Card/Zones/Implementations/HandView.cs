using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.LowLevel;

public class HandView : CardZoneView<Hand>
{
    private Coroutine reordering;

    public override CardView GetViewForCard(CardInstance card)
    {
        var view = base.GetViewForCard(card);
        Reorder();
        return view;
    }

    public override CardAnchor GetAnchorForCard(CardInstance card)
    {
        var anchor = base.GetAnchorForCard(card);
        anchor.transform.SetSiblingIndex(0);
        return anchor;
    }

    public override void RemoveCard(CardInstance card, bool destroyView)
    {
        base.RemoveCard(card, destroyView);
        Reorder();
    }

    public override void OnCardDrag(CardAnchor cardObject, PointerEventData pointerData)
    {
        Reorder();
    }

    public override void OnEndCardDrag(CardAnchor cardObject, PointerEventData pointerData)
    {
        Reorder();
        var drop = pointerData.hovered.Select(dz => dz.GetComponent<IDropZone>()).FirstOrDefault();
        if (drop != null)
        {
            Debug.Log("Trying to drop at " + drop);
        }
    }

    private void Reorder()
    {
        if (reordering != null)
        {
            StopCoroutine(reordering);
        }

        reordering = StartCoroutine(ReorderCO());
    }

    IEnumerator ReorderCO()
    {
        if (anchors.Count <= 0)
        {
            yield break;
        }

        var orderedAnchors = anchors.OrderBy(kvp =>
        {
            if (views.TryGetValue(kvp.Key, out var view) && view.State == CardState.Controlled)
            {
                return view.transform.position.x;
            }
            return kvp.Value.transform.position.x;
        }).ToArray();

        for (int i = 0; i < orderedAnchors.Length; i++)
        {
            var anchor = orderedAnchors[i].Value;
            anchor.transform.SetSiblingIndex(i);
        }

        yield return null;
        
        
        for (int i = 0; i < orderedAnchors.Length; i++)
        {
            var anchor = orderedAnchors[i].Value;
            Debug.Log(anchor.transform.position);
            var pos = anchor.transform.position;
            pos.z = orderedAnchors.Length - i;
            anchor.transform.position = pos;
            
            anchor.RefreshAnchor();
        }
    }
}

[Serializable]
public class Hand : CardZone
{

}
