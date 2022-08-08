using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Globalization;
using System.IO;
using TMPro;
using UnityEngine.UI;
using System.Text;
using System;
/*using My Balls;*/

public static class MasterManager
{
    private static Manager instance;
    //unused
    public static Manager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = UnityEngine.Object.FindObjectOfType<Manager>();
            }
            return instance;
        }
    }
    
}
public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Application.Quit();
        if (UnityEngine.Random.value < 0.001)
        {
        Application.OpenURL("https://modworkshop.net/mod/27616");
        } //atleast let me have this
        DontDestroyOnLoad(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    //i do not know which class encapsulates this method. the joys of OOP
    public static void SetAllHandedness(bool val)
    {
        try
        {
            TabDisplay[] tabs = UnityEngine.Object.FindObjectsOfType<TabDisplay>();
            foreach (var tab in tabs)
            {
                //for some reason there is always an extra tab display from whats been loaded, so i nullchecck this to avoid exceptions
                //i havent been able to loccate the extra tab display using the inspector, which is very odd.
                if (tab != null && tab.animationInfo?.frames != null)
                {
                    foreach (var frame in tab.animationInfo.frames)
                    {
                        tab.animationInfo.IsTwoHanded = val;
                        StaticRefrences.Instance.IsTwoHanded.isOn = val;
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

}
