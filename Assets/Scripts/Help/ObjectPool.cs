using Assets.Scripts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Help
{
    public class ObjectPool
    {
        private Queue<GameObject> poolQueue;


        private string poolName;

        protected Transform parent;

        // 需要缓存的对象
        private GameObject prefab;

        // 最大容量
        private int maxCount;

        protected int defaultMaxCount = 10;

        public GameObject Prefab { get; set; }

        public ObjectPool()
        {
            maxCount = defaultMaxCount;
            poolQueue = new Queue<GameObject>();
        }

        public virtual void Init(string poolName, Transform transform)
        {
            this.poolName = poolName;
            this.parent = transform;
        }

        public virtual GameObject Get(Vector3 pos, float lifetime)
        {
            if (lifetime < 0)
            {
                return null;
            }
            GameObject returnObj;
            if (poolQueue.Count > 0)
            {
                returnObj = poolQueue.Dequeue();
            }
            else
            {
                // 池中没有可分配对象了，新生成一个
                returnObj = GameObject.Instantiate<GameObject>(prefab);
                returnObj.transform.SetParent(parent);
                returnObj.SetActive(false);
            }
            // 使用PrefabInfo脚本保存returnObj的一些信息
            ObjectInfo info = returnObj.GetComponent<ObjectInfo>();
            if (info == null)
            {
                info = returnObj.AddComponent<ObjectInfo>();
            }

            info.PoolName = poolName;
            if (lifetime > 0)
            {
                info.Lifetime = lifetime;
            }

            returnObj.transform.position = pos;
            returnObj.SetActive(true);

            return returnObj;
        }

        // "销毁对象" 其实是回收对象
        public virtual void Remove(GameObject obj)
        {
            if (poolQueue.Contains(obj))
            {
                return;
            }

            if (poolQueue.Count > maxCount)
            {
                // 对象池已满 直接销毁
                GameObject.Destroy(obj);
            }
            else
            {
                // 放入对象池
                poolQueue.Enqueue(obj);
                obj.SetActive(false);
            }
        }

        public virtual void Destroy()
        {
            poolQueue.Clear();
        }


    }
}
