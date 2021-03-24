using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SFB;
using TMPro;

public class JsonGenerator : MonoBehaviour
{
    public static string FilePath;
    [SerializeField]
    GameObject hand;
    [SerializeField]
    GameObject hand2;

    [SerializeField]
    Toggle TwoHandToggle;
    double Hand1X = 0.0;
    double Hand1Y = 0.0;

    double Hand2X = 0;
    double Hand2Y = 0;
    
    [SerializeField]
    TMP_InputField XOffset;   
    [SerializeField]
    TMP_InputField YOffset;
    
    [SerializeField]
    public GameObject CheckMark;

    public void OnCreateJsonPressed()
    {   if(TwoHandToggle.isOn ==false)
        {
         string XoffsetString = XOffset.text;
         string YoffsetString = YOffset.text;

        float XOffsetValue = float.Parse(XoffsetString);
        float YOffsetValue = float.Parse(YoffsetString);
         
        float correctedXOffsetValue = XOffsetValue/16;
        float correctedYOffsetValue = YOffsetValue/16;

        float moveAmount = transform.right.x /  5.4f;
        float distanceXHand1 = hand.transform.position.x - OnImportImagesPressed.readPoint.x;
        var PixelsMovedHand1X = distanceXHand1 / moveAmount;
        var RoundedPixelsMovedHand1X = Mathf.Round(PixelsMovedHand1X);

        Hand1X = (RoundedPixelsMovedHand1X)/16;

        float distanceYHand1 = hand.transform.position.y - OnImportImagesPressed.readPoint.y;
        var PixelsMovedHand1Y = distanceYHand1 / moveAmount;
        var RoundedPixelsMovedHand1Y = Mathf.Round(PixelsMovedHand1Y);

        Hand1Y = (RoundedPixelsMovedHand1Y)/16;

        Hand1X += correctedXOffsetValue;
        Hand1Y += correctedYOffsetValue;

        bool isInt1X = Hand1X % 1 == 0;
        bool isInt1Y = Hand1Y % 1 == 0;
        if(isInt1X)
            Hand1X+=0.0001;
        if(isInt1Y)
            Hand1Y+= 0.0001;
        
        

        dataOneHanded = "{"+
    "\n\"name\": null," +
    "\n\"x\": 0,"+
    "\n\"y\": 0,"+
    "\n\"width\": 27,"+ 
    "\n\"height\": 27,"+
    "\n\"flip\": 1,"+
    "\n\"attachPoints\": ["+
    "\n{" +
    "\n   \".\": \"arraytype\","+
    "\n   \"name\": \"array\"," +
    "\n   \"size\": 2" +
    "\n},"+
    "\n{"+
    "\n   \"name\": \"PrimaryHand\","+
    "\n\"position\": {" +
    $"\n   \"x\": {Hand1X},"+
    $"\n   \"y\":{Hand1Y},"+
    "\n   \"z\": 0.0" +
    "\n}," +
    "\n\"angle\": 0.0"+
    "\n},"+
    
    
    "\n\n\n{"+
    "\n\"name\": \"Casing\"," +
    "\n\"position\": {"+
    "\n   \"x\": 0.5625,"+
    "\n   \"y\": 0.375,"+
    "\n   \"z\": 0.0" +
    "\n},"+
    "\n\"angle\": 0.0"+
    "\n}" +
    "\n]"+
    "\n}";
       
        Debug.Log(Hand1X);
        Debug.Log(Hand1Y);
        if (!string.IsNullOrEmpty(FilePath)) {
            File.WriteAllText(FilePath.Replace(".png", ".json"), dataOneHanded);
            Debug.Log("nice, it (should have) worked");
            onImportImagesPressed.SelectedTab.JsonHasBeenGenerated = true;
        }
        else
        {
            Debug.LogError("Shit, path was empty!");
        }
        }
        else 
        {
            string XoffsetString = XOffset.text;
         string YoffsetString = YOffset.text;

        float XOffsetValue = float.Parse(XoffsetString);
        float YOffsetValue = float.Parse(YoffsetString);
         
        float correctedXOffsetValue = XOffsetValue/16;
        float correctedYOffsetValue = YOffsetValue/16;

            //Hand1
             float moveAmount = transform.right.x /  5.4f;
        float distanceXHand1 = hand.transform.position.x - OnImportImagesPressed.readPoint.x;
        var PixelsMovedHand1X = distanceXHand1 / moveAmount;
        var RoundedPixelsMovedHand1X = Mathf.Round(PixelsMovedHand1X);

        Hand1X = (RoundedPixelsMovedHand1X)/16;

        float moveAmountY = transform.up.y /  5.4f;
        float distanceYHand1 = hand.transform.position.y - OnImportImagesPressed.readPoint.y;
        var PixelsMovedHand1Y = distanceYHand1 / moveAmountY;
        var RoundedPixelsMovedHand1Y = Mathf.Round(PixelsMovedHand1Y);

        Hand1Y = (RoundedPixelsMovedHand1Y)/16;

        Hand1X += correctedXOffsetValue;
        Hand1Y += correctedYOffsetValue;
        bool isInt1X = Hand1X % 1 == 0;
        bool isInt1Y = Hand1Y % 1 == 0;
        if(isInt1X)
            Hand1X+=0.00001;
        if(isInt1Y)
            Hand1Y+= 0.00001;
        

            //Hand2
            float moveAmount2handed = transform.right.x /  5.4f;
        float distanceXHand2 = hand2.transform.position.x - OnImportImagesPressed.readPoint.x;
        var PixelsMovedHand2X = distanceXHand2 / moveAmount2handed;
        var RoundedPixelsMovedHand2X = Mathf.Round(PixelsMovedHand2X);

        Hand2X = (RoundedPixelsMovedHand2X)/16;

        float moveAmount2handedY = transform.up.y /  5.4f;
        float distanceYHand2 = hand2.transform.position.y - OnImportImagesPressed.readPoint.y;
        var PixelsMovedHand2Y = distanceYHand2 / moveAmount2handedY;
        var RoundedPixelsMovedHand2Y = Mathf.Round(PixelsMovedHand2Y);

        Hand2Y = (RoundedPixelsMovedHand2Y)/16;

        Hand2X += correctedXOffsetValue;
        Hand2Y += correctedYOffsetValue;
         bool isInt2X = Hand2X % 1 == 0;
        bool isInt2Y = Hand2Y % 1 == 0;
        if(isInt2X)
            Hand2X+=0.00001;
        if(isInt2Y)
            Hand2Y+= 0.00001;

        dataTwoHanded =
        "\n{"+
    "\n\"name\": null,"+
  "\n\"x\": 0,"+
  "\n\"y\": 0,"+
  "\n\"width\": 10,"+
  "\n\"height\": 24,"+
  "\n\"flip\": 1," +
  "\n\"attachPoints\": ["+
    "\n{"+
    "\n  \".\": \"arraytype\","+
    "\n  \"name\": \"array\","+
    "\n  \"size\": 4"+
    "\n},"+
    "\n{"+
    "\n  \"name\": \"PrimaryHand\","+
    "\n  \"position\": {"+
   $"\n  \"x\": {Hand1X},"+
   $"\n  \"y\": {Hand1Y},"+
   $"\n  \"z\": 0.0"+
    "\n  },"+
    "\n  \"angle\": 0.0"+
    "\n},"+
    "\n{"+
    "\n  \"name\": \"SecondaryHand\","+
    "\n  \"position\": {"+
   $"\n    \"x\": {Hand2X}," +
   $"\n    \"y\": {Hand2Y}," +
    "\n    \"z\": 0.0"+
    "\n  },"+
    "\n  \"angle\": 0.0"+
    "\n},"+
    "\n{"+
    "\n  \"name\": \"Clip\","+
    "\n  \"position\": {"+
    "\n    \"x\": 0.625,"+
    "\n    \"y\": 0.125," +
    "\n    \"z\": 0.0" +
    "\n  },"+
    "\n  \"angle\": 0.0"+
    "\n},"+
    "\n{"+
    "\n  \"name\": \"Casing\","+
    "\n  \"position\": {"+
    "\n    \"x\": 0.5625,"+
    "\n    \"y\": 0.375,"+
    "\n    \"z\": 0.0"+
    "\n  },"+
    "\n  \"angle\": 0.0"+
    "\n}"+
  "\n]"+
"\n}";
        if (!string.IsNullOrEmpty(FilePath)) {
            File.WriteAllText(FilePath.Replace(".png", ".json"), dataTwoHanded);
            Debug.Log("nice, it (should have) worked");
            onImportImagesPressed.SelectedTab.JsonHasBeenGenerated = true;
        }
        else
        {
            Debug.LogError("Shit, path was empty!");
        }
        }
    }
    [SerializeField]
    OnImportImagesPressed onImportImagesPressed;
    void Update()
    {
        if(onImportImagesPressed.SelectedTab != null)
        {
        if(onImportImagesPressed.SelectedTab.JsonHasBeenGenerated == true)
            CheckMark.SetActive(true); 
        else
            CheckMark.SetActive(false);
        }
    }

    public static string dataOneHanded;
    public static string dataTwoHanded;
}
