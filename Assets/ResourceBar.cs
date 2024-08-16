using UnityEngine;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour
{
    [SerializeField] private PlayerStatus status;
    [SerializeField] private Image counter;

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
        var scale = counter.transform.localScale;
        scale.y = Mathf.Clamp(lastValue, 0, 10);
        counter.transform.localScale = scale;
    }
}