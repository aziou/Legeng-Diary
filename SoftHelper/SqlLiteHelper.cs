using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Collections.ObjectModel;
using DiaryModel;
namespace SoftHelper
{
    public class SqlLiteHelper
    {
        string connectionstring = string.Empty;//Data Source=test.db;Pooling=true;FailIfMissing=false


        private SQLiteConnection conn=null;
        public SqlLiteHelper(string SqlPath)
        {
            connectionstring = SqlPath;
            conn = new SQLiteConnection(connectionstring);
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
        }

        public bool InsertData(List<string> sqlList)
        {
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(); //声明命令对象
            SQLiteTransaction _Tx = conn.BeginTransaction(); //实例化事务
            cmd.Connection = conn;
            cmd.Transaction = _Tx;
            int int_i = 0;
            try
            {
                DateTime startTime = DateTime.Now;
                for (int i = 0; i < sqlList.Count; i++)
                {
                     string _sqlString="";
                    _sqlString = sqlList[i].ToString();
                    cmd.CommandText = _sqlString;
                    if (cmd.ExecuteNonQuery() < 1 && _sqlString.ToUpper().IndexOf("DELETE") == -1)
                    {


                    }
                  
                }
                _Tx.Commit();
                _Tx.Dispose();
                cmd.Dispose();
                return true;
            }
            catch (SQLiteException e)
            {
                _Tx.Rollback();
                _Tx.Dispose();
                cmd.Dispose();
                return false;
            }
            catch (Exception ex)
            {
                _Tx.Rollback();
                _Tx.Dispose();
                cmd.Dispose();
                return false;
            }
            finally
            {

            }

        }

        public bool Query(string exSql)
        {
            conn = new SQLiteConnection(connectionstring);
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();

            SQLiteCommand cmd = new SQLiteCommand();
            SQLiteTransaction _tx =conn.BeginTransaction();
            cmd.Connection = conn;
            cmd.Transaction = _tx;
            try
            {
                string sql = exSql;
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() < -1)
                {

                }

                return true;
            }
            catch (SQLiteException e)
            {
                _tx.Rollback();
                _tx.Dispose();
                cmd.Dispose();

                return false;
            }
            catch (Exception ex)
            {
                _tx.Rollback();
                _tx.Dispose();
                cmd.Dispose();

                return false;
            }

        }

        public string GetSingelData(string keyName,string SqlWord)
        {
            string str_value = "";

            conn = new SQLiteConnection(connectionstring);
            if (conn.State == System.Data.ConnectionState.Closed) conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(SqlWord,conn);
            SQLiteDataReader myreader = null;
            myreader = cmd.ExecuteReader();
            while (myreader.Read())
            {
                str_value = myreader[keyName].ToString().Trim();
                break;

            }
         
            cmd.Dispose();

            return str_value;
        }


        public List<string> GetSingelDataList(string keyName, string SqlWord)
        {
            List<string> str_value = new List<string>();

            conn = new SQLiteConnection(connectionstring);
            if (conn.State == System.Data.ConnectionState.Closed) conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(SqlWord, conn);
            SQLiteDataReader myreader = null;
            myreader = cmd.ExecuteReader();
            while (myreader.Read())
            {
                if (keyName == "WriteDownDate")
                {
                    str_value.Add(Convert.ToDateTime( myreader[keyName]).ToString("yyyy-MM-dd").Trim());

                }
                else
                {
                    str_value.Add(myreader[keyName].ToString().Trim());

                }


            }

            cmd.Dispose();

            return str_value;
        }

        public Dictionary<string,string> GetDateAndIDDataList( string SqlWord)
        {
            Dictionary<string, string> str_value = new Dictionary<string, string>();

            conn = new SQLiteConnection(connectionstring);
            if (conn.State == System.Data.ConnectionState.Closed) conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(SqlWord, conn);
            SQLiteDataReader myreader = null;
            myreader = cmd.ExecuteReader();
            while (myreader.Read())
            {
               
                    str_value.Add(Convert.ToDateTime(myreader["WriteDownDate"]).ToString("yyyy-MM-dd").Trim(), myreader["UserId"].ToString());

                
             


            }

            cmd.Dispose();

            return str_value;
        }

        public ObservableCollection<DiaryModel.DiaryModel> GetDiaryItem()
        {
            conn = new SQLiteConnection(connectionstring);
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            string sql = "select * from baseinfo order by WriteDownDate desc ";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            SQLiteDataReader myreader = null;
            myreader = cmd.ExecuteReader();

            ObservableCollection<DiaryModel.DiaryModel> tempCol = new ObservableCollection<DiaryModel.DiaryModel>();
            while (myreader.Read())
            {
                tempCol.Add(new DiaryModel.DiaryModel()
                {
                    diaryType = Convert.ToInt16( myreader["DiaryType"].ToString().Trim()),
                    UserName = myreader["UserName"].ToString().Trim(),
                    writeDate = Convert.ToDateTime( myreader["WriteDownDate"].ToString().Trim()),
                    itemTitle = myreader["DiaryTitle"].ToString().Trim(),
                    diaryContent=myreader["DiaryContext"].ToString().Trim(),
                });
            }

            conn.Clone();
            return tempCol;
        }
    }
}
