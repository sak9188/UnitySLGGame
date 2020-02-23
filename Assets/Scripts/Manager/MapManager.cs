using Assets.Scripts.Help;
using Assets.Scripts.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.ConfigReader.MapReader;

using Md5Map = System.Tuple<string, Map>;
using Md5CellList = System.Tuple<string, CellList>;

namespace Assets.Scripts.Manager
{
    /// <summary>
    /// 地图管理器 提供各种地图相关的东西
    /// </summary>
    public class MapManager
    {
        #region 单例
        public static GameConsole GC = null;
        private static MapManager instance = null;
        private static readonly object padlock = new object();

        private MapManager()
        {
            Init();
        }

        public static MapManager Instance(GameConsole gc = null)
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    // 如果类的实例不存在则创建，否则直接返回
                    if (instance == null)
                    {
                        instance = new MapManager();
                        GC = gc;
                    }
                }
            }
            return instance;
        }
        # endregion


        // 存储Map的相关数值
        private IDictionary<string, Md5Map> mapDict;
        private IDictionary<string, Md5CellList> cellListDict;

        private void Init()
        {
            //统一管理所有的地图
            mapDict = new Dictionary<string, Md5Map>();
        }

        public void LoadMap(string path)
        {
            MapReader mr = new MapReader();
            Map map = mr.GetMap(path);
            LoadMap(map);
        }

        /// <summary>
        /// 载入地图配置
        /// </summary>
        /// <param name="map"></param>
        public void LoadMap(Map map)
        {
            try
            {
                mapDict.Add(map.md5, new Md5Map(map.name, map));
                LoadCellList(map.md5);
            }
            catch (ArgumentException)
            {
                Debug.LogWarning(string.Format("MapDict 重复的键值对{}-{}", map.md5, map.name));
            }
        }

        public void LoadCellList(string md5)
        {
            CellListReader clr = new CellListReader();
            CellList list = clr.GetCellList(md5);
            LoadCellList(list);
        }

        /// <summary>
        /// 载入Cell配置
        /// </summary>
        private void LoadCellList(CellList list)
        {
            try
            {
                cellListDict.Add(list.md5, new Md5CellList(list.name, list));
            }
            catch (ArgumentException)
            {
                Debug.LogWarning(string.Format("CellListDict 重复的键值对{}-{}", list.md5, list.name));
            }
        }

        public void GenerateMap(GameConsole gc, Map map)
        {
            if (IsEmpty.Empty(gc))
                Debug.LogWarning("生成地图失败 没有Console");

            MapView mv = new MapView();
            mv.Height = map.height;
            mv.Width = map.width;
            mv.CellList = GetCellListByMap(map);
            gc.GenerateMap(mv);
        }

        public IList<Cell> GetCellListByMap(Map map)
        {
            Dictionary<int, string> content = map.content;
            var cellTuple = cellListDict[map.md5];
            var clist = cellTuple.Item2;
            List<Cell> retrunList = new List<Cell>();
            foreach (int key in content.Keys)
            {
                string val = content[key];
                string[] vals = val.Split(' ');
                int[] nums = Array.ConvertAll(vals, int.Parse);
                foreach (int idx in nums)
                {
                    retrunList.Add(clist.list[idx]);
                }
            }
            return retrunList;
        }
    }
}
