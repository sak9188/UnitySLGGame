using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class Map : AJson
{
    public string md5;
    public string name;
    public int width;
    public int height;
    public int cell_size; // 以像素作为单位所以没有float
    public Dictionary<int, string> content;
}