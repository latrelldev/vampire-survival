using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionLider : MonoBehaviour
{
    [SerializeField] private int speedBuff;
    [SerializeField] private int powerBuff;

    private List<Player> buffedPlayers = new List<Player>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trying to buff");
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null && !buffedPlayers.Contains(player))
        {
            Debug.Log("buffing");
            buffedPlayers.Add(player);
            player.SpeedModifier += speedBuff;
            player.PowerModifier += powerBuff;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Trying to debuff");
        var player = collision.gameObject.GetComponent<Player>();
        if(player != null && buffedPlayers.Contains(player))
        {
            Debug.Log("Debuff");
            buffedPlayers.Remove(player);
            player.SpeedModifier -= speedBuff;
            player.PowerModifier -= powerBuff;
        }
    }
}
