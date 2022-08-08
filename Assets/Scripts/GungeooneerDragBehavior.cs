using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
using SFB;
using UnityEngine.EventSystems;
//theres wayyyyyyy too  many drag behaviors in this project. bad design probably:\
public class GungeooneerDragBehavior : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

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
        int roundX = Mathf.RoundToInt(position.x / StaticRefrences.zoomScale) * StaticRefrences.zoomScale;
        int roundY = Mathf.RoundToInt(position.y / StaticRefrences.zoomScale) * StaticRefrences.zoomScale;
        position = new Vector2(roundX, roundY);
        rectTransform.anchoredPosition = position;
        StaticRefrences.Instance.previewController.UpdateSprite();
    }
}
