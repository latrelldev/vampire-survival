using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardViewController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform viewHolder;
    [SerializeField] private CardsManager cardManager;

    private Dictionary<CardInstance, ViewAnchorReference> references = new Dictionary<CardInstance, ViewAnchorReference>();
    private Dictionary<CardZone, ICardZoneView> zones = new Dictionary<CardZone, ICardZoneView>();

    [Header("Views")]
    [SerializeField] private HandView hand;
    [SerializeField] private DeckView deck;
    [SerializeField] private PlayZoneView playZone;

    private List<ICardTransition> transitions = new List<ICardTransition>()
    {
        new DefaultCardTransition(),
    };

    private void Awake()
    {
        RegisterZoneView(hand);
        RegisterZoneView(deck);
        RegisterZoneView(playZone);

        CardEvents.OnCardMoved += OnCardMoved;
    }

    private void OnDestroy()
    {
        CardEvents.OnCardMoved -= OnCardMoved;
    }

    private void RegisterZoneView<T>(CardZoneView<T> view) where T : CardZone
    {
        zones.Add(cardManager.GetZone<T>(), view);
    }

    private void OnCardMoved(CardInstance instance, CardZone zone1, CardZone zone2)
    {
        ICardZoneView fromView = zone1 == null ? null : zones[zone1];
        ICardZoneView toView = zone2 == null ? null : zones[zone2];
        ICardTransition transition = transitions.FirstOrDefault(t => t.ValidateTransition(fromView, toView));

        if (!references.TryGetValue(instance, out var reference))
        {
            reference = new ViewAnchorReference(instance, null, null, viewHolder);
            references[instance] = reference;
        }

        var routine = transition?.Execute(reference, fromView, toView);
        StartCoroutine(routine);
    }
}

public class ViewAnchorReference
{
    public ViewAnchorReference(CardInstance card, CardView viewInstance, CardAnchor anchor, Transform holder)
    {
        Card = card;
        ViewInstance = viewInstance;
        Anchor = anchor;
        Holder = holder;
    }

    public CardInstance Card { get; private set; }
    public CardView ViewInstance { get; private set; }
    public CardAnchor Anchor { get; private set; }
    public Transform Holder { get; private set; }

    public void SetNewViewAnchor(CardView view, CardAnchor anchor)
    {
        view.transform.SetParent(Holder, true);
        
        if (ViewInstance != null)
        {
            view.transform.position = ViewInstance.transform.position;
            view.transform.rotation = ViewInstance.transform.rotation;
            view.transform.localScale = ViewInstance.transform.localScale;
        }

        ViewInstance = view;
        SetNewAnchor(anchor);
    }

    public void SetNewAnchor(CardAnchor anchor)
    {
        Anchor = anchor;
        ViewInstance.SetViewAnchor(anchor);
    }
}
