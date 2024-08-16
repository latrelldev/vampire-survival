using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardDrawer : MonoBehaviour
{
    [SerializeField] private CardView cardView;

    [SerializeField] private TMP_Text energyText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text effectText;
    [SerializeField] private SpriteRenderer cardImage;


    private void Start()
    {
        energyText.text = cardView.Card.Card.Cost.ToString();
        nameText.text = cardView.Card.Card.Name;
        effectText.text = cardView.Card.Card.Description;

        cardImage.sprite = cardView.Card.Card.Sprite;
    }
}
