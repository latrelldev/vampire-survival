using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceCounter : MonoBehaviour
{
    [SerializeField] private PlayerStatus status;
    [SerializeField] private TextMeshProUGUI counter;

    private int lastValue = int.MinValue;
    void Update()
    {
        if (status.Resources != lastValue)
        {
            lastValue = status.Resources;
            RefreshValue();
        }
    }

    private void RefreshValue()
    {
        counter.text = status.Resources.ToString();
    }
}
