using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;


public class FrameInfo
{
    public FrameInfo(Texture2D texture)
    {
        this.texture = texture;
    }
    public FrameInfo(Texture2D texture,Sprite sprite)
    {
        this.texture = texture;
        this.sprite = sprite;
    }
    public FrameInfo(Texture2D texture, Sprite sprite, int handOffsetX, int handOffsetY, int offsetX, int offsetY, bool isTwoHanded)
    {
        this.texture = texture;
        this.sprite = sprite;
        this.handOffsetX = handOffsetX;
        this.handOffsetY = handOffsetY;
        this.offsetX = offsetX;
        this.offsetY = offsetY;
        this.isTwoHanded = isTwoHanded;
    }
    public Texture2D texture;
    public Sprite sprite;
    public int offsetX;
    public int offsetY;
    public int handOffsetX;
    public int handOffsetY;
    public bool isTwoHanded;
}

