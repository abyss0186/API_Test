using AutoMapperDemo01.Models;
using Dapper;
//using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AutoMapperDemo01.Repository
{
    public class StudentRepository
    {
        /// <summary>
        /// 連線字串
        /// </summary>                           
        private readonly string _connectString = 
            "Data Source=ITTC-04503-0024\\SQLEXPRESS;Initial Catalog=School;Integrated Security=True";
        
        /// <summary>
        /// 查詢卡片列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Student> GetList()
        {
            var sql = "SELECT * FROM Student";

            using (IDbConnection dbConnection = new SqlConnection(_connectString))
            {
                dbConnection.Open();
                var result = dbConnection.Query<Student>(sql);
                return result;
            }
        }
        /// <summary>
        /// 查詢卡片
        /// </summary>
        /// <returns></returns>
        public Student Get(int ID)
        {
            var sql =
            @"
                SELECT * 
                FROM Student 
                Where ID = @ID
            ";
            var parameters = new DynamicParameters();
            parameters.Add("ID", ID, System.Data.DbType.Int32);

            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.QueryFirstOrDefault<Student>(sql, parameters);
                return result;
            }
        }
        /// <summary>
        /// 新增卡片
        /// </summary>
        /// <param name="parameter">參數</param>
        /// <returns></returns>
        public int Create(StudentParameter parameter)
        {
            var sql =
            @"
            INSERT INTO Student
            (
               [ID]
              ,[Name]
              ,[Age]
              ,[Gender]
            ) 
            VALUES 
            (
                 @ID
                ,@Name
                ,@Age
                ,@Gender
            );
            ";

            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.QueryFirstOrDefault<int>(sql, parameter);
                return result;
            }
        }
        //修改
        /// <summary>
        /// 修改卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <param name="parameter">參數</param>
        /// <returns></returns>
        public bool Update(int ID, StudentParameter parameter)
        {
            var sql =
            @"
            UPDATE Student
            SET 
                 [ID] = @ID
                ,[Name] = @Name
                ,[Age] = @Age
                ,[Gender] = @Gender
            WHERE 
                ID = @ID
        ";

            var parameters = new DynamicParameters(parameter);
            parameters.Add("ID", ID, System.Data.DbType.Int32);

            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.Execute(sql, parameters);
                return result > 0;
            }
        }
        //刪除
        /// <summary>
        /// 刪除卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <returns></returns>
        public void Delete(int ID)
        {
            var sql =
            @"
            DELETE FROM Card
            WHERE ID = @ID
        ";

            var parameters = new DynamicParameters();
            parameters.Add("ID", ID, System.Data.DbType.Int32);

            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.Execute(sql, parameters);
            }
        }
        //controller還沒接起來
    }
}
