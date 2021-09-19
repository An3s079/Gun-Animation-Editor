using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;

public class FrameJsonInfo
{
    public string name = null;
    public float x;
    public float y;
    public int width;
    public int height;
    public int flip;
    public object[] attachPoints = { new ArrayTypeUnkownAndSize(), new AttachPoint(), new AttachPoint() }; 

    public FrameJsonInfo() { }
}
public class AttachPoint
{
    public string name;
    public PositionVector position;
    public float angle;
    public AttachPoint() { }
    public AttachPoint(PositionVector position)
    {
        this.position = position;
    }
}
public class ArrayTypeUnkownAndSize
{
    [JsonProperty(".")]
    public string dot = "arraytype";
    public string name = "array";
    public int size = 2;
    public ArrayTypeUnkownAndSize() { }
    public ArrayTypeUnkownAndSize(int size) 
    {
        this.size = size;
    }

}
public class PositionVector
{
    public float x = 0;
    public float y = 0;
    public float z = 0;
    public PositionVector() { }
    public PositionVector(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

}

