using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainSpriteController : MonoBehaviour
{
    public Image mainSprite;
    private GaeAnimationInfo currentAnimationInfo;
    private int index = 0;

    public static MainSpriteController instance;

    
    public void SetAnimation(GaeAnimationInfo animation)
    {
        currentAnimationInfo = animation;
        index = 0;
        UpdateSprite();
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
            
            UpdateSprite();
        }       
    }
    public void UpdateCurrentFrameHandData()
    {
        if (currentAnimation != null && currentFrame != null)
        {
            Image mainSprite = StaticRefrences.Instance.MainSprite;
            RawImage hand1 = StaticRefrences.Instance.handIMG;
            RawImage hand2 = StaticRefrences.Instance.handIMG;
            FrameInfo info = currentFrame;
            info.hand1PositionX = (hand1.rectTransform.anchoredPosition.x - mainSprite.rectTransform.anchoredPosition.x) / 10;
            info.hand1PositionY = (hand1.rectTransform.anchoredPosition.y - mainSprite.rectTransform.anchoredPosition.y) / 10;
            info.hand2PositionX = (hand2.rectTransform.anchoredPosition.x - mainSprite.rectTransform.anchoredPosition.x) / 10;
            info.hand2PositionY = (hand2.rectTransform.anchoredPosition.y - mainSprite.rectTransform.anchoredPosition.y) / 10;
        }
    }
    private void UpdateSprite()
    {
        if (currentAnimation != null && currentFrame!= null)
        {
            mainSprite.sprite = currentAnimationInfo.frames[index].sprite;
            mainSprite.SetNativeSize();
            Vector2 anchoredPos = mainSprite.rectTransform.anchoredPosition;
            Vector2 pos = new Vector2(-mainSprite.sprite.rect.width / 2 * 10, -mainSprite.sprite.rect.height / 2 * 10);
            pos = new Vector2(Mathf.Round(pos.x / 10f) * 10, Mathf.Round(pos.y / 10f) * 10);
            mainSprite.rectTransform.anchoredPosition = pos;
            Vector2 handpos1 = new Vector2(pos.x + currentFrame.hand1PositionX * 10, pos.y + currentFrame.hand1PositionY * 10);
            Vector2 handpos2 = new Vector2(pos.x + currentFrame.hand2PositionX * 10, pos.y + currentFrame.hand2PositionY * 10);
            StaticRefrences.Instance.handIMG.rectTransform.anchoredPosition = handpos1;
            StaticRefrences.Instance.handIMG2.rectTransform.anchoredPosition = handpos2;
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
