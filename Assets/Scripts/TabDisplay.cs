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

    public string FilePath;

    public bool JsonHasBeenGenerated;
    public OnImportImagesPressed onImportImagesPressed;
    public JsonGenerator jsonGenerator;
    void Start()
    {
       
    }

    public void OnTabClicked()
    {
        StaticRefrences.Instance.spriteController.SetAnimation(animationInfo);
    }

    void Update()
    {
       
    }

    public void OnXClicked()
    {
        Destroy(gameObject);
    }

}
