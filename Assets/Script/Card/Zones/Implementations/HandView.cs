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
        Debug.Log("1");
        Reorder();
        return view;
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
        Debug.Log("ordering");
        var orderedAnchors = anchors.OrderBy(kvp =>
        {
            var anchorPos = kvp.Value.transform.position.x;
            var viewPos = -1f;
            if (views.TryGetValue(kvp.Key, out var view))
            {
                viewPos = view.transform.position.x;
                Debug.Log(1 + ". " + kvp.Value.name + " - " + anchorPos + " - " + viewPos);
                return view.transform.position.x;
            }
            Debug.Log(2 + ". " + kvp.Value.name + " - " + anchorPos + " - " + viewPos);
            return kvp.Value.transform.position.x;
        }).ToArray();

        Debug.Log($"First one is: {orderedAnchors.First().Value.name}");
        yield return null;

        for (int i = 0; i < orderedAnchors.Length; i++)
        {
            var anchor = orderedAnchors[i].Value;
            anchor.transform.SetSiblingIndex(i);
            anchor.RefreshAnchor();
        }
        Debug.Log($"First child is: {orderedAnchors.First().Value.transform.parent.GetChild(0).name}");

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reorder();
        }
    }
}

[Serializable]
public class Hand : CardZone
{

}
