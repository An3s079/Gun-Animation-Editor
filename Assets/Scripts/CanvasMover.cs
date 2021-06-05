using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMover : MonoBehaviour
{
    Vector2 mousePos;
    private float deltaXH1, deltaYH1, deltaXH2, deltaYH2, deltaXC, deltaYC;
    public bool SettingPos;

    [SerializeField]
    private OnImportImagesPressed onImportImagesPressed;

    public GameObject hand1, hand2, canvas;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse2))
		{
            deltaXH1 = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - hand1.transform.position.x;
            deltaYH1 = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - hand1.transform.position.y;
            deltaXH2 = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - hand2.transform.position.x;
            deltaYH2 = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - hand2.transform.position.y;
            deltaXC = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - canvas.transform.position.x;
            deltaYC = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - canvas.transform.position.y;
            onImportImagesPressed.SelectedTab.JsonHasBeenGenerated = false;
        }
        if(Input.GetKey(KeyCode.Mouse2))
		{
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hand1.transform.position = new Vector3((mousePos.x - deltaXH1), (mousePos.y - deltaYH1));
            hand2.transform.position = new Vector3((mousePos.x - deltaXH2), (mousePos.y - deltaYH2));
            canvas.transform.position = new Vector3((mousePos.x - deltaXC), (mousePos.y - deltaYC));
        }
        if(Input.GetKeyUp(KeyCode.Mouse2))
		{
            hand1.transform.position = new Vector3(hand1.transform.position.x, hand1.transform.position.y);
            hand2.transform.position = new Vector3(hand2.transform.position.x, hand2.transform.position.y);
            canvas.transform.position = new Vector3(canvas.transform.position.x, canvas.transform.position.y);
            Vector3 DesiredHandPos = OnImportImagesPressed.readPoint;
        }
    }
}
