using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


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

    public void OnCreateJsonAnimationPressed()
    {
        for (int i = 0; i < MainSpriteController.instance.currentAnimation.frames.Length; i++)
        {
            string data;
            FrameInfo frameInfo = MainSpriteController.instance.currentAnimation.frames[i];
            FrameJsonInfo frameJsonInfo = new FrameJsonInfo();


            frameJsonInfo.x = frameInfo.offsetX;
            frameJsonInfo.y = frameInfo.offsetY;
            frameJsonInfo.width = frameInfo.texture.width;
            frameJsonInfo.height = frameInfo.texture.height;

            List<object> attachPoints = new List<object>();
            ArrayTypeUnkownAndSize iHonestlyDontKnowWhatToCallThisThing = new ArrayTypeUnkownAndSize(twoHandToggle.isOn ? 3 : 4);
            attachPoints.Add(iHonestlyDontKnowWhatToCallThisThing);

            var primaryHandX = (frameInfo.hand1PositionX + frameInfo.offsetX) / 16;
            var primaryHandY = (frameInfo.hand1PositionY + frameInfo.offsetY) / 16;
            attachPoints.Add(new AttachPoint("SecondaryHand", new PositionVector(primaryHandX, primaryHandY)));

            if (frameInfo.isTwoHanded)
            {
                var secondaryHandX = (frameInfo.hand2PositionX + frameInfo.offsetX) / 16;
                var secondaryHandY = (frameInfo.hand2PositionY + frameInfo.offsetY) / 16;
                attachPoints.Add(new AttachPoint("SecondaryHand", new PositionVector(secondaryHandX, secondaryHandY)));
            }

            attachPoints.Add(new AttachPoint("Clip", new PositionVector(0.5625f, 0.375f)));
            attachPoints.Add(new AttachPoint("Casing", new PositionVector(0.5625f, 0.375f)));

            frameJsonInfo.attachPoints = attachPoints.ToArray();


            data = JsonConvert.SerializeObject(frameJsonInfo);


            if (!string.IsNullOrEmpty(frameInfo.path))
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
        
    }
    public void OnCreateJsonPressed()
    {
        string data;
        var xOffsetValue = float.Parse(xOffset.text);
        var yOffsetValue = float.Parse(xOffset.text);

        FrameInfo frameInfo = MainSpriteController.instance.currentFrame;
        FrameJsonInfo frameJsonInfo = new FrameJsonInfo();


        frameJsonInfo.x = frameInfo.offsetX;
        frameJsonInfo.y = frameInfo.offsetY;
        frameJsonInfo.width = frameInfo.texture.width;
        frameJsonInfo.height = frameInfo.texture.height;

        List<object> attachPoints = new List<object>();
        ArrayTypeUnkownAndSize iHonestlyDontKnowWhatToCallThisThing = new ArrayTypeUnkownAndSize(twoHandToggle.isOn ? 3 : 4);
        attachPoints.Add(iHonestlyDontKnowWhatToCallThisThing);

        var primaryHandX = (frameInfo.hand1PositionX + frameInfo.offsetX) / 16;
        var primaryHandY = (frameInfo.hand1PositionY + frameInfo.offsetY) / 16;
        attachPoints.Add(new AttachPoint("SecondaryHand", new PositionVector(primaryHandX, primaryHandY)));

        if (frameInfo.isTwoHanded)
        {
            var secondaryHandX = (frameInfo.hand2PositionX + frameInfo.offsetX) / 16;
            var secondaryHandY = (frameInfo.hand2PositionY + frameInfo.offsetY) / 16;
            attachPoints.Add(new AttachPoint("SecondaryHand", new PositionVector(secondaryHandX, secondaryHandY)));
        }

        attachPoints.Add(new AttachPoint("Clip", new PositionVector(0.5625f, 0.375f)));
        attachPoints.Add(new AttachPoint("Casing", new PositionVector(0.5625f, 0.375f)));

        frameJsonInfo.attachPoints = attachPoints.ToArray();


        data = JsonConvert.SerializeObject(frameJsonInfo);


        if (!string.IsNullOrEmpty(frameInfo.path))
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
    }
}