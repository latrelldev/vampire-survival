using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player Player => player;
    [SerializeField] private Player player;

    public EnemyManager EnemyManager => enemyManager;
    [SerializeField] private EnemyManager enemyManager;

}

