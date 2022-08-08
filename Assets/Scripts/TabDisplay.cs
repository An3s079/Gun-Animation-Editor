using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
using TMPro;

public class TabDisplay : MonoBehaviour
{
    
    public TextMeshProUGUI TMPtext;
    public RawImage HandImage;
    public RawImage HandImage2;
    public Toggle IsTwoHanded;
    public GaeAnimationInfo animationInfo;
    public Texture2D texture;
    public Image buttonImage;

    public string FilePath;

    public bool JsonHasBeenGenerated;
    public OnImportImagesPressed onImportImagesPressed;
    public JsonGenerator jsonGenerator;
    //literally just stores animation infos
    void Start()
    {
        if (buttonImage!= null)
        {
            if (animationInfo?.frames?[0]?.texture!= null)
            {
                texture = animationInfo.frames[0].texture;
                buttonImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 1);
                buttonImage.preserveAspect = true;
            }
        }
    }

    public void OnTabClicked()
    {
        StaticRefrences.Instance.spriteController.SetAnimation(animationInfo);
        StaticRefrences.Instance.previewController.UpdateSprite();
    }

    void Update()
    {
       
    }

    public void OnXClicked()
    {
        if (MainSpriteController.instance.currentAnimation == animationInfo)
        {
            MainSpriteController.instance.SetAnimation(null);
        }
        Destroy(gameObject);
    }

}
