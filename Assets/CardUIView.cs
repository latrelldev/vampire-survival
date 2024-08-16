using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUIView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private TextMeshProUGUI Cost;
    [SerializeField] private TextMeshProUGUI Description;

    [SerializeField] private Image image;

    public void Initialize(CardInstance card)
    {
        cardName.text = card.Card.Name;
        Cost.text = card.Card.Cost.ToString();
        Description.text = card.Card.Description.ToString();
    }
}
