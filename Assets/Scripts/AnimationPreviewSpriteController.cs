using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;
using UnityEngine.EventSystems;

public class AnimationPreviewSpriteController : MonoBehaviour
{
    public Image DisplaySprite;
    private int index = 0;

    private static readonly CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

    // Start is called before the first frame update
    void Start()
    {
        defaultGungeoneerPos = gungeoneer.anchoredPosition;
        StaticRefrences.Instance.previewController = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    
    private RectTransform rectTransform;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private RectTransform hand1;
    [SerializeField]
    private RectTransform hand2;
    [SerializeField]
    private RectTransform gungeoneer;
    [SerializeField]
    private GameObject gungeoneer_LHand;
    private Vector2 defaultGungeoneerPos = new Vector2(0, 0);
    [SerializeField]
    GameObject parentPanel;

    [SerializeField]
    private TMP_InputField initialXOffset;

    [SerializeField]
    private TMP_InputField initialYOffset;
    [SerializeField]
    private TMP_InputField frameRateInputLabel;
    [SerializeField]
    private TextMeshProUGUI frameCounter;

    private Vector2 initialOffset = Vector2.zero;

    private Vector2 posOffset = new Vector2(0, 0);
    public void UpdateSprite()
    {
        if (currentAnimation != null && currentFrame != null && currentFrame.sprite != null)
        {

            DisplaySprite.sprite = currentFrame.sprite;

            DisplaySprite.SetNativeSize();

            Vector2 anchoredPos = DisplaySprite.rectTransform.anchoredPosition;
            //currently centers sprite, should actully offset sprite based on gungeoneer pos.

            Vector2 pos = gungeoneer.anchoredPosition + (new Vector2(currentFrame.offsetX, currentFrame.offsetY) + initialOffset) * StaticRefrences.zoomScale;
            DisplaySprite.rectTransform.anchoredPosition = pos;

            hand2.gameObject.SetActive(currentFrame.isTwoHanded);
            gungeoneer_LHand.SetActive(!currentFrame.isTwoHanded);

            Vector2 handpos1 = new Vector2(pos.x + currentFrame.hand1PositionX * StaticRefrences.zoomScale, pos.y + currentFrame.hand1PositionY * StaticRefrences.zoomScale);
            Vector2 handpos2 = new Vector2(pos.x + currentFrame.hand2PositionX * StaticRefrences.zoomScale, pos.y + currentFrame.hand2PositionY * StaticRefrences.zoomScale);

            hand1.anchoredPosition = handpos1;
            hand2.anchoredPosition = handpos2;

            if (frameCounter != null)
            {
                frameCounter.text = (index + 1).ToString();
            }
        }
    }

    int framerate = 12;
    public void UpdateFramerate()
    {
        int.TryParse(frameRateInputLabel.text,out framerate);
    }
    public void UpdateInitialOffsets()
    {
        float x = 0;
        float y = 0;
        float.TryParse(initialXOffset.text, out x);
        float.TryParse(initialYOffset.text, out y);
        initialOffset = new Vector2(x, y);
        UpdateSprite();
    }
    public void UpdateOffsets()
    {
        int zoom = StaticRefrences.zoomScale;
        currentFrame.offsetX = (DisplaySprite.rectTransform.anchoredPosition.x - gungeoneer.anchoredPosition.x) / zoom;
        currentFrame.offsetY = (DisplaySprite.rectTransform.anchoredPosition.y - gungeoneer.anchoredPosition.y) / zoom;
    }

    public void Open()
    {
        parentPanel.SetActive(true);
        UpdateInitialOffsets();
        UpdateFramerate();
        UpdateSprite();
    }
    public void Close()
    {
        gungeoneer.anchoredPosition = defaultGungeoneerPos;
        StopAllCoroutines();
        //initialXOffset.text = "0";
        //initialYOffset.text = "0";
        //frameRateInputLabel.text = "12";
        //framerate = 12;
        frameCounter.text = "1";
        index = 0;
        parentPanel.SetActive(false);
    }

    IEnumerator frameCycleCoroutine;
    public void StartFrameCycle()
    {
        frameCycleCoroutine = CycleFrames();
        StartCoroutine(frameCycleCoroutine);
    }
    public void StopFrameCycle()
    {
        StopAllCoroutines();
    }
    IEnumerator CycleFrames()
    {
        while(true)
        {
            if (framerate > 0)
            {
                yield return new WaitForSecondsRealtime(1f / framerate);
            }
            else
            {
                yield return new WaitForSecondsRealtime(1f / 12f);
            }
            NextFrame();
        }
    }


    public FrameInfo currentFrame
    {
        get
        {
            return currentAnimation.frames[index];
        }
    }
    public GaeAnimationInfo currentAnimation
    {
        get
        {
            return StaticRefrences.Instance.spriteController.currentAnimation;
        }
    }

    public void NextFrame()
    {
        if (currentAnimation != null)
        {
            MoveIndex(1);
        }
    }
    public void PreviousFrame()
    {

        if (currentAnimation != null)
        {
            MoveIndex(-1);
        }
    }
    private void MoveIndex(int i)
    {
        index += i;
        if (currentAnimation != null)
        {

            if (index < 0)
            {
                index = currentAnimation.frames.Length - 1;
            }
            else if (index >= currentAnimation.frames.Length)
            {
                index = 0;
            }
            UpdateSprite();
        }
    }
}
