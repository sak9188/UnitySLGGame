using Assets.Scripts.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Base
{
    public class ObjectInfo : MonoBehaviour
    {
        public float Lifetime = 0;
        public string PoolName;

        private WaitForSeconds waitTime;

        private void Awake()
        {
            if (Lifetime > 0)
            {
                waitTime = new WaitForSeconds(Lifetime);
            }
        }

        private void OnEnable()
        {
            if (Lifetime > 0)
            {
                StartCoroutine(CountDown(Lifetime));
            }
        }

        IEnumerator CountDown(float lifetime)
        {
            yield return waitTime;
            ObjcetPoolManager.Instance().RemoveGameObject(PoolName, gameObject);
        }
        
    }
}
