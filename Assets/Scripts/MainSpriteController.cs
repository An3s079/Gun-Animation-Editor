using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSpriteController : MonoBehaviour
{
    public Image mainSprite;
    private GaeAnimationInfo currentAnimationInfo;
    private int index = 0;

    public void SetAnimation(GaeAnimationInfo animation)
    {
        currentAnimationInfo = animation;
        index = 0;
        Debug.Log("animation set");
        mainSprite.sprite = currentAnimationInfo.frames[index].sprite;
        mainSprite.SetNativeSize();
    }
    public void NextFrame()
    {
        if (currentAnimationInfo!= null)
        {
            index++;
            if (index>=currentAnimationInfo.frames.Length)
            {
                index = currentAnimationInfo.frames.Length - 1;
            }
            mainSprite.sprite = currentAnimationInfo.frames[index].sprite;
        }
    }
    public void PreviousFrame()
    {
        if (currentAnimationInfo != null)
        {
            index--;
            if (index <= 0)
            {
                index = 0;
            }
            mainSprite.sprite = currentAnimationInfo.frames[index].sprite;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
