using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
using SFB;
using UnityEngine.EventSystems;

public class HandController : MonoBehaviour
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
          Vector3 DesiredHandPos = OnImportImagesPressed.readPoint;
        
        float moveAmount = transform.right.x / 5.4f;
        float distanceXHand1 = transform.position.x - OnImportImagesPressed.readPoint.x;
        var PixelsMovedHand1X = distanceXHand1 / moveAmount;
        var RoundedPixelsMovedHand1X = Mathf.Round(PixelsMovedHand1X);

        float distanceYHand1 = transform.position.y - OnImportImagesPressed.readPoint.y;
        var PixelsMovedHand1Y = distanceYHand1 / moveAmount;
        var RoundedPixelsMovedHand1Y = Mathf.Round(PixelsMovedHand1Y);
        for(int i = 0; i < RoundedPixelsMovedHand1X; i++)
            DesiredHandPos.x += transform.right.x / 5.4f;

        for(int i = 0; i < RoundedPixelsMovedHand1Y; i++)
            DesiredHandPos.y += transform.right.x / 5.4f;
        transform.position = DesiredHandPos;
    }
}
