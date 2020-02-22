using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Help
{
    public static class IsEmpty
    {
        public static bool Empty(object obj)
        {
            return obj == null;
        }

        public static bool Empty(string str)
        {
            return str == "";
        }
    }
}
