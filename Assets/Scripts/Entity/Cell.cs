using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

public class Cell : AJson
{
    [NonSerialized]
    public string md5;

    public string name;
    public int avo;
    public int def;
    public int alt;
    public string other;
}