using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;
using UnityEngine.EventSystems;

public class MainSpriteController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Image mainSprite;
    private GaeAnimationInfo currentAnimationInfo;
    private int index = 0;

    public static MainSpriteController instance;
    [SerializeField]
    private TMP_InputField xOffset;

    [SerializeField]
    private TMP_InputField yOffset;

    [SerializeField]
    private TextMeshProUGUI frameCounter;

    private static readonly CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

    public void SetAnimation(GaeAnimationInfo animation)
    {
        currentAnimationInfo = animation;
        index = 0;
        UpdateSprite(true);
        if (animation == null)
        {
            mainSprite.sprite = null;
        }
    }
    public void NextFrame()
    {
        if (currentAnimationInfo!= null)
        {
            MoveIndex(1);
        }
    }
    public void PreviousFrame()
    {

        if (currentAnimationInfo != null)
        {
            MoveIndex(-1);
        }
    }
    private void MoveIndex(int i)
    {
        UpdateCurrentFrameHandData();
        index += i;
        if (currentAnimation != null)
        {
            

            if (index< 0)
            {
                index = currentAnimation.frames.Length-1;
            }
            else if(index >= currentAnimation.frames.Length)
            {
                index = 0;
            }
            
            UpdateSprite(true);
        }       
    }
    public void UpdateCurrentFrameHandData()
    {
        if (currentAnimation != null && currentFrame != null)
        {
            Image mainSprite = StaticRefrences.Instance.MainSprite;
            RawImage hand1 = StaticRefrences.Instance.handIMG;
            RawImage hand2 = StaticRefrences.Instance.handIMG2;
            FrameInfo info = currentFrame;
            info.hand1PositionX = (hand1.rectTransform.anchoredPosition.x - mainSprite.rectTransform.anchoredPosition.x) / StaticRefrences.zoomScale;
            info.hand1PositionY = (hand1.rectTransform.anchoredPosition.y - mainSprite.rectTransform.anchoredPosition.y) / StaticRefrences.zoomScale;
            info.hand2PositionX = (hand2.rectTransform.anchoredPosition.x - mainSprite.rectTransform.anchoredPosition.x) / StaticRefrences.zoomScale;
            info.hand2PositionY = (hand2.rectTransform.anchoredPosition.y - mainSprite.rectTransform.anchoredPosition.y) / StaticRefrences.zoomScale;
            info.muzzleflashPositionX = (MuzzleFlashObject.anchoredPosition.x - mainSprite.rectTransform.anchoredPosition.x) / StaticRefrences.zoomScale;
            info.muzzleflashPositionY = (MuzzleFlashObject.anchoredPosition.y - mainSprite.rectTransform.anchoredPosition.y) / StaticRefrences.zoomScale;
            float.TryParse(xOffset.text,out info.offsetX);
            float.TryParse(yOffset.text,out info.offsetY);
            info.isTwoHanded = StaticRefrences.Instance.IsTwoHanded.isOn;
            
        }
    }
    [SerializeField]
    private GameObject barreGenButton;
    [SerializeField]
    private GameObject barrelInfoButton;
    [SerializeField]
    private RectTransform MuzzleFlashObject;
    [SerializeField]
    private GameObject MuzzleFlashSign;
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private Canvas canvas;

    private Vector2 posOffset = new Vector2(0, 0);
    public void UpdateSprite(bool UpdateInputLabels = false)
    {

        if (currentAnimation != null && currentFrame!= null)
        {
            
            mainSprite.sprite = currentAnimationInfo.frames[index].sprite;

            mainSprite.SetNativeSize();

            Vector2 anchoredPos = mainSprite.rectTransform.anchoredPosition;
            
            Vector2 pos = new Vector2(-mainSprite.sprite.rect.width / 2 * StaticRefrences.zoomScale,-mainSprite.sprite.rect.height / 2 * StaticRefrences.zoomScale) + posOffset;
            
            pos = new Vector2(Mathf.Round(pos.x / StaticRefrences.zoomScale) * StaticRefrences.zoomScale, Mathf.Round(pos.y / StaticRefrences.zoomScale) * StaticRefrences.zoomScale);

            if (StaticRefrences.Instance.IsGungeoneerOn.isOn)
            {
                pos = StaticRefrences.Instance.Gungeoneer.anchoredPosition;
                pos = new Vector2(pos.x + currentFrame.offsetX* StaticRefrences.zoomScale, pos.y + currentFrame.offsetY* StaticRefrences.zoomScale);
            }

            mainSprite.rectTransform.anchoredPosition = pos;
            
            Vector2 handpos1 = new Vector2(pos.x + currentFrame.hand1PositionX * StaticRefrences.zoomScale, pos.y + currentFrame.hand1PositionY * StaticRefrences.zoomScale);
            Vector2 handpos2 = new Vector2(pos.x + currentFrame.hand2PositionX * StaticRefrences.zoomScale, pos.y + currentFrame.hand2PositionY * StaticRefrences.zoomScale);

            StaticRefrences.Instance.handIMG.rectTransform.anchoredPosition = handpos1;
            StaticRefrences.Instance.handIMG2.rectTransform.anchoredPosition = handpos2;
            StaticRefrences.Instance.IsTwoHanded.isOn = currentFrame.isTwoHanded;
            if(barrelInfoButton != null && barreGenButton != null && MuzzleFlashObject != null)
            {
                if (currentFrame.path.Contains("idle_001"))
                {
                    barreGenButton.SetActive(true);
                    barrelInfoButton.SetActive(true);
                    MuzzleFlashObject.gameObject.SetActive(true);
                    MuzzleFlashSign.SetActive(true);
                    int zoomscale = StaticRefrences.zoomScale;
                    Vector2 muzzlepos = new Vector2(pos.x + currentFrame.muzzleflashPositionX * zoomscale, pos.y + currentFrame.muzzleflashPositionY * zoomscale);
                    MuzzleFlashObject.anchoredPosition = muzzlepos;
                }
                else
                {
                    barreGenButton.SetActive(false);
                    barrelInfoButton.SetActive(false);
                    MuzzleFlashObject.gameObject.SetActive(false);
                    MuzzleFlashSign.SetActive(false);
                }
            }
            if (frameCounter != null)
            {
                frameCounter.text = (index + 1).ToString();
            }
            if (UpdateInputLabels)
            {
                //pulls both values before setting the variables because setting the input fields values triggers a "UpdateCurrentFrameHandData" call
                //this is also why this bit of code is right near the end
                string offsetx = currentFrame.offsetX.ToString("F2", culture);
                string offsety = currentFrame.offsetY.ToString("F2", culture);
                xOffset.text = offsetx;
                yOffset.text = offsety;
            }

        }
        else if (barrelInfoButton != null && barreGenButton != null && MuzzleFlashObject != null)
        {
            barreGenButton.SetActive(false);
            barrelInfoButton.SetActive(false);
            MuzzleFlashObject.gameObject.SetActive(false);
            MuzzleFlashSign.SetActive(false);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Middle)
        {
            posOffset += eventData.delta / canvas.scaleFactor;
            UpdateSprite(false);
        }
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {

    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Middle)
        {
          /*  Vector2 position = rectTransform.anchoredPosition;
            int roundX = Mathf.RoundToInt(position.x / StaticRefrences.zoomScale) * StaticRefrences.zoomScale;
            int roundY = Mathf.RoundToInt(position.y / StaticRefrences.zoomScale) * StaticRefrences.zoomScale;
            position = new Vector2(roundX, roundY);
            //-mainSprite.sprite.rect.width / 2 * StaticRefrences.zoomScale,posOffsetY + -mainSprite.sprite.rect.height / 2 * StaticRefrences.zoomScale);

            posOffsetX = roundX + (int)(-mainSprite.sprite.rect.width / 2 * StaticRefrences.zoomScale);
            posOffsetY = roundY + roundX + (int)(-mainSprite.sprite.rect.height / 2 * StaticRefrences.zoomScale);
            rectTransform.anchoredPosition = position;
            MainSpriteController.instance.UpdateCurrentFrameHandData();
            MainSpriteController.instance.UpdateSprite(true);*/
        }
    }

    public FrameInfo currentFrame 
    {
        get
        {
            return currentAnimationInfo.frames[index];
        }
    }
    public GaeAnimationInfo currentAnimation
    {
        get
        {
            return currentAnimationInfo;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        MainSpriteController.instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCurrentFrameHandData();
    }
}
