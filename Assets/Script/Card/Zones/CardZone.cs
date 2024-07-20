using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CardZone
{
    public IReadOnlyList<CardInstance> Cards => cards;
    [SerializeField] protected List<CardInstance> cards = new List<CardInstance>();

    public event Action<CardInstance> OnCardAdded = delegate { };
    public event Action<CardInstance> OnCardRemoved = delegate { };

    public void AddCard(CardInstance card)
    {
        cards.Add(card);
        //OnCardAdded(card);
    }

    public void RemoveCard(CardInstance card)
    {
        cards.Remove(card);
    }
}