using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftHelper.Helper
{
    public class DataBaseHelper
    {


        private volatile static DataBaseHelper instance = null;
        private static readonly object lockHelper = new object();

       public static SqlLiteHelper MyDatabase = new SqlLiteHelper(@"Data Source="+ @"D:\AziouStudio\sqllit\MyFirstDataBase.db" + ";Pooling=true;FailIfMissing=false");

        private DataBaseHelper() { }
        public static DataBaseHelper GetInstance()
        {
            if (instance == null)
            {
                lock (lockHelper)
                {
                    if (instance == null)
                    {
                        instance = new DataBaseHelper();
                    }
                }
            }
            return instance;
        }
        public bool InsertSql(string sql)
        {
            bool result = false;
            result = MyDatabase.Query(sql);
            return result;
        }

        public string GetSingelData(string keyName, string sql)
        {
            string result = "";
            result = MyDatabase.GetSingelData(keyName, sql);
            return result;
        }

        public List<string> GetSingelDataList(string keyName, string sql)
        {
            List<string> resultList = new List<string>();
            resultList = MyDatabase.GetSingelDataList(keyName, sql);

            return resultList;

        }

    }
}
