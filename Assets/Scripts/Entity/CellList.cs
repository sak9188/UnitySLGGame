using UnityEngine;
using System.Collections;
using System;

public class CellList : AJson
{
    [NonSerialized]
    public string md5;

    public string name;
    public int avo;
    public int def;
    public int alt;
    public string other;
}
