using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;


public class MainSpriteController : MonoBehaviour
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
                index = 0;
            }
            else if(index >= currentAnimation.frames.Length)
            {
                index = currentAnimation.frames.Length - 1;
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
            float.TryParse(xOffset.text,out info.offsetX);
            float.TryParse(yOffset.text,out info.offsetY);
            info.isTwoHanded = StaticRefrences.Instance.IsTwoHanded.isOn;
        }
    }
    public void UpdateSprite(bool UpdateInputLabels)
    {
        if (currentAnimation != null && currentFrame!= null)
        {
            mainSprite.sprite = currentAnimationInfo.frames[index].sprite;
            if (UpdateInputLabels)
            {
                xOffset.text = currentFrame.offsetX.ToString("F4", culture); 
                yOffset.text = currentFrame.offsetY.ToString("F4", culture); 
            }
            
            mainSprite.SetNativeSize();
            Vector2 anchoredPos = mainSprite.rectTransform.anchoredPosition;
            
            Vector2 pos = new Vector2(-mainSprite.sprite.rect.width / 2 * StaticRefrences.zoomScale, -mainSprite.sprite.rect.height / 2 * StaticRefrences.zoomScale);
            
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

            if (frameCounter != null)
            {
                frameCounter.text = (index + 1).ToString();
            }
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
