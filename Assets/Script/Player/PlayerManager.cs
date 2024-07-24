using Polarith.AI.Move;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager: MonoBehaviour
{
    private bool playerAlive;
    public List<Player> Players = new List<Player>();
    [SerializeField] private AIMEnvironment playerEnv;

    public void Setup()
    {
        Players = GetComponentsInChildren<Player>().ToList();
        playerAlive = Players.Count > 0;
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
        playerAlive = true;
        playerEnv.UpdateLayerGameObjects();
    }

    public void RemovePlayer(Player player)
    {
        Players.Remove(player);
        if (Players.Count == 0 && playerAlive == true)
        {
            playerAlive = false;
            GameOver();
        }
    }

    public void GameOver()
    {
        if (Players.Count == 0)
        {
            Debug.Log("GameOver");
            SceneManager.LoadScene("GameOver");
        }
    }

}

