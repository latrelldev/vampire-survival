using Polarith.AI.Move;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager: MonoBehaviour
{
    public List<Player> Players = new List<Player>();
    [SerializeField] private AIMEnvironment playerEnv;

    private void Awake()
    {
        Players = GetComponentsInChildren<Player>().ToList();
        foreach (Player player in Players)
        {
            player.Setup(this);
        }
    }

    public void CreatePlayer(Player playerPrefab)
    {
        Player player = Instantiate(playerPrefab, transform);
        Players.Add(player);
        player.Setup(this);
        playerEnv.UpdateLayerGameObjects();
    }

    public void RemovePlayer(Player player)
    {
        Players.Remove(player);
    }
}

