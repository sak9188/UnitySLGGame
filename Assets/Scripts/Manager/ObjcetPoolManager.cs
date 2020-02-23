using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private void Init()
        {

        }
    }
}
