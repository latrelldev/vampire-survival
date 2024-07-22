using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    private Dictionary<Type, CardZone> zones = new Dictionary<Type, CardZone>();

    [SerializeField] private Deck deck;
    [SerializeField] private Hand hand;
    [SerializeField] private PlayZone playZone;


    private void Awake()
    {
        RegisterZone(deck);
        RegisterZone(hand);
        RegisterZone(playZone);
    }

    private void Start()
    {
        List<CardInstance> cards = new List<CardInstance>()
        {
            new CardInstance(1),
            new CardInstance(2),
            new CardInstance(3),
            new CardInstance(4),
            new CardInstance(5),
        };

        Setup(cards);
    }

    private void RegisterZone(CardZone zone)
    {
        zones.TryAdd(zone.GetType(), zone);
    }

    private void Setup(List<CardInstance> cards)
    {
        Deck deck = GetZone<Deck>();
        foreach(var card in cards)
        {
            MoveCard(card, null, deck);
        }
    }

    public T GetZone<T>() where T : CardZone
    {
        return (T)zones[typeof(T)];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            var card = GetZone<Deck>().Cards.FirstOrDefault();
            if (card != null)
            {
                MoveCard(card, GetZone<Deck>(), GetZone<Hand>());
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            var card = GetZone<Hand>().Cards.FirstOrDefault();
            if (card != null)
            {
                MoveCard(card, GetZone<Hand>(), GetZone<Deck>());
            }
        }
    }

    public void MoveCard(CardInstance card, CardZone from, CardZone to)
    {
        if (from != null)
        {
            from.RemoveCard(card);
        }

        if(to != null)
        {
            to.AddCard(card);
        }

        CardEvents.OnCardMoved(card, from, to);
    }
}

public static class CardEvents
{
    public static Action<CardInstance, CardZone, CardZone> OnCardMoved = delegate { };
}