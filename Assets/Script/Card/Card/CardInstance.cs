using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CardInstance
{
    public CardInstance(Card card)
    {
        Card = card;
        Id = card.Id;
    }

    public Card Card { get; set; }
    public string Id { get; }
}
