using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace MVC.Dapper
{
    public class DapperBase:IDisposable
    {
        private static volatile DapperBase instance;
        public static DapperBase Instance 
        {
            get
            {
                if (instance == null)
                {
                     
                        if (instance == null)
                        {
                            instance = new DapperBase();
                        }
                   
                }
                return instance;
            }
        }

        private DapperBase()
        {
        }

        private  readonly string _dbConnectionStr = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;

        private  IDbConnection dbConnection;

        public IDbConnection DbConnection
        {
            get
            {
                if ( !String.IsNullOrEmpty(_dbConnectionStr))
                    dbConnection = new SqlConnection(_dbConnectionStr);
                else
                    throw new ArgumentNullException("dbConnectionStr");

                bool isClosed = dbConnection.State == ConnectionState.Closed;
                if (isClosed) dbConnection.Open();
                return dbConnection;
            }
        }

        /// <summary>
        /// 执行增删改操作(包括批量操作)
        /// </summary>
        /// <param name="sql">sql语句(有参数参数化)</param>
        /// <param name="param">参数化值</param>
        /// <returns></returns>
        public bool Execute(string sql, object param)
        {
            bool isSuccess = false;
            if ( !String.IsNullOrEmpty(sql))
            {
                using (DbConnection)
                {
                    try
                    {
                        int result = DbConnection.Execute(sql, param);
                        isSuccess = result > 0 ? true : false;
                    }
                    catch
                    {
                        isSuccess = false;
                    }
                }
            }
            return isSuccess;
        }

        /// <summary>
        /// 执行存储过程操作
        /// </summary>
        /// <param name="sql">存储过程名称</param>
        /// <param name="param">参数化值</param>
        /// <returns>返回存储过程是否执行成功</returns>
        public bool ExecuteStored(string storedName, object param)
        {
            bool isSuccess = false;
            if (!String.IsNullOrEmpty(storedName))
            {
                using (DbConnection)
                {
                    try
                    {
                        int result = DbConnection.Execute(storedName, param, commandType: CommandType.StoredProcedure);
                        isSuccess = result > 0 ? true : false;
                    }
                    catch
                    {
                        isSuccess = false;
                    }
                }
            }
            return isSuccess;
        }

        /// <summary>
        /// 执行存储过程操作
        /// </summary>
        /// <param name="storedName">存储过程名称</param>
        /// <param name="param">存储过程参数</param>
        /// <returns>返回存储过程要返回的值</returns>
        public DynamicParameters ExecuteStored(string storedName, DynamicParameters param)
        {
            if (!String.IsNullOrEmpty(storedName))
            {
                using (DbConnection)
                {
                    try
                    {
                        DbConnection.Execute(storedName, param, commandType: CommandType.StoredProcedure);
                    }
                    catch { }
                }
            }
            return param;
        }


        /// <summary>
        /// 查询操作
        /// </summary>
        /// <typeparam name="T">返回集合的类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数化值</param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string sql, object param)
        {
            IEnumerable<T> _list = default(IEnumerable<T>);
            if (!string.IsNullOrEmpty(sql))
            {
                using (DbConnection)
                {
                    try
                    {
                        _list = DbConnection.Query<T>(sql, param);
                    }
                    catch { }

                }
            }
            return _list;
        }

        /// <summary>
        /// 执行存储过程查询操作
        /// </summary>
        /// <typeparam name="T">返回集合的类型</typeparam>
        /// <param name="storedName">存储过程</param>
        /// <param name="param">参数化值</param>
        /// <returns></returns>
        public IEnumerable<T> QueryStored<T>(string storedName, object param)
        {
            IEnumerable<T> _list = default(IEnumerable<T>);
            if (!string.IsNullOrEmpty(storedName))
            {
                using (DbConnection)
                {
                    try
                    {
                        _list = DbConnection.Query<T>(storedName, commandType: CommandType.StoredProcedure);
                    }
                    catch { }
                }
            }
            return _list;
        }

        /// <summary>
        /// 查询操作返回默认第一条数据(如返回null则创建默认类型)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public T FirstOrDefault<T>(string sql, object param)
        {
            var model = default(T);
            if (!string.IsNullOrEmpty(sql))
            {
                using (DbConnection)
                {
                    try
                    {
                        model = DbConnection.Query<T>(sql, param).FirstOrDefault();
                    }
                    catch { }
                }
            }
            return model == null ? Activator.CreateInstance<T>() : model;
        }



        /// <summary>
        /// 查询一组SQL语句并返回值
        /// </summary>
        /// <typeparam name="T1">第一条语句返回集合类型</typeparam>
        /// <typeparam name="T2">第二条语句返回集合类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数化值</param>
        /// <returns></returns>
        public Tuple<IEnumerable<T1>, IEnumerable<T2>> Query<T1, T2>(string sql, object param)
        {
            IEnumerable<T1> _item1 = null; IEnumerable<T2> _item2 = null;
            if (!string.IsNullOrEmpty(sql))
            {
                using (DbConnection)
                {
                    try
                    {
                        using (var multi = DbConnection.QueryMultiple(sql, param))
                        {
                            _item1 = multi.Read<T1>();
                            _item2 = multi.Read<T2>();
                        }
                    }
                    catch { }
                }
            }
            return Tuple.Create<IEnumerable<T1>, IEnumerable<T2>>(_item1, _item2);
        }

        /// <summary>
        /// 查询一组SQL语句并返回值
        /// </summary>
        /// <typeparam name="T1">第一条语句返回集合类型</typeparam>
        /// <typeparam name="T2">第二条语句返回集合类型</typeparam>
        /// <typeparam name="T3">第三条语句返回集合类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数化值</param>
        /// <returns></returns>
        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> Query<T1, T2, T3>(string sql, object param)
        {
            IEnumerable<T1> _item1 = null; IEnumerable<T2> _item2 = null; IEnumerable<T3> _item3 = null;
            if (!string.IsNullOrEmpty(sql))
            {
                using (DbConnection)
                {
                    try
                    {
                        using (var multi = DbConnection.QueryMultiple(sql, param))
                        {
                            _item1 = multi.Read<T1>();
                            _item2 = multi.Read<T2>();
                            _item3 = multi.Read<T3>();
                        }
                    }
                    catch { }
                }
            }
            return Tuple.Create<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>(_item1, _item2, _item3);
        }



        public void Dispose()
        {
            if (dbConnection != null)
            {
                try
                {
                    bool isClosed = dbConnection.State == ConnectionState.Closed;
                    if (!isClosed) dbConnection.Close();
                    //dbConnection.Dispose();
                }
                catch { }
            }
        }

        //public void TransactionExcute()
        //{

        //    using (dbConnection)
        //    {
        //        //开始事务
        //        IDbTransaction transaction = dbConnection.BeginTransaction();
        //        try
        //        {
        //            string query = "DELETE FROM Book WHERE id = @id";
        //            string query2 = "DELETE FROM BookReview WHERE BookId = @BookId";
        //            dbConnection.Execute(query2, new { BookId = id }, transaction, null, null);
        //            dbConnection.Execute(query, new { id = id }, transaction, null, null);
        //            //提交事务
        //            transaction.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            //出现异常，事务Rollback
        //            transaction.Rollback();
        //            throw new Exception(ex.Message);
        //        }
        //    }
        
        //}

    }
 
}
