using System.Globalization;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Newtonsoft.Json;


public enum Axis
{
    X = 0,
    Y = 1
}

public class JsonGenerator : MonoBehaviour
{
    public static string dataOneHanded;
    public static string dataTwoHanded;
    public static string FilePath;

    [SerializeField]
    public GameObject CheckMark;

    [SerializeField]
    private GameObject primaryHand;

    [SerializeField]
    private GameObject secondaryHand;

    [SerializeField]
    private Toggle twoHandToggle;

    [SerializeField]
    private TMP_InputField xOffset;

    [SerializeField]
    private TMP_InputField yOffset;

    [SerializeField]
    private OnImportImagesPressed onImportImagesPressedComponent;

    private static readonly CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

    public static float DoTheMath(Transform generatorObject, GameObject objectToCheck, Axis axis, float offset)
    {
        // calculation without offset
        var moveAmount = axis == Axis.X ? generatorObject.right.x / 5.4f : generatorObject.up.y / 5.4f;

        var distance = axis == Axis.X ? objectToCheck.transform.position.x - OnImportImagesPressed.readPoint.x : objectToCheck.transform.position.y - OnImportImagesPressed.readPoint.y;

        var pixelsMoved = Mathf.Round(distance / moveAmount);

        // adding offset now
        pixelsMoved += offset;

        // doing the division to the sum and then parsing the string
        var objectValue = pixelsMoved / 16;
        return objectValue;
    }

    public void OnCreateJsonPressed()
    {
        string data;
        var xOffsetValue = float.Parse(xOffset.text);
        var yOffsetValue = float.Parse(xOffset.text);

        FrameJsonInfo frameInfo = new FrameJsonInfo();

        
        List<object> attachPoints = new List<object>();
        ArrayTypeUnkownAndSize iHonestlyDontKnowWhatToCallThisThing = new ArrayTypeUnkownAndSize(twoHandToggle.isOn ? 2 : 3);
        attachPoints.Add(iHonestlyDontKnowWhatToCallThisThing);

        var primaryHandX = DoTheMath(this.transform, primaryHand, Axis.X, xOffsetValue);
        var primaryHandY = DoTheMath(this.transform, primaryHand, Axis.Y, yOffsetValue);
        attachPoints.Add(new AttachPoint(new PositionVector(primaryHandX, primaryHandY)));

            
        if(twoHandToggle.isOn)
        {
            var secondaryHandX = DoTheMath(this.transform, secondaryHand, Axis.X, xOffsetValue);
            var secondaryHandY = DoTheMath(this.transform, secondaryHand, Axis.Y, yOffsetValue);
            attachPoints.Add(new AttachPoint(new PositionVector(secondaryHandX, secondaryHandY)));
        }

        attachPoints.Add(new AttachPoint(new PositionVector(0.5625f, 0.375f)));

        frameInfo.attachPoints = attachPoints.ToArray();
        
        
        data = JsonConvert.SerializeObject(frameInfo);


        if (!string.IsNullOrEmpty(FilePath))
        {
            File.WriteAllText(FilePath.Replace(".png", ".json"), data);
            Debug.Log("nice, it (should have) worked");
            onImportImagesPressedComponent.SelectedTab.JsonHasBeenGenerated = true;
        }
        else
        {
            Debug.LogError("Shit, path was empty!");
        }
        
    }

    private void Update()
    {
        /*if (onImportImagesPressedComponent.SelectedTab != null)
        {
            CheckMark.SetActive(onImportImagesPressedComponent.SelectedTab.JsonHasBeenGenerated);
        }*/
    }
}
/*=
                    "\n{" +
                "\n\"name\": null," +
              "\n\"x\": 0," +
              "\n\"y\": 0," +
              "\n\"width\": 10," +
              "\n\"height\": 24," +
              "\n\"flip\": 1," +
              "\n\"attachPoints\": [" +
                "\n{" +
                "\n  \".\": \"arraytype\"," +
                "\n  \"name\": \"array\"," +
                "\n  \"size\": 4" +
                "\n}," +
                "\n{" +
                "\n  \"name\": \"PrimaryHand\"," +
                "\n  \"position\": {" +
               $"\n  \"x\": {primaryHandXString}," +
               $"\n  \"y\": {primaryHandYString}," +
               $"\n  \"z\": 0.0" +
                "\n  }," +
                "\n  \"angle\": 0.0" +
                "\n}," +
                "\n{" +
                "\n  \"name\": \"SecondaryHand\"," +
                "\n  \"position\": {" +
               $"\n    \"x\": {secondaryHandXString}," +
               $"\n    \"y\": {secondaryHandYString}," +
                "\n    \"z\": 0.0" +
                "\n  }," +
                "\n  \"angle\": 0.0" +
                "\n}," +
                "\n{" +
                "\n  \"name\": \"Clip\"," +
                "\n  \"position\": {" +
                "\n    \"x\": 0.625," +
                "\n    \"y\": 0.125," +
                "\n    \"z\": 0.0" +
                "\n  }," +
                "\n  \"angle\": 0.0" +
                "\n}," +
                "\n{" +
                "\n  \"name\": \"Casing\"," +
                "\n  \"position\": {" +
                "\n    \"x\": 0.5625," +
                "\n    \"y\": 0.375," +
                "\n    \"z\": 0.0" +
                "\n  }," +
                "\n  \"angle\": 0.0" +
                "\n}" +
              "\n]" +
            "\n}";

            Debug.Log(dataTwoHanded);
*/