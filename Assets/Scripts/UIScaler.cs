using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScaler : MonoBehaviour
{
    [Header("Camera")]
    public float speed = 10.0f;

    float zoom;
         [Header("Zoom")]
    public float minZoom = 0.009611567f;
    public float maxZoom = 77.75285f;
    
    void Start()
    {
       zoom = 5.660473f;
    }

     void Update()
    {
        Zoom();
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        GetComponent<Camera>().orthographicSize = zoom; 
        
    }

    
     void Zoom()
    {
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            zoom -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            zoom += speed * Time.deltaTime;
        }

        if (Input.mouseScrollDelta.y > 0)
        {
            zoom -= speed * Time.deltaTime * 10f;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            zoom += speed * Time.deltaTime * 10f;
        }
    }
}
