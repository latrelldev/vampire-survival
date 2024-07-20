using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardZoneView<T> : MonoBehaviour, ICardZoneView where T : CardZone
{
    [Header("References")]
    [SerializeField] protected Transform holder;
    [SerializeField] protected CardViewController viewController;

    [Header("Prefabs")]
    [SerializeField] protected CardView zoneViewPrefab;
    [SerializeField] protected CardAnchor anchorPrefab;

    protected Dictionary<CardInstance, CardAnchor> anchors = new Dictionary<CardInstance, CardAnchor>();
    protected Dictionary<CardInstance, CardView> views = new Dictionary<CardInstance, CardView>();

    public virtual CardAnchor GetAnchorForCard(CardInstance card)
    {
        if (!anchors.TryGetValue(card, out var anchor))
        {
            anchor = Instantiate(anchorPrefab, holder);
            anchor.transform.name += $"({anchors.Count()})";
            anchor.SetZone(this);
            anchors.Add(card, anchor);
        }
        return anchor;
    }

    public virtual CardView GetViewForCard(CardInstance card)
    {
        if (views.TryGetValue(card, out var view))
        {
            view = Instantiate(zoneViewPrefab);
            view.transform.name += $"({views.Count()})";
            //CardAnchor anchor = anchors[card];
            view.Text.text = views.Count().ToString();
            view.Setup(card);
            views.Add(card, view);
        }
        return view;
    }

    private void TryBindViewAnchor(CardInstance card)
    {
        if(!anchors.TryGetValue(card, out var anchor))
        {
            return;
        }

        //if()
    }

    public virtual void RemoveCard(CardInstance card, bool destroyView)
    {
        if (anchors.TryGetValue(card, out var anchor))
        {
            anchors.Remove(card);
            Destroy(anchor.gameObject);
        }

        if (views.TryGetValue(card, out var view))
        {
            views.Remove(card);
            if (destroyView)
            {
                Destroy(view.gameObject);
            }
        }
    }

    public virtual void OnBeginCardDrag(CardAnchor cardObject, PointerEventData pointerData)
    {

    }

    public virtual void OnCardDrag(CardAnchor cardObject, PointerEventData pointerData)
    {

    }

    public virtual void OnEndCardDrag(CardAnchor cardObject, PointerEventData pointerData)
    {

    }

}
