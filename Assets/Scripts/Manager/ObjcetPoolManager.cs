using Assets.Scripts.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class ObjcetPoolManager
    {
        #region 单例
        public static GameConsole GC = null;
        private static ObjcetPoolManager instance = null;
        private static readonly object padlock = new object();

        private ObjcetPoolManager()
        {
            Init();
        }

        public static ObjcetPoolManager Instance(GameConsole gc = null)
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    // 如果类的实例不存在则创建，否则直接返回
                    if (instance == null)
                    {
                        instance = new ObjcetPoolManager();
                        GC = gc;
                    }
                }
            }
            return instance;
        }
        #endregion

        private Dictionary<string, ObjectPool> poolDic;
        private Transform rootPoolTrans;

        private void Init()
        {
            poolDic = new Dictionary<string, ObjectPool>();

            // 根对象池
            GameObject go = new GameObject("ObjcetPoolManager");
            rootPoolTrans = go.transform;
        }

        // 创建一个新的对象池
        public T CreateObjectPool<T>(string poolName) where T : ObjectPool, new()
        {
            if (poolDic.ContainsKey(poolName))
            {
                return poolDic[poolName] as T;
            }

            GameObject obj = new GameObject(poolName);
            obj.transform.SetParent(rootPoolTrans);
            T pool = new T();
            pool.Init(poolName, obj.transform);
            poolDic.Add(poolName, pool);
            return pool;
        }

        public GameObject GetGameObject(string poolName, Vector3 position, float lifetTime)
        {
            if (poolDic.ContainsKey(poolName))
            {
                return poolDic[poolName].Get(position, lifetTime);
            }
            return null;
        }

        public void RemoveGameObject(string poolName, GameObject go)
        {
            if (poolDic.ContainsKey(poolName))
            {
                poolDic[poolName].Remove(go);
            }
        }

        // 销毁所有对象池
        public void Destroy()
        {
            poolDic.Clear();
            GameObject.Destroy(rootPoolTrans);
        }
    }
}
