using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftHelper
{
    
        public class SingletonBase<T>
       where T : new()
        {

            private static T instance;
            private static object syncRoot = new Object();
            /// <summary>
            /// 获取对象的单例实例
            /// </summary>
            public static T Instance
            {
                get
                {
                    if (instance == null)
                    {
                        lock (syncRoot)
                        {
                            if (instance == null)
                                instance = new T();
                        }
                    }
                    return instance;
                }
            }


        
    }
}
