using MVC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVC.DAL
{
    public class UserInfoDAL
    {

        public UserInfo GetUserInfoByNameAndPwd(string userName, string userPwd)
        {
            string sql = "Select * from UserInfo Where UserName=@UserName AND UserPwd=@UserPwd";
            return  Dapper.DapperBase.Instance.FirstOrDefault<UserInfo>(sql, new { UserName = userName, UserPwd = userPwd });
        }



        public bool EditPassWord(string id, string newPwd)
        {
            string sql = "update UserInfo Set UserPwd=@UserPwd  Where Id=@Id";
            return Dapper.DapperBase.Instance.Execute(sql, new { Id = id, UserPwd = newPwd });
        }

        public List<UserInfo> GetAll()
        {
            string sql = "Select * from UserInfo ";
            return Dapper.DapperBase.Instance.Query<UserInfo>(sql, null).ToList();
        
        }
    }
}
