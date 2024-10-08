﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CardViewController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CardsManager cardManager;

    [Header("Views")]
    [SerializeField] private HandView hand;
    [SerializeField] private DeckView deck;
    [SerializeField] private PlayZoneView playZone;
    [SerializeField] private DiscardView discard;

    private List<ICardTransition> transitions;
    private Dictionary<CardZone, ICardZoneView> zones = new Dictionary<CardZone, ICardZoneView>();

    private bool animating = false;
    private Queue<IEnumerator> pendingTransitions = new Queue<IEnumerator>();

    public void Setup()
    {
        CardEvents.OnCardMoved += OnCardMoved;
        
        RegisterTransitions();

        RegisterZoneView(hand);
        RegisterZoneView(deck);
        RegisterZoneView(playZone);
        RegisterZoneView(discard);
    }

    public void OnDestroy()
    {
        CardEvents.OnCardMoved -= OnCardMoved;
    }

    private void RegisterTransitions()
    {
        transitions = GetComponentsInChildren<ICardTransition>().ToList();
        foreach (var transition in transitions)
        {
            transition.Set(this);
        }
    }

    private void RegisterZoneView<T>(CardZoneView<T> view) where T : CardZone
    {
        zones.Add(cardManager.GetZone<T>(), view);
    }

    private void OnCardMoved(CardInstance instance, CardZone zone1, CardZone zone2)
    {
        ICardZoneView fromView = zone1 == null ? null : zones[zone1];
        ICardZoneView toView = zone2 == null ? null : zones[zone2];

        Debug.Log($"Transition from {zone1} to {zone2}");
        ICardTransition transition = transitions.FirstOrDefault(t => t.ValidateTransition(fromView, toView));
        if (transition == null)
        {
            throw new Exception($"Failed to evaluate correct transition for card between {zone1} && {zone2}");
        }

        var routine = transition?.Execute(instance, fromView, toView);
        pendingTransitions.Enqueue(routine);

        if (animating)
        {
            return;
        }
        StartCoroutine(WaitForTransitionTimeCO());
    }

    private IEnumerator WaitForTransitionTimeCO()
    {
        animating = true;
        while (pendingTransitions.Count > 0)
        {
            var nextTransition = pendingTransitions.Dequeue();
            yield return nextTransition;
        }
        animating = false;
    }
}