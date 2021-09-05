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
    public TabDisplay SelectedTab ;
    public JsonGenerator jsonGenerator;

    void Start()
    {
        Image.sprite = DefaultTexture;
        Image.SetNativeSize();      
                Image.transform.localScale = new Vector3(20,20, 1);   
        InvokeRepeating("UpdateEverySecond", 0, 0.1f);
    }

   public void ImportImages()
   {
        Image.sprite = DefaultTexture;
        //Image.SetNativeSize();
         paths = StandaloneFileBrowser.OpenFilePanel("Title", "", "png", true);
        if (paths.Length > 0) {
            StartCoroutine(OutputRoutine(paths));
        }
     
   }

    private IEnumerator OutputRoutine(string[] url) {
    
        foreach(string s in url)
        {       
        Image.sprite = DefaultTexture;
        Image.transform.localScale = new Vector3(20f,20f, 1); 
        var loader = new WWW(s);
        
        yield return loader;
        
        var SpriteToUse = Sprite.Create(loader.texture, new Rect(0.0f, 0.0f, loader.texture.width, loader.texture.height), new Vector2(0.5f, 0.5f), 1);
        Image.sprite = SpriteToUse;
        Image.preserveAspect = true;
        Image.sprite.texture.filterMode = FilterMode.Point;
        Image.SetNativeSize();
        

        handIMG.SetNativeSize();
        
        
        var newButton = Instantiate(ImgTabPrefab, new Vector3(-5.26f, 0), Quaternion.identity, TabArea.transform);
        newButton.name = "Dabutton";
        newButton.GetComponent<RectTransform>().localPosition = new Vector3(newButton.GetComponent<RectTransform>().localPosition.x, newButton.GetComponent<RectTransform>().localPosition.y, 0);
        newButton.transform.localScale = new Vector3(1.658659f, 1.508957f, 0.9996841f);
        string ItemName = s;
        string[] ItenmName2;
        ItemName.Replace("_", " ");
        ItenmName2 = ItemName.Split('\\');
        
        newButton.GetComponent<TabDisplay>().TMPtext.text = ItenmName2[ItenmName2.Length -1];

        newButton.GetComponent<TabDisplay>().Sprite.sprite = SpriteToUse;
        newButton.GetComponent<TabDisplay>().SpriteDisplay = Image;
        newButton.GetComponent<TabDisplay>().BeegSprite = Image.sprite;
        newButton.GetComponent<TabDisplay>().Sprite.preserveAspect = true;
        newButton.GetComponent<TabDisplay>().DefaultTexture = DefaultTexture;
        newButton.GetComponent<TabDisplay>().ScrollThingBitchIdkLol = scrollthing;
        newButton.GetComponent<TabDisplay>().GreyBackgroundThangIdk = GreyBackgroundThangIdk;
        newButton.GetComponent<TabDisplay>().HandImage = handIMG;
        newButton.GetComponent<TabDisplay>().FilePath = s;
        newButton.GetComponent<TabDisplay>().HandImage2 = handIMG2;
        newButton.GetComponent<TabDisplay>().IsTwoHanded = IsTwoHanded;
        newButton.GetComponent<TabDisplay>().onImportImagesPressed = this;
        newButton.GetComponent<TabDisplay>().jsonGenerator = jsonGenerator;
        SelectedTab = newButton.GetComponent<TabDisplay>();
            Vector3[] corners = new Vector3[4];
            Image.GetComponent<RectTransform>().GetWorldCorners(corners);
            handIMG.transform.position = corners[0];
            JsonGenerator.FilePath = s;
            handIMG2.transform.position = corners[0];
            handIMG2.transform.localScale = handIMG.transform.localScale;
            // // // // // // // // float width = (Image.GetComponent<RectTransform> ().offsetMax.x - Image.GetComponent<RectTransform> ().offsetMin.x) * Image.transform.lo;
            // // // // // // // // SizePerPixel =  width / Image.texture.width;
            // // // // // // // // Debug.Log(SizePerPixel + "width"+ width);
        }

        
        

    }
    private void OnDrawGizmos()
    {
    Vector3[] corners = new Vector3[4];
    Image.GetComponent<RectTransform>().GetWorldCorners(corners);

    var center = (corners[0] + corners[2]) / 2;
    var size = corners[2]- corners[0];
    Gizmos.color = new Color(0, 1, 0, 0.5f);
    Gizmos.DrawCube(center, size);
    }
    float startTime = 0f;
    float holdTime = 0.3f;
    void Update()
    {

        GreyBackgroundThangIdk.rectTransform.sizeDelta = Image.rectTransform.sizeDelta;
        GreyBackgroundThangIdk.transform.localScale = Image.transform.localScale;
        GreyBackgroundThangIdk.transform.position = Image.transform.position;
        Vector3[] corners = new Vector3[4];
        Image.GetComponent<RectTransform>().GetWorldCorners(corners);
        readPoint = corners[0];

            if(Input.GetKeyDown(KeyCode.D))
            {startTime = Time.time;
                handIMG.transform.position += transform.right / 5.4f;
                SelectedTab.JsonHasBeenGenerated = false;
            }
            if(Input.GetKeyDown(KeyCode.W))
            {startTime = Time.time;
                handIMG.transform.position += transform.up / 5.4f;
                SelectedTab.JsonHasBeenGenerated = false;
            }
            if(Input.GetKeyDown(KeyCode.S))
            {startTime = Time.time;
                handIMG.transform.position += -transform.up / 5.4f;
                SelectedTab.JsonHasBeenGenerated = false;
            }
            if(Input.GetKeyDown(KeyCode.A))
            {startTime = Time.time;
                handIMG.transform.position += -transform.right / 5.4f;
                SelectedTab.JsonHasBeenGenerated = false;
            }
        
        if(IsTwoHanded.isOn)
		{
			//if (ControllingHand2 == true)
			//{
				if (Input.GetKeyDown(KeyCode.RightArrow))
				{
					startTime = Time.time;
					handIMG2.transform.position += transform.right / 5.4f;
					SelectedTab.JsonHasBeenGenerated = false;
				}
				if (Input.GetKeyDown(KeyCode.UpArrow))
				{
					startTime = Time.time;
					handIMG2.transform.position += transform.up / 5.4f;
					SelectedTab.JsonHasBeenGenerated = false;
				}
				if (Input.GetKeyDown(KeyCode.DownArrow))
				{
					startTime = Time.time;
					handIMG2.transform.position += -transform.up / 5.4f;
					SelectedTab.JsonHasBeenGenerated = false;
				}
				if (Input.GetKeyDown(KeyCode.LeftArrow))
				{
					startTime = Time.time;
					handIMG2.transform.position += -transform.right / 5.4f;
					SelectedTab.JsonHasBeenGenerated = false;
				}
			//}


		}
	}

    private void UpdateEverySecond()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (startTime + holdTime <= Time.time)
                handIMG.transform.position += transform.right / 5.4f;
            SelectedTab.JsonHasBeenGenerated = false;
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (startTime + holdTime <= Time.time)
                handIMG.transform.position += transform.up / 5.4f;
            SelectedTab.JsonHasBeenGenerated = false;
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (startTime + holdTime <= Time.time)
                handIMG.transform.position += -transform.up / 5.4f;
            SelectedTab.JsonHasBeenGenerated = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (startTime + holdTime <= Time.time)
                handIMG.transform.position += -transform.right / 5.4f;
            SelectedTab.JsonHasBeenGenerated = false;
        }
        if (IsTwoHanded.isOn)
        { 
			
			//if(ControllingHand2 == true)
			//{
				if (Input.GetKey(KeyCode.RightArrow))
				{
					if (startTime + holdTime <= Time.time)
						handIMG2.transform.position += transform.right / 5.4f;
					SelectedTab.JsonHasBeenGenerated = false;
				}
				if (Input.GetKey(KeyCode.UpArrow))
				{
					if (startTime + holdTime <= Time.time)
						handIMG2.transform.position += transform.up / 5.4f;
					SelectedTab.JsonHasBeenGenerated = false;
				}
				if (Input.GetKey(KeyCode.DownArrow))
				{
					if (startTime + holdTime <= Time.time)
						handIMG2.transform.position += -transform.up / 5.4f;
					SelectedTab.JsonHasBeenGenerated = false;
				}
				if (Input.GetKey(KeyCode.LeftArrow))
				{
					if (startTime + holdTime <= Time.time)
						handIMG2.transform.position += -transform.right / 5.4f;
					SelectedTab.JsonHasBeenGenerated = false;
				}
			//}
		}
    }

}

public static class FloatExtensions
{
    public enum ROUNDING { UP, DOWN, CLOSEST }
 
    public static float ToNearestMultiple(this float f, int multiple, ROUNDING roundTowards = ROUNDING.CLOSEST)
    {
        f /= multiple;
 
        return (roundTowards == ROUNDING.UP ? Mathf.Ceil(f) : (roundTowards == ROUNDING.DOWN ? Mathf.Floor(f) : Mathf.Round(f))) * multiple;
    }
 
    /// <summary>
    /// Using a multiple with a maximum of two decimal places, will round to this value based on the ROUNDING method chosen
    /// </summary>
    public static float ToNearestMultiple(this float f, float multiple, ROUNDING roundTowards = ROUNDING.CLOSEST)
    {
        f = float.Parse((f * 100).ToString("f0"));
        multiple = float.Parse((multiple * 100).ToString("f0"));
 
        f /= multiple;
 
        f = (roundTowards == ROUNDING.UP ? Mathf.Ceil(f) : (roundTowards == ROUNDING.DOWN ? Mathf.Floor(f) : Mathf.Round(f))) * multiple;
 
        return f / 100;
    }
}


static class CanvasExtensions {
public static Vector2 SizeToParent(this RawImage image, float padding = 0) {
        float w = 0, h = 0;
        var parent = image.GetComponentInParent<RectTransform>();
        var imageTransform = image.GetComponent<RectTransform>();

        // check if there is something to do
        if (image.texture != null) {
            if (!parent) { return imageTransform.sizeDelta; } //if we don't have a parent, just return our current width;
            padding = 1 - padding;
            float ratio = image.texture.width / (float)image.texture.height;
            var bounds = new Rect(0, 0, parent.rect.width, parent.rect.height);
            if (Mathf.RoundToInt(imageTransform.eulerAngles.z) % 180 == 90) {
                //Invert the bounds if the image is rotated
                bounds.size = new Vector2(bounds.height, bounds.width);
            }
            //Size by height first
            h = bounds.height * padding;
            w = h * ratio;
            if (w > bounds.width * padding) { //If it doesn't fit, fallback to width;
                w = bounds.width * padding;
                h = w / ratio;
            }
        }
        imageTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w);
        imageTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, h);
        return imageTransform.sizeDelta;
    }


    
}
