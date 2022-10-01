using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
using SFB;
using UnityEngine.EventSystems;

public class AnimationPreviewDragBehvaior : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Vector2 mousePos;
    private float deltaX, deltaY;
    public bool SettingPosz;

    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private RectTransform GungeoneerTransform;
    Vector2 curPos = Vector2.zero;

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
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor / StaticRefrences.zoomScale;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        curPos = rectTransform.anchoredPosition;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        int roundX = Mathf.RoundToInt(rectTransform.anchoredPosition.x);
        int roundY = Mathf.RoundToInt(rectTransform.anchoredPosition.y);
        rectTransform.anchoredPosition = new Vector2(roundX, roundY);
        StaticRefrences.Instance.previewController.UpdateOffsets();
        StaticRefrences.Instance.previewController.UpdateSprite();
        //incosnistent use of static refrences and local refrences, why am i doing this?
        StaticRefrences.Instance.spriteController.UpdateSprite(true);
    }
}
   

