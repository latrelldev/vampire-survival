using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Card
{
    [Header("Details")]
    [SerializeField] private string id;
    public string Id => id;
    public string Name => name;
    [SerializeField] private string name;
    public Sprite Sprite => sprite;
    [SerializeField] private Sprite sprite;


    [Header("Values")]
    [SerializeField] private int cost;
    public int Cost => cost;
    public int UpgradeCost => upgradeCost;
    [SerializeField] private int upgradeCost;


    public abstract void OnCardPlayed();
}


public class MinionSpawnCard : Card
{
    public Player minionPrefab;

    public override void OnCardPlayed()
    {
        Debug.Log("Spawn");
    }
}

public class CardPlanejamento : Card
{
    public int drawCount;

    public override void OnCardPlayed()
    {
        Debug.Log("Draw 2");
    }
}

public class CardBateria: Card
{
    public int energyGain;

    public override void OnCardPlayed()
    {
        Debug.Log("Received energy " + energyGain);
    }
}