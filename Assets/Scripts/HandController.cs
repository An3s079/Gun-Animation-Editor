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
    public bool SettingPosz; 

    [SerializeField]
    private OnImportImagesPressed onImportImagesPressed;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private RectTransform rectTransform;

    [SerializeField]
    private KeyCode up = KeyCode.None;
    [SerializeField]
    private KeyCode down = KeyCode.None;
    [SerializeField]
    private KeyCode left = KeyCode.None;
    [SerializeField]
    private KeyCode right = KeyCode.None;
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
    void Update()
    {
        if (Input.GetKeyDown(up))
        {
            Vector2 pos = rectTransform.anchoredPosition;
            pos.y += StaticRefrences.zoomScale;
            int roundX = Mathf.RoundToInt(pos.x / StaticRefrences.zoomScale) * StaticRefrences.zoomScale;
            int roundY = Mathf.RoundToInt(pos.y / StaticRefrences.zoomScale) * StaticRefrences.zoomScale;
            pos = new Vector2(roundX, roundY);
            rectTransform.anchoredPosition = pos;
        }
        if (Input.GetKeyDown(down))
        {
            Vector2 pos = rectTransform.anchoredPosition;
            pos.y -= StaticRefrences.zoomScale;
            int roundX = Mathf.RoundToInt(pos.x / StaticRefrences.zoomScale) * StaticRefrences.zoomScale;
            int roundY = Mathf.RoundToInt(pos.y / StaticRefrences.zoomScale) * StaticRefrences.zoomScale;
            pos = new Vector2(roundX, roundY);
            rectTransform.anchoredPosition = pos;
        }
        if (Input.GetKeyDown(left))
        {
            Vector2 pos = rectTransform.anchoredPosition;
            pos.x -= StaticRefrences.zoomScale;
            int roundX = Mathf.RoundToInt(pos.x / StaticRefrences.zoomScale) * StaticRefrences.zoomScale;
            int roundY = Mathf.RoundToInt(pos.y / StaticRefrences.zoomScale) * StaticRefrences.zoomScale;
            pos = new Vector2(roundX, roundY);
            rectTransform.anchoredPosition = pos;
        }
        if (Input.GetKeyDown(right))
        {
            Vector2 pos = rectTransform.anchoredPosition;
            pos.x += StaticRefrences.zoomScale;
            int roundX = Mathf.RoundToInt(pos.x / StaticRefrences.zoomScale) * StaticRefrences.zoomScale;
            int roundY = Mathf.RoundToInt(pos.y / StaticRefrences.zoomScale) * StaticRefrences.zoomScale;
            pos = new Vector2(roundX, roundY);
            rectTransform.anchoredPosition = pos;
        }

    }
    //used drag the hands and anchor around. reason it updates sprite when dragged is to make it follow the anchor.
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        //Vector2 position = rectTransform.anchoredPosition;
        //int roundX = Mathf.RoundToInt(position.x / StaticRefrences.zoomScale) * StaticRefrences.zoomScale;
        //int roundY = Mathf.RoundToInt(position.y / StaticRefrences.zoomScale) * StaticRefrences.zoomScale;
        //position = new Vector2(roundX, roundY);
        //rectTransform.anchoredPosition = position;
        MainSpriteController.instance.UpdateCurrentFrameHandData();
        MainSpriteController.instance.UpdateSprite(true);
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Vector2 position = rectTransform.anchoredPosition;
        int roundX = Mathf.RoundToInt(position.x / StaticRefrences.zoomScale) * StaticRefrences.zoomScale;
        int roundY = Mathf.RoundToInt(position.y / StaticRefrences.zoomScale) * StaticRefrences.zoomScale;
        position = new Vector2(roundX,roundY);
        rectTransform.anchoredPosition = position;
        MainSpriteController.instance.UpdateCurrentFrameHandData();
        MainSpriteController.instance.UpdateSprite(true);
    }
}
