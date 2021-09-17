using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
using SFB;
using System;


public class OnImportImagesPressed : MonoBehaviour
{

    public static Vector2 readPoint;
    public GameObject ImgTabPrefab;
    public GameObject TabArea;
    public GameObject scrollthing;
    public Toggle IsTwoHanded;
    public RawImage handIMG;
    public RawImage handIMG2;
    public Image Image;
    public RawImage GreyBackgroundThangIdk;

    public Sprite DefaultTexture;
    string[] paths;
    HandController handController1;
    HandController handController2;
    public bool ControllingHand2 = false;
    public TabDisplay SelectedTab;

    void Start()
    {
        Image.sprite = DefaultTexture;
        Image.SetNativeSize();      
              //  Image.transform.localScale = new Vector3(20,20, 1);   
    }

   public void ImportImages()
   {
        Image.sprite = DefaultTexture;
        paths = StandaloneFileBrowser.OpenFilePanel("gotta make them guns ey?", "", "png", false);

        if (paths.Length>0)
        {
            FileInfo info = new FileInfo(paths[0]);
            string fileName = Path.GetFileNameWithoutExtension(paths[0]);

            string trimmedNum = fileName.Substring(0,fileName.Length - 4);
            FileInfo[] files = info.Directory.GetFiles(trimmedNum + "*.png");

            GaeAnimationInfo animationInfo = new GaeAnimationInfo();
            animationInfo.frames = new FrameInfo[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                byte[] bytes = File.ReadAllBytes(files[i].FullName);
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(bytes);
                tex.filterMode = FilterMode.Point;
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 1);
                animationInfo.frames[i] = new FrameInfo(tex,sprite);

                
            }
            string[] sections = fileName.Split('_');

            animationInfo.fullAnimationName = Path.GetFileNameWithoutExtension(files[0].FullName);
            animationInfo.animationType = sections[1];
            animationInfo.animationName = sections[0];

            string end = sections[sections.Length-1];
            RectTransform content = StaticRefrences.Instance.TabArea.GetComponent<ScrollRect>().content;
            RectTransform tabArea = StaticRefrences.Instance.TabArea.GetComponent<RectTransform>();
            var newButton = Instantiate(ImgTabPrefab, content,false);
            newButton.name = animationInfo.animationName+"_" + animationInfo.animationType+" button";
            RectTransform buttonRectTransform = newButton.GetComponent<RectTransform>();
            buttonRectTransform.localPosition = new Vector3(0,0,0);
            float ratio = tabArea.rect.width / buttonRectTransform.rect.width;
            newButton.transform.localScale = new Vector3(ratio, ratio, 1f);
           
            TabDisplay newButtonTabDisplay = newButton.GetComponent<TabDisplay>();
            newButtonTabDisplay.TMPtext.text = animationInfo.animationName+"_"+animationInfo.animationType;
            newButtonTabDisplay.animationInfo = animationInfo;
        }


    }
    GameObject newbutton = null;

    private void OnDrawGizmos()
    {
        Vector3[] corners = new Vector3[4];
        Image.GetComponent<RectTransform>().GetWorldCorners(corners);

        var center = (corners[0] + corners[2]) / 2;
        var size = corners[2] - corners[0];
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
    void Update()
    {

        GreyBackgroundThangIdk.rectTransform.sizeDelta = Image.rectTransform.sizeDelta;
        GreyBackgroundThangIdk.transform.localScale = Image.transform.localScale;
        GreyBackgroundThangIdk.transform.position = Image.transform.position;
        Vector3[] corners = new Vector3[4];
        Image.GetComponent<RectTransform>().GetWorldCorners(corners);
        readPoint = corners[0];

       
    }


}

