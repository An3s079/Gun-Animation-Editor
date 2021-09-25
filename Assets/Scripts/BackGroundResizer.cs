using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundResizer : MonoBehaviour
{
    // Start is called before the first frame update
    RectTransform rectTransform;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    //this method literally just takes a rectTransform and resizes it to fit the size of main sprite. used with the background sprite and grid sprite.
    void Update()
    {
        Image image = StaticRefrences.Instance.MainSprite;
        Vector2 rectScale = new Vector2(rectTransform.transform.localScale.x, rectTransform.transform.localScale.y);
        Vector2 imageScale = new Vector2(image.transform.localScale.x, image.transform.localScale.y);
        Vector2 sizefactor = ( imageScale/rectScale);
        rectTransform.sizeDelta = image.rectTransform.sizeDelta * sizefactor;
        
        rectTransform.transform.position = image.transform.position;
    }
}
