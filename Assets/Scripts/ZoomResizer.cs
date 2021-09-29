using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ZoomResizer : MonoBehaviour
{
    [SerializeField]
    private List<Transform> Resizeables;
    [SerializeField]
    private TMP_InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.Equals))
        {
            Resize(1);
        }
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
                Resize(-1);
            
        }
    }

    public void  TextInput(string text)
    {
        int scale = StaticRefrences.zoomScale;
        int.TryParse(text, out scale);
        Resize(scale - StaticRefrences.zoomScale);
    }

    //for each resizeable, updates the scale. one of the reasons most things in gae should be of standard size / world units to pixels ratio that makes sense
    //also calls update sprite to update position of things that need updating
    public void Resize(int IncreaseAmount)
    {
        int scale = IncreaseAmount + StaticRefrences.zoomScale;
        if (scale <= 1)
        {
            scale = 1;
        }
        for (int i = 0; i < Resizeables.Count; i++)
        {
            Resizeables[i].localScale = new Vector3(scale, scale, scale);
            StaticRefrences.zoomScale = scale;
            MainSpriteController.instance.UpdateSprite(true);
        }
        if (inputField != null)
        {
            inputField.text = StaticRefrences.zoomScale.ToString();
        }
    }
}
