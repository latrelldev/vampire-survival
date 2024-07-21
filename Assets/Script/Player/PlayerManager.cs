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

    private void Awake()
    {
        Players = GetComponentsInChildren<Player>().ToList();
        foreach (Player player in Players)
        {
            player.Setup(this);
        }
    }
    public void Start()
    {
        if (Players.Count == 0)
        {
            playerAlive = false;
            Debug.Log("JogoNaoComeçou");

        }
        else
        {
            playerAlive = true;
            Debug.Log("JogoComeçou");
        }

        if(Players.Count == 0 && playerAlive == true)
        {
            GameOver();
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

    public void GameOver()
    {
        if (Players.Count == 0)
        {
            SceneManager.LoadScene("GameOver");
            Debug.Log("GameOver");
        }
    }

}

