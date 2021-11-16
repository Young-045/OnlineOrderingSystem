using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataDal
{
    public class Db
    {
        static string DbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database");

        private static SQLiteConnection CreateDatabaseConnection(string dbName = null)
        {
            if (!string.IsNullOrEmpty(DbPath) && !Directory.Exists(DbPath))
                Directory.CreateDirectory(DbPath);
            dbName = dbName == null ? "database.db" : dbName;
            var dbFilePath = Path.Combine(DbPath, dbName);
            return new SQLiteConnection("DataSource = " + dbFilePath);
        }

        // 使用全局静态变量保存连接
        private static SQLiteConnection connection = CreateDatabaseConnection();

        private static void Open(SQLiteConnection connection)
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
        }

        public static bool ExecuteNonQuery(string sql)
        {
            // 确保连接打开
            Open(connection);
            int flag = -1;
            try
            {
                using (var tr = connection.BeginTransaction())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = sql;
                        flag = command.ExecuteNonQuery();
                    }
                    tr.Commit();
                }
            }
            catch(Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return flag > 0 ? true : false;
        }

        public static bool ImportExecute(List<string> sqls)
        {
            // 确保连接打开
            Open(connection);
            int flag = -1;
            try
            {
                using (var tr = connection.BeginTransaction())
                {
                    foreach (var sql in sqls)
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = sql;
                            flag = command.ExecuteNonQuery();
                        }
                    }                   
                    tr.Commit();
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return flag > 0 ? true : false;
        }



        public static SQLiteDataReader ExecuteQuery(string sql)
        {
            // 确保连接打开
            Open(connection);
            SQLiteDataReader res=null;
            try
            {
                using (var tr = connection.BeginTransaction())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = sql;

                        // 执行查询会返回一个SQLiteDataReader对象
                        res = command.ExecuteReader();


                    }
                    tr.Commit();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);

            }
            return res;
        }

        public static string GenerateMD5(string txt)
        {
            using (MD5 mi = MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(txt);
                //开始加密
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }


    }
}
