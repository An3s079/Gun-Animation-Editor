using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class PrideFlags : MonoBehaviour
{
    public static List<Texture2D> prideFlags = new List<Texture2D>();

    public Image flag;

    public void ChangeFlag()
    {
        Debug.Log("Changing flag");
        var i = UnityEngine.Random.Range(0, prideFlags.Count - 1);
        var s = Sprite.Create(prideFlags[i], new Rect(0.0f, 0.0f, prideFlags[i].width, prideFlags[i].height), new Vector2(0.5f, 0.5f));
        flag.sprite = s;
    }

    void Start()
    {
        var path = Application.dataPath;
            path += "/../";
        path += "Flags/";
        Debug.Log(path);
        prideFlags.Add(Resources.Load<Sprite>("gay_pride").texture);

        var files = GetTexturesFromDirectory(path);
        foreach(Texture2D t in files)
		{
            prideFlags.Add(t);
        }
    }

    //Yea i stole this from ItemAPI, so what?

    /// <summary>
    /// Converts all png's in a folder to a list of Texture2D objects
    /// </summary>
    public static List<Texture2D> GetTexturesFromDirectory(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
        {
            Debug.LogError(directoryPath + " not found.");
            return null;
        }

        List<Texture2D> textures = new List<Texture2D>();
        foreach (string filePath in Directory.GetFiles(directoryPath))
        {
            if (!filePath.EndsWith(".png")) continue;

            Texture2D texture = BytesToTexture(File.ReadAllBytes(filePath), Path.GetFileName(filePath).Replace(".png", ""));
            textures.Add(texture);
        }
        return textures;
    }

    /// <summary>
    /// Converts a byte array into a Texture2D
    /// </summary>
    public static Texture2D BytesToTexture(byte[] bytes, string resourceName)
    {
        Texture2D texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        ImageConversion.LoadImage(texture, bytes);
        texture.filterMode = FilterMode.Point;
        texture.name = resourceName;
        return texture;
    }

}
