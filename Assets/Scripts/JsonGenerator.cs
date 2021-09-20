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
            OutputFrameAsJson(MainSpriteController.instance.currentAnimation.frames[i]);
        }
        
    }
    public void OnCreateJsonPressed()
    {
        FrameInfo frameInfo = MainSpriteController.instance.currentFrame;
        OutputFrameAsJson(frameInfo);
    }
    private void OutputFrameAsJson(FrameInfo info)
    {
        string data;
        FrameInfo frameInfo = info;
        FrameJsonInfo frameJsonInfo = new FrameJsonInfo();


        frameJsonInfo.x = frameInfo.offsetX;
        frameJsonInfo.y = frameInfo.offsetY;
        frameJsonInfo.width = frameInfo.texture.width;
        frameJsonInfo.height = frameInfo.texture.height;

        List<object> attachPoints = new List<object>();
        ArrayTypeUnkownAndSize iHonestlyDontKnowWhatToCallThisThing = new ArrayTypeUnkownAndSize(twoHandToggle.isOn ? 4 : 3);
        attachPoints.Add(iHonestlyDontKnowWhatToCallThisThing);

        var primaryHandX = (frameInfo.hand1PositionX + frameInfo.offsetX) / 16;
        var primaryHandY = (frameInfo.hand1PositionY + frameInfo.offsetY) / 16;
        attachPoints.Add(new AttachPoint("PrimaryHand", new PositionVector(primaryHandX, primaryHandY)));

        if (frameInfo.isTwoHanded)
        {
            var secondaryHandX = (frameInfo.hand2PositionX + frameInfo.offsetX) / 16;
            var secondaryHandY = (frameInfo.hand2PositionY + frameInfo.offsetY) / 16;
            attachPoints.Add(new AttachPoint("SecondaryHand", new PositionVector(secondaryHandX, secondaryHandY)));
        }

        attachPoints.Add(new AttachPoint("Clip", new PositionVector(0.5625f, 0.375f)));
        attachPoints.Add(new AttachPoint("Casing", new PositionVector(0.5625f, 0.375f)));

        frameJsonInfo.attachPoints = attachPoints.ToArray();


        data = JsonConvert.SerializeObject(frameJsonInfo, Formatting.Indented);


        if (!string.IsNullOrEmpty(frameInfo.path))
        {
            if (!frameInfo.path.EndsWith(".png"))
            {
                frameInfo.path += ".png";
            }
            File.WriteAllText(frameInfo.path.Replace(".png", ".json"), data);
            Debug.Log("nice, it (should have) worked");
        }
        else
        {
            Debug.LogError("Shit, path was empty!");
        }
    }
    public void GenerateOffsetCode()
    {
        GaeAnimationInfo animation = MainSpriteController.instance.currentAnimation;
        if (animation != null)
        {
            string offsetCode = 
                "//make sure the animation name and variable names are correct, the program may have made the wrong desicion\n" +
                "tk2dSpriteAnimationClip animationclip = gun.sprite.spriteAnimator.GetClipByName(" + animation.animationName.Trim('_') + ");\n" +
                "float[] offsetsX = new float[] {";
            offsetCode += (animation.frames[0].offsetX/16).ToString("f4", culture);
            for (int i = 1; i < animation.frames.Length; i++)
            {
                offsetCode += "," + (animation.frames[i].offsetX/16).ToString("f4", culture);
            }
            offsetCode += "};\n";


            offsetCode += "float[] offsetsY = new float[] {";
            offsetCode += (animation.frames[0].offsetY/16f).ToString("f4", culture);
            for (int i = 1; i < animation.frames.Length; i++)
            {
                offsetCode += "," + (animation.frames[i].offsetY/16f).ToString("f4",culture);
            }
            offsetCode += "};\n";
            offsetCode +=
            "for (int i = 0; i < offsetsX.Length && i < offsetsY.Length && i < fireClip.frames.Length; i++)" +
                "{" +
                    "int id = fireClip.frames[i].spriteId;" +
                    "animationclip.frames[i].spriteCollection.spriteDefinitions[id].position0.x += offsetsX[i];" +
                    "animationclip.frames[i].spriteCollection.spriteDefinitions[id].position0.y += offsetsY[i];" +
                    "animationclip.frames[i].spriteCollection.spriteDefinitions[id].position1.x += offsetsX[i];" +
                    "animationclip.frames[i].spriteCollection.spriteDefinitions[id].position1.y += offsetsY[i];" +
                    "animationclip.frames[i].spriteCollection.spriteDefinitions[id].position2.x += offsetsX[i];" +
                    "animationclip.frames[i].spriteCollection.spriteDefinitions[id].position2.y += offsetsY[i];" +
                    "animationclip.frames[i].spriteCollection.spriteDefinitions[id].position3.x += offsetsX[i];" +
                    "animationclip.frames[i].spriteCollection.spriteDefinitions[id].position3.y += offsetsY[i];" +
               " }";

            if (!string.IsNullOrEmpty(animation.AnimationDirectory))
            {
                
                File.WriteAllText(Path.Combine(animation.AnimationDirectory, animation.animationName+" offset code"+".txt"), offsetCode);
                Debug.Log("nice, it (should have) worked");
            }
            else
            {
                Debug.LogError("Shit, path was empty!");
            }

        }
    }

    private void Update()
    {
    }
}