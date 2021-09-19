using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
using SFB;
using UnityEngine.EventSystems;

public class HandController : MonoBehaviour, IDragHandler,IBeginDragHandler,IEndDragHandler
{
    Vector2 mousePos;
    private float  deltaX, deltaY;
    public bool SettingPos;

    [SerializeField]
    private OnImportImagesPressed onImportImagesPressed;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private RectTransform rectTransform;
    private void Start()
    {
        if (canvas == null)
        {
            canvas = GetComponentInParent<Canvas>();
        }
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Vector2 position = rectTransform.anchoredPosition;
        int roundX = Mathf.RoundToInt(position.x / 10) * 10;
        int roundY = Mathf.RoundToInt(position.y / 10) * 10;
        position = new Vector2(roundX,roundY);
        rectTransform.anchoredPosition = position;
        MainSpriteController.instance.UpdateCurrentFrameHandData();
    }
}
