using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    [SerializeField] private PlayerStatus status;
    [SerializeField] private Image counter;

    private int lastValue = int.MinValue;

    void Update()
    {
        if (status.Energy != lastValue)
        {
            lastValue = status.Energy;
            RefreshValue();
        }
    }

    private void RefreshValue()
    {
        var scale = counter.transform.localScale;
        scale.y = Mathf.Clamp(lastValue, 0, 10);
        counter.transform.localScale = scale;
    }
}
