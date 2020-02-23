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

public class MapView
{
    public int Width;
    public int Height;
    public float CellSize;

    private Vector3 centerPoint;
    public Vector3 CenterPoint
    {
        get { return centerPoint; }
        set
        {
            float halfX = Width / 2.0 * CellSize;
            float halfY = Height / 2.0 * CellSize;
            leftTopPoint = new Vector3(centerPoint.x - halfX, centerPoint.y - halfY);
        }
    }

    private Vector3 leftTopPoint;
    public Vector3 LeftTopPoint
    {
        get { return leftTopPoint; }
    }
    public IList<Cell> CellList;

    public MapView()
    {
        CenterPoint = Vector3.zero;
        CellSize = SLGDefine.GET_CELL_SIZE();
    }
}
