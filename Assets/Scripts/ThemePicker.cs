using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class ThemePicker : MonoBehaviour
{
    public RawImage Canvas;
    public Texture DarkTheme;
    public Texture LightTheme;
    public Button LightButton;
    public Button DarkButton;
    public void OnLightThemePressed()
    {
        Canvas.texture = LightTheme;
        foreach(Transform child in gameObject.transform)
        {
            var colors =  child.GetComponent<Button>().colors;
            colors.normalColor = Color.gray;
            child.GetComponent<Button>().colors = colors;
        }
        var Selfcolors = LightButton.GetComponent<Button>().colors;
         Selfcolors.normalColor = Color.white;
         LightButton.GetComponent<Button>().colors = Selfcolors;
    }

    public void OnDarkThemePressed()
    {
        Canvas.texture = DarkTheme;
        foreach(Transform child in gameObject.transform)
        {
            var colors =  child.GetComponent<Button>().colors;
            colors.normalColor = Color.gray;
            child.GetComponent<Button>().colors = colors;
        }
        var Selfcolors = DarkButton.GetComponent<Button>().colors;
         Selfcolors.normalColor = Color.white;
         DarkButton.GetComponent<Button>().colors = Selfcolors;
    }
}
