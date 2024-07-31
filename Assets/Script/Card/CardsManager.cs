using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    private Dictionary<Type, CardZone> zones = new Dictionary<Type, CardZone>();
    private Dictionary<CardInstance, CardZone> cardLookup = new Dictionary<CardInstance, CardZone>();

    [SerializeField] private Deck deck;
    [SerializeField] private Hand hand;
    [SerializeField] private PlayZone playZone;
    [SerializeField] private Discard discard;

    [SerializeField] private List<CardData> StartingCards = new List<CardData>();

    private GameManager gameManager;


    public void Setup(GameManager manager)
    {
        RegisterZone(deck);
        RegisterZone(hand);
        RegisterZone(playZone);
        RegisterZone(discard);

        gameManager = manager;
        gameManager.OnGameStarted += OnGameStarted;

        CardEvents.OnCardPlayed += OnCardPlayed;
    }

    private void OnGameStarted()
    {
        gameManager.OnGameStarted -= OnGameStarted;

        List<CardInstance> cards = StartingCards.Select(cd => new CardInstance(cd.card)).ToList();
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

    private void OnCardPlayed(CardInstance instance)
    {
        Debug.Log("Played");
        if(gameManager.PlayerStatus.Resources >= instance.Card.Cost)
        {
            gameManager.PlayerStatus.Resources -= instance.Card.Cost;

            CardZone from = cardLookup[instance];
            instance.Card.OnCardPlayed(this);
            MoveCard(instance, from, discard);
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

        cardLookup[card] = to;
        CardEvents.OnCardMoved(card, from, to);
    }
}

public static class CardEvents
{
    public static Action<CardInstance, CardZone, CardZone> OnCardMoved = delegate { };

    public static Action<CardInstance> OnCardPlayed = delegate { };
}