using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomResizer : MonoBehaviour
{
    [SerializeField]
    private List<Transform> Resizeables;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            Resize(StaticRefrences.zoomScale + 1);
        }
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            if (StaticRefrences.zoomScale>1)
            {
                Resize(StaticRefrences.zoomScale - 1);
            }
        }
    }
    private void Resize(int scale)
    {
        for (int i = 0; i < Resizeables.Count; i++)
        {
            Resizeables[i].localScale = new Vector3(scale, scale, scale);
            StaticRefrences.zoomScale = scale;
            MainSpriteController.instance.UpdateSprite();
        }
    }
}
