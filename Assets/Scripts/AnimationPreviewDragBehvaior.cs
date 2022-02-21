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
    [SerializeField]
    private AnimationPreviewSpriteController animationPreview;

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
        Vector2 positionDiff = (rectTransform.anchoredPosition - GungeoneerTransform.anchoredPosition) / StaticRefrences.zoomScale;
        int roundX = Mathf.RoundToInt(positionDiff.x) * StaticRefrences.zoomScale;
        int roundY = Mathf.RoundToInt(positionDiff.y) * StaticRefrences.zoomScale;
        rectTransform.anchoredPosition = new Vector2(roundX, roundY) + GungeoneerTransform.anchoredPosition;
        animationPreview.UpdateOffsets();
        animationPreview.UpdateSprite();
        //incosnistent use of static refrences and local refrences, why am i doing this?
        StaticRefrences.Instance.spriteController.UpdateSprite(true);
    }
}
   

