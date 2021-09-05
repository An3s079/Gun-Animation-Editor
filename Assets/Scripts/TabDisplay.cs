using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
using TMPro;

public class TabDisplay : MonoBehaviour
{
    public RawImage GreyBackgroundThangIdk;
    public TextMeshProUGUI TMPtext;
    public Image Sprite;
    public Image SpriteDisplay;
    public Sprite DefaultTexture;
    public GameObject ScrollThingBitchIdkLol;
    public Sprite BeegSprite;

    public RawImage HandImage;
    public RawImage HandImage2;
    public Toggle IsTwoHanded;

    public string FilePath;

    private Vector2 Hand1Pos;
    private Vector2 Hand2Pos;
    private Vector2 CanvasPos;
    public bool JsonHasBeenGenerated;
    public OnImportImagesPressed onImportImagesPressed;
    public JsonGenerator jsonGenerator;
    void Start()
    {
        Vector3[] corners = new Vector3[4];
        SpriteDisplay.GetComponent<RectTransform>().GetWorldCorners(corners);
        Hand1Pos = corners[0];
        Hand2Pos = corners[0];
        JsonHasBeenGenerated = false;
        
    }

    public void OnTabClicked()
    {

        onImportImagesPressed.SelectedTab = this;
        SpriteDisplay.sprite = DefaultTexture;
        SpriteDisplay.SetNativeSize();
        SpriteDisplay.sprite.texture.filterMode = FilterMode.Point;

        SpriteDisplay.sprite = BeegSprite;
        SpriteDisplay.SetNativeSize();
        GreyBackgroundThangIdk.rectTransform.sizeDelta = SpriteDisplay.rectTransform.sizeDelta;
        GreyBackgroundThangIdk.transform.localScale = SpriteDisplay.transform.localScale;
        GreyBackgroundThangIdk.transform.position = SpriteDisplay.transform.position;       
        HandImage.SetNativeSize();
        var handCollider = HandImage.GetComponent<BoxCollider2D>();

        //SpriteDisplay.SizeToParent();
        foreach(Transform child in ScrollThingBitchIdkLol.transform)
        {
            var colors =  child.GetComponent<Button>().colors;
            colors.normalColor = Color.gray;
            child.GetComponent<Button>().colors = colors;
        }
         var Selfcolors = this.GetComponent<Button>().colors;
         Selfcolors.normalColor = Color.white;
         this.GetComponent<Button>().colors = Selfcolors;
         JsonGenerator.FilePath = FilePath;

        if(JsonHasBeenGenerated == true)
            jsonGenerator.CheckMark.SetActive(true);
        else
            jsonGenerator.CheckMark.SetActive(false);

        Vector3[] corners = new Vector3[4];
        SpriteDisplay.GetComponent<RectTransform>().GetWorldCorners(corners);
        SpriteDisplay.transform.position = CanvasPos;
        HandImage.transform.position = Hand1Pos;
        HandImage2.transform.position = Hand2Pos;
        HandImage2.transform.localScale = HandImage.transform.localScale;
    }

    void Update()
    {
        if(SpriteDisplay.sprite == BeegSprite)
        {
            Hand1Pos = HandImage.transform.position;
            Hand2Pos = HandImage2.transform.position;
            CanvasPos = SpriteDisplay.transform.position;
        }
    }

    public void OnXClicked()
    {
        Destroy(gameObject);
    }

}
