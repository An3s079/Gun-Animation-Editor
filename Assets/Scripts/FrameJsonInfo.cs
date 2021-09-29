using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
//these couple of classes represent a gun json file as directly as possible, and are used when reading and writing to jsons
public class FrameJsonInfo
{
    public string name = null;
    public float x;
    public float y;
    public int width;
    public int height;
    public int flip = 1;
    public object[] attachPoints = { new ArrayTypeUnkownAndSize(), new AttachPoint(), new AttachPoint() }; 

    public FrameJsonInfo() { }
}
public class AttachPoint
{
    [JsonRequiredAttribute]
    public string name;
    [JsonRequiredAttribute]
    public PositionVector position;
    [JsonRequiredAttribute]
    public float angle;
    public AttachPoint() { }
    public AttachPoint(PositionVector position)
    {
        this.position = position;
    }
    public AttachPoint(string name, PositionVector position)
    {

        this.name = name;
        this.position = position;
    }
}
public class ArrayTypeUnkownAndSize
{
    [JsonRequiredAttribute]
    [JsonProperty(".")]
    public string dot = "arraytype";
    [JsonRequiredAttribute]
    public string name = "array";
    [JsonRequiredAttribute]
    public int size = 2;
    public ArrayTypeUnkownAndSize() { }
    public ArrayTypeUnkownAndSize(int size) 
    {
        this.size = size;
    }

}
public class PositionVector
{
    [JsonRequiredAttribute]
    public float x = 0;
    [JsonRequiredAttribute]
    public float y = 0;
    [JsonRequiredAttribute]
    public float z = 0;
    public PositionVector() { }
    public PositionVector(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

}

