using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovePanel : MonoBehaviour, IDragHandler
{
    public RectTransform curruntRect;

    public void OnDrag(PointerEventData eventData)
    {
        curruntRect.anchoredPosition += eventData.delta;
    }

    void Awake()
    {
        curruntRect = GetComponent<RectTransform>();
    }
}
