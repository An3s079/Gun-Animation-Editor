using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
using SFB;
using UnityEngine.EventSystems;

public class CanvasGrabber : MonoBehaviour
{
    Vector2 mousePos;
    private float  deltaX, deltaY;
    public bool SettingPos;

    [SerializeField]
    private OnImportImagesPressed onImportImagesPressed;

    private void OnMouseDown()
    {
        deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
        onImportImagesPressed.SelectedTab.JsonHasBeenGenerated = false;
    }

    private void OnMouseDrag()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3((mousePos.x - deltaX), (mousePos.y - deltaY));

      
    }

    private void OnMouseUp()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y);
    }
}
