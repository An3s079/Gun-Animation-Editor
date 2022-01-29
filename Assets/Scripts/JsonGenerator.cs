using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;
using System.Collections;

//so ive noticed each generator button has an individual instance of this script, and i think that should probably be changed to a single instance on some game object, or a static refrence
//especially since each instance needs its own variables for a lot of stuff. 
//this is great if you want each instance to use something different, (like different success and failure indicators), but could still be simplified into a few game objects dedicated to this behavior instead of being a component of the button
//so long winded note for future me or whatever poor soul has to update this mess, put a single jsongenerator component on an empty gameobject and drag it into the buttons
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

    [SerializeField]
    private GameObject FailureX;

    [SerializeField]
    private GameObject SuccessCheckmark;

    private static readonly CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
    //this class is basically responsible for all of data file output
   
    public IEnumerator AppearAndDisappear(GameObject obj)
    {
        obj.SetActive(true);
        yield return new WaitForSecondsRealtime(3);
        obj.SetActive(false);
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
    public void OnCreateAllJsonsPressed()
    {
        try
        {
            TabDisplay[] tabs = FindObjectsOfType<TabDisplay>();
            foreach (var tab in tabs)
            {
                //for some reason there is always an extra tab display from whats been loaded, so i nullchecck this to avoid exceptions
                //i havent been able to loccate the extra tab display using the inspector, which is very odd.
                if (tab != null && tab.animationInfo?.frames != null)
                {
                    foreach (var frame in tab.animationInfo.frames)
                    {
                        OutputFrameAsJson(frame);
                    }
                }
            }
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
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
            StartCoroutine(AppearAndDisappear(SuccessCheckmark));
        }
        else
        {
            Debug.LogError("Shit, path was empty!");
            StartCoroutine(AppearAndDisappear(FailureX));
        }
    }
    public void GenerateOffsetCode()
    {
        GaeAnimationInfo animation = MainSpriteController.instance.currentAnimation;
        if (animation != null)
        {
            StringBuilder builder = new StringBuilder("//make sure the animation name and variable names are correct, the program may have made the wrong decision \n");
            builder.Append("// it is better to be getting your clips like so \"gun.sprite.spriteAnimator.GetClipByName(gun.shootAnimation);\" and vary the animation name of course\n");
            builder.Append("tk2dSpriteAnimationClip animationclip = gun.sprite.spriteAnimator.GetClipByName(\"" + animation.animationName.Trim('_') + "\");\n");
            builder.Append("float[] offsetsX = new float[] {");
            builder.Append((animation.frames[0].offsetX / 16).ToString("f4", culture));
            builder.Append("f");
            for (int i = 1; i < animation.frames.Length; i++)
            {
                builder.Append(",");
                builder.Append((animation.frames[i].offsetX / 16).ToString("f4", culture));
                builder.Append("f");
            }
            builder.Append("};\n");

            builder.Append("float[] offsetsY = new float[] {");
            builder.Append((animation.frames[0].offsetY / 16).ToString("f4", culture));
            builder.Append("f");
            for (int i = 1; i < animation.frames.Length; i++)
            {
                builder.Append(",");
                builder.Append((animation.frames[i].offsetY / 16).ToString("f4", culture));
                builder.Append("f");
            }
            builder.Append("};\n");
            builder.Append("//simple method \n");
            builder.Append("for (int i = 0; i < offsetsX.Length && i < offsetsY.Length && i < animationclip.frames.Length; i++)");
            builder.Append("{");
            builder.Append("int id = animationclip.frames[i].spriteId;");
            builder.Append("tk2dSpriteDefinition def = frames.spriteCollection.spriteDefinitions[id];");
            builder.Append("Vector2 offset = new Vector2(offsetsX[i],offsetsY[i]);");
            builder.Append("def.position0 += offset;");
            builder.Append("def.position1 += offset;");
            builder.Append("def.position2 += offset;");
            builder.Append("def.position3 += offset;");
            builder.Append("}\n\n");

            builder.Append("//it is reccomended you keep a cleaner more maintainable code by doing this\n");
            builder.Append("//first add this method to your toolbox\n\n");

            builder.Append("static void AddOffsetToTk2DFrame(tk2dSpriteAnimationFrame frame, Vector2 offset)\n");
            builder.Append("for (int i = 0; i < offsetsX.Length && i < offsetsY.Length && i < animationclip.frames.Length; i++)\n");
            builder.Append("{\n");
            builder.Append("\tint id = animationclip.frames[i].spriteId;\n");
            builder.Append("\ttk2dSpriteDefinition def = frames.spriteCollection.spriteDefinitions[id];\n");
            builder.Append("\tdef.position0 += offset;\n");
            builder.Append("\tdef.position1 += offset;\n");
            builder.Append("\tdef.position2 += offset;\n");
            builder.Append("\tdef.position3 += offset;\n");
            builder.Append("}\n\n");

            builder.Append("//then you can this \n");
            builder.Append("for (int i = 0; i < offsetsX.Length && i < offsetsY.Length && i < animationclip.frames.Length; i++)\n");
            builder.Append("{\n");
            builder.Append("\tyourToolBox.AddOffsetToTk2DFrame(animationclip.frames[i],new Vector2(offsetsX[i],offsetsY[i]));\n");
            builder.Append("}");

            if (!string.IsNullOrEmpty(animation.AnimationDirectory))
            {
                
                File.WriteAllText(Path.Combine(animation.AnimationDirectory, animation.animationName+" offset code"+".txt"), builder.ToString());
                Debug.Log("nice, it (should have) worked");
            }
            else
            {
                Debug.LogError("Shit, path was empty!");
            }
        }
    }
    public void GenerateBarrelPositionCode()
    {
        GaeAnimationInfo animation = MainSpriteController.instance.currentFrame.animationInfo;
        FrameInfo info = MainSpriteController.instance.currentFrame;
        if (animation != null)
        {
            if (!info.path.Contains("idle_001"))
            {
                Debug.Log("this shouldnt ever happen, but ill let it slide");
            }
            float barrelOffsetXString = info.muzzleflashPositionX / 16f;
            float barrelOffsetYString = info.muzzleflashPositionY / 16f;

            string offsetCode = $"gun.barrelOffset.localPosition = new Vector3({barrelOffsetXString}f, {barrelOffsetYString}f, 0f);"; 
            if (!string.IsNullOrEmpty(animation.AnimationDirectory))
            {

                File.WriteAllText(Path.Combine(animation.AnimationDirectory, animation.animationName + " barrel offset code" + ".txt"), offsetCode);
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