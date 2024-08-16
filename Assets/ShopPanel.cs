using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private float panelSpeed = 500f;
    [SerializeField] private Toggle shopToggle;

    private RectTransform rect;
    private Vector3 hiddenPos = Vector3.zero;
    private Vector3 visiblePos = Vector3.zero;
    private Vector3 offset;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        hiddenPos = rect.anchoredPosition;
        visiblePos = new Vector3(320, hiddenPos.y, hiddenPos.z);

        shopToggle.onValueChanged.AddListener(Toggle);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        rect.DOKill();
        offset = eventData.position - rect.anchoredPosition;
        offset.z = 0;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var pos = (Vector3)eventData.position - offset;
        pos.z = transform.position.z; //maintain current Z, as that is setup by anchor or custom
        pos.y = rect.anchoredPosition.y;
        pos.x = Mathf.Clamp(pos.x, hiddenPos.x, visiblePos.x);
        rect.anchoredPosition = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var closeDist = Vector3.Distance(rect.anchoredPosition, visiblePos);
        var openDist = Vector3.Distance(rect.anchoredPosition, hiddenPos);

        if (closeDist > openDist)
        {
            shopToggle.SetIsOnWithoutNotify(false);
            Close();
        }
        else
        {
            shopToggle.SetIsOnWithoutNotify(true);
            Open();
        }
    }

    public void Open()
    {
        rect.DOKill();
        rect.DOAnchorPos(visiblePos, panelSpeed).SetSpeedBased();
    }


    public void Close()
    {
        rect.DOKill();
        rect.DOAnchorPos(hiddenPos, panelSpeed).SetSpeedBased();
    }

    public void Toggle(bool on)
    {
        if (on)
        {
            Open();
        }
        else
        {
            Close();
        }
    }
}
