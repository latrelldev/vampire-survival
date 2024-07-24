using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerManager PlayerManager => playerManager;
    [SerializeField] private PlayerManager playerManager;


    public EnemyManager EnemyManager => enemyManager;
    [SerializeField] private EnemyManager enemyManager;

    public CardsManager Cards => cards;
    [SerializeField] private CardsManager cards;

    public CardViewController CardViewController => cardViewController;
    [SerializeField] private CardViewController cardViewController;

    public event Action OnGameStarted = delegate { };

    private void Start()
    {
        playerManager.Setup();
        enemyManager.Setup(this);
        cards.Setup(this);
        cardViewController.Setup();

        OnGameStarted?.Invoke();
    }
}

