using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardDrawer : MonoBehaviour
{
    [SerializeField] private CardView cardView;

    [SerializeField] private TextMeshPro energyText;
    [SerializeField] private TextMeshPro nameText;
    [SerializeField] private TextMeshPro effectText;
    [SerializeField] private SpriteRenderer cardImage;


    private void Start()
    {
        energyText.text = cardView.Card.Card.Cost.ToString();
        nameText.text = cardView.Card.Card.Name;
        cardImage.sprite = cardView.Card.Card.Sprite;
    }
}
