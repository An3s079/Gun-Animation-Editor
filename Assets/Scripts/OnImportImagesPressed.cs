using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
using SFB;
using System;
using Newtonsoft.Json;


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

    public void ImportSingleImages()
    {
        Image.sprite = DefaultTexture;
        paths = StandaloneFileBrowser.OpenFilePanel("gotta make them guns ey?", "", "png", true);

        if (paths.Length > 0)
        {
            for (int i = 0; i < paths.Length; i++)
            {
                FileInfo info = new FileInfo(paths[i]);

                GaeAnimationInfo animationInfo = new GaeAnimationInfo();

                animationInfo.frames = new FrameInfo[1];
                
                animationInfo.frames[0] = LoadSingleFrame(info.FullName);

                animationInfo.animationName = Path.GetFileName(info.FullName);

                RectTransform content = StaticRefrences.Instance.TabArea.GetComponent<ScrollRect>().content;
                RectTransform tabArea = StaticRefrences.Instance.TabArea.GetComponent<RectTransform>();

                var newButton = Instantiate(ImgTabPrefab, content, false);
                newButton.name = animationInfo.animationName + " button";
                RectTransform buttonRectTransform = newButton.GetComponent<RectTransform>();
                buttonRectTransform.localPosition = new Vector3(0, 0, 0);
                float ratio = tabArea.rect.width / buttonRectTransform.rect.width;
                newButton.transform.localScale = new Vector3(ratio, ratio, 1f);

                TabDisplay newButtonTabDisplay = newButton.GetComponent<TabDisplay>();
                newButtonTabDisplay.TMPtext.text = animationInfo.animationName;
                newButtonTabDisplay.animationInfo = animationInfo;

                StaticRefrences.Instance.spriteController.SetAnimation(animationInfo);
            }
            
         
            
        }
    }
    
   public void ImportAnimation()
   {
        Image.sprite = DefaultTexture;
        paths = StandaloneFileBrowser.OpenFilePanel("gotta make them guns ey?", "", "png", false);

        if (paths.Length>0)
        {
            FileInfo info = new FileInfo(paths[0]);
            string fileName = Path.GetFileNameWithoutExtension(paths[0]);

            string animationName = RemoveTrailingDigits(fileName);
            FileInfo[] files = info.Directory.GetFiles(animationName + "*.png");
           
            GaeAnimationInfo animationInfo = new GaeAnimationInfo();

            animationInfo.frames = new FrameInfo[files.Length];
            for (int i = 0; i < animationInfo.frames.Length; i++)
            {
                animationInfo.frames[i] = LoadSingleFrame(files[i].FullName);
            }
            animationInfo.animationName = RemoveTrailingDigits(Path.GetFileNameWithoutExtension(files[0].FullName));
            Debug.Log(animationInfo.animationName);
  
            RectTransform content = StaticRefrences.Instance.TabArea.GetComponent<ScrollRect>().content;
            RectTransform tabArea = StaticRefrences.Instance.TabArea.GetComponent<RectTransform>();

            var newButton = Instantiate(ImgTabPrefab, content,false);
            newButton.name = animationInfo.animationName+" button";
            RectTransform buttonRectTransform = newButton.GetComponent<RectTransform>();
            buttonRectTransform.localPosition = new Vector3(0,0,0);
            float ratio = tabArea.rect.width / buttonRectTransform.rect.width;
            newButton.transform.localScale = new Vector3(ratio, ratio, 1f);
           
            TabDisplay newButtonTabDisplay = newButton.GetComponent<TabDisplay>();
            newButtonTabDisplay.TMPtext.text = animationInfo.animationName;
            newButtonTabDisplay.animationInfo = animationInfo;

            StaticRefrences.Instance.spriteController.SetAnimation(animationInfo);
        }


    }
    GameObject newbutton = null;
    private string RemoveTrailingDigits(string path)
    {

        int firstNumberIndex = path.Length-1;
        while (firstNumberIndex >= 0 && char.IsDigit(path[firstNumberIndex]))
        {
            firstNumberIndex--;
        }
        return path.Substring(0, firstNumberIndex);
    }
    private FrameInfo LoadSingleFrame(string path)
    {
        path = System.IO.Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
       
        string pngPath = path + ".png";
        string jsonPath = path + ".json";
        if (!File.Exists(pngPath))
        {
            return null;
        }
        byte[] bytes = File.ReadAllBytes(pngPath);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(bytes);
        tex.filterMode = FilterMode.Point;
        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 1);
        if (File.Exists(jsonPath))
        {
            try
            {
                string jsonInfo = File.ReadAllText(jsonPath);
                FrameJsonInfo frameJsonInfo = JsonConvert.DeserializeObject<FrameJsonInfo>(jsonInfo);
                frameJsonInfo.attachPoints[0] = (frameJsonInfo.attachPoints[0] as Newtonsoft.Json.Linq.JObject).ToObject<ArrayTypeUnkownAndSize>();
                for (int i = 1; i < frameJsonInfo.attachPoints.Length; i++)
                {
                    frameJsonInfo.attachPoints[i] = (frameJsonInfo.attachPoints[i] as Newtonsoft.Json.Linq.JObject).ToObject<AttachPoint>();
                }
                if (frameJsonInfo.attachPoints.Length == 3)
                {
                    AttachPoint hand1 = frameJsonInfo.attachPoints[1] as AttachPoint;
                    float convertedX1 = hand1.position.x * 16;
                    float convertedY1 = hand1.position.y * 16;
                    return new FrameInfo(tex, sprite, convertedX1, convertedY1, 0, 0, frameJsonInfo.x, frameJsonInfo.x, false, path);
                }
                if (frameJsonInfo.attachPoints.Length == 4)
                {
                    AttachPoint hand1 = frameJsonInfo.attachPoints[1] as AttachPoint;
                    AttachPoint hand2 = frameJsonInfo.attachPoints[2] as AttachPoint;
                    float convertedX1 = hand1.position.x * 16;
                    float convertedY1 = hand1.position.y * 16;
                    float convertedX2 = hand2.position.x * 16;
                    float convertedY2 = hand2.position.y * 16;
                    return new FrameInfo(tex, sprite, convertedX1, convertedY1, convertedX2, convertedY2, frameJsonInfo.x, frameJsonInfo.x, true,path);
                }
            }
            catch (Exception)
            {
                throw new Exception("json seems to be invalid! or i dont know how to read jsons!");
            }
        }
        return new FrameInfo(tex, sprite , path);
    }



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
        Vector3[] corners = new Vector3[4];
        Image.GetComponent<RectTransform>().GetWorldCorners(corners);
        readPoint = corners[0];  
    }
}

