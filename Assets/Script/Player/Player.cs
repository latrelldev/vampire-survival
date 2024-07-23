using Polarith.AI.Move;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameManager gameManager;
    private PlayerManager playerManager;

    public int PowerModifier;
    public int SpeedModifier;
    public int LifeModifier;

    public void Setup(PlayerManager manager)
    {
        playerManager = manager;
    }

    private void OnDestroy()
    {
        if (playerManager != null)
        {
            playerManager.RemovePlayer(this);
        }
    }



    public void OnPointerClick(PointerEventData eventData)
    {

    }
}
