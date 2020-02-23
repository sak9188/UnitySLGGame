using Assets.Scripts.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class GameConsole
    {
        #region 单例
        private static GameConsole instance = null;
        private static readonly object padlock = new object();

        private GameConsole()
        {
        }

        public static GameConsole Instance()
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    // 如果类的实例不存在则创建，否则直接返回
                    if (instance == null)
                    {
                        instance = new GameConsole();
                    }
                }
            }
            return instance;
        }
        # endregion

        private ObjcetPoolManager OPM;
        private MapManager MM;
        private SceneManager SM;

        private void Init()
        {
            MM = MapManager.Instance(this);
            SM = SceneManager.Instance(this);
            OPM = ObjcetPoolManager.Instance(this);
        }

        public void StartGame()
        {

        }

        public void EndGame()
        {

        }

        public void CreateObjectPool<T>(string poolName) where T : ObjectPool, new()
        {
            var pool = OPM.CreateObjectPool<T>(poolName);
            if (IsEmpty.Empty(pool))
                Debug.LogErrorFormat("创建对象池{0}失败", poolName);
        }

        internal void GenerateScene(MapView mv)
        {
            SM.GenerateScene(mv, OPM);            
        }
    }
}
