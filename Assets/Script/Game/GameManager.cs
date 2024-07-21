using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerManager PlayerManager => playerManager;
    [SerializeField] private PlayerManager playerManager;


    public EnemyManager EnemyManager => enemyManager;
    [SerializeField] private EnemyManager enemyManager;

}

