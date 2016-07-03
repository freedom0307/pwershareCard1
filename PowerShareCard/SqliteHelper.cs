using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace PowerShareCard
{
    public class SqliteHelper
    {
        #region ExecuteNonQuery
        /// <summary>   
        /// 执行 SQL 语句并返回受影响的行数   
        /// </summary>   
        /// <param name="connString">数据库连接串</param>   
        /// <param name="commandText">SQL语句</param>   
        /// <returns>受影响的行数</returns>   
        public static int ExecuteNonQuery(string connString, string commandText)
        {
            return ExecuteNonQuery(connString, commandText, (SQLiteParameter[])null);
        }

        /// <summary>   
        /// 执行 SQL 语句并返回受影响的行数   
        /// </summary>   
        /// <param name="connString">数据库连接串</param>   
        /// <param name="commandText">SQL语句</param>   
        /// <param name="paras">SQL语句参数</param>   
        /// <returns>受影响的行数</returns>   
        public static int ExecuteNonQuery(string connString, string commandText, params SQLiteParameter[] paras)
        {
            int count = 0;
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                SQLiteTransaction tran = null;
                SQLiteCommand cmd = new SQLiteCommand(conn);

                cmd.CommandText = commandText;
                if (paras != null) cmd.Parameters.AddRange(paras);

                #region 事务处理
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();
                    cmd.Transaction = tran;
                    count = cmd.ExecuteNonQuery();
                    tran.Commit();
                }
                catch
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                    throw;
                }
                finally
                {
                    if (tran != null)
                    {
                        tran.Dispose();
                    }
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
                #endregion
            }
            return count;
        }
        #endregion

        #region ExecuteReader
        /// <summary>   
        /// 将 CommandText 发送到 Connection 并生成一个 IDataReader   
        /// </summary>   
        /// <param name="connString">数据库连接串</param>   
        /// <param name="commandText">SQL语句</param>   
        /// <returns>IDataReader</returns>   
        public static IDataReader ExecuteReader(string connString, string commandText)
        {
            return ExecuteReader(connString, commandText, (SQLiteParameter[])null);
        }

        /// <summary>   
        /// 将 CommandText 发送到 Connection 并生成一个 IDataReader   
        /// </summary>   
        /// <param name="connString">数据库连接串</param>   
        /// <param name="commandText">SQL语句</param>   
        /// <param name="paras">SQL语句参数</param>   
        /// <returns>IDataReader</returns>   
        public static IDataReader ExecuteReader(string connString, string commandText, params SQLiteParameter[] paras)
        {
            SQLiteConnection conn = new SQLiteConnection(connString);

            SQLiteCommand cmd = new SQLiteCommand(conn);

            cmd.CommandText = commandText;
            if (paras != null) cmd.Parameters.AddRange(paras);

            conn.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        #endregion

        #region ExecuteScalar
        /// <summary>   
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。   
        /// </summary>   
        /// <param name="connString">数据库连接串</param>   
        /// <param name="commandText">SQL语句</param>   
        /// <returns>第一行的第一列的数据</returns>   
        public static Object ExecuteScalar(string connString, string commandText)
        {
            return ExecuteScalar(connString, commandText, (SQLiteParameter[])null);
        }

        /// <summary>   
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。   
        /// </summary>   
        /// <param name="connString">数据库连接串</param>   
        /// <param name="commandText">SQL语句</param>   
        /// <param name="paras">SQL语句参数</param>   
        /// <returns>第一行的第一列的数据</returns>   
        public static Object ExecuteScalar(string connString, string commandText, params SQLiteParameter[] paras)
        {
            Object obj = null;
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                SQLiteCommand cmd = new SQLiteCommand(conn);

                cmd.CommandText = commandText;
                if (paras != null) cmd.Parameters.AddRange(paras);

                try
                {
                    conn.Open();
                    obj = cmd.ExecuteScalar();
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
            return obj;
        }
        #endregion

        #region GetDataSet
        /// <summary>   
        /// 执行查询，并返回一个DataSet   
        /// </summary>   
        /// <param name="connString">数据库连接串</param>   
        /// <param name="commandText">SQL语句</param>   
        /// <returns>第一行的第一列的数据</returns>   
        public static DataSet GetDataSet(string connString, string commandText)
        {
            return GetDataSet(connString, commandText, (SQLiteParameter[])null);
        }

        /// <summary>   
        /// 执行查询，并返回一个DataSet   
        /// </summary>   
        /// <param name="connString">数据库连接串</param>   
        /// <param name="commandText">SQL语句</param>   
        /// <param name="paras">SQL语句参数</param>   
        /// <returns>第一行的第一列的数据</returns>   
        public static DataSet GetDataSet(string connString, string commandText, params SQLiteParameter[] paras)
        {
            DataSet ds = new DataSet();
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                SQLiteCommand cmd = new SQLiteCommand(conn);

                cmd.CommandText = commandText;
                if (paras != null) cmd.Parameters.AddRange(paras);

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                adapter.Fill(ds);
            }
            return ds;
        }
        #endregion
    }
}
