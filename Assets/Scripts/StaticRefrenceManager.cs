using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class StaticRefrences
{
    private static StaticRefrenceManager instance;
    public static int zoomScale = 10;
    public static StaticRefrenceManager Instance
    {

        get
        {
            if (instance == null)
            {
                instance = UnityEngine.Object.FindObjectOfType<StaticRefrenceManager>();
            }
            return instance;
        }
    }
}
public class StaticRefrenceManager : MonoBehaviour
{
    public GameObject ImgTabPrefab;
    public GameObject TabArea;
    public GameObject scrollthing;
    public Toggle IsTwoHanded;
    public Toggle IsGungeoneerOn;
    public RectTransform Gungeoneer;
    public RawImage handIMG;
    public RawImage handIMG2;
    public Image MainSprite;
    public RawImage BackgroundImage;
    public MainSpriteController spriteController; 
    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
