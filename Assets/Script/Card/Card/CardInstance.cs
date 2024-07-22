using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CardInstance
{
    public CardInstance(int id)
    {
        Id = id;
    }

    public int Id { get; }
}
