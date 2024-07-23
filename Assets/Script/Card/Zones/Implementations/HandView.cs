using System;
using System.Collections;
using System.Collections.Generic;
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

    public override void OnCardDrag(CardView cardObject, PointerEventData pointerData)
    {
        Reorder();
    }

    public override void OnEndCardDrag(CardView cardObject, PointerEventData pointerData)
    {
        Reorder();

        //var drops = GetPossibleDrops(pointerData);
        foreach(var hov in pointerData.hovered)
        {
            Debug.Log(hov.name);
        }
        var drops = pointerData.hovered.Select(d => d.GetComponent<IDropZone>()).Where(d => d != null).ToList();
        if (drops != null && drops.Count > 0)
        {
            Debug.Log("Trying to drop at " + (drops[0] as MonoBehaviour).name);
        }
    }

    private List<IDropZone> GetPossibleDrops(PointerEventData pointer)
    {
        var newPointer = new PointerEventData(EventSystem.current);
        newPointer.position = pointer.position;
        var hits = new List<RaycastResult>();
        EventSystem.current.RaycastAll(newPointer, hits);
        return hits.Select(h => h.gameObject.GetComponent<IDropZone>()).Where(d => d != null).ToList();
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
            var pos = anchor.transform.position;
           
            //pos.z = anchor.transform.parent.position.z + (orderedAnchors.Length - i);
            //Debug.Log(pos.z);

            anchor.transform.position = pos;
            anchor.RefreshAnchor();
        }
    }
}

[Serializable]
public class Hand : CardZone
{

}
