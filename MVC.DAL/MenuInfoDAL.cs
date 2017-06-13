using MVC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVC.DAL
{
   public class MenuInfoDAL
    {

       public List<MenuInfo> GetByParentId(int parentid)
       {
           string sql = "Select * from MenuInfo Where ParentId=@ParentId ";
           IEnumerable<MenuInfo> query  = Dapper.DapperBase.Instance.Query<MenuInfo>(sql, new { ParentId = parentid });
           if (query != null)
               return query.ToList();
           else return null;
       }


       public List<MenuInfo> GetAll()
       {
           string sql = "Select * from MenuInfo ";
           IEnumerable<MenuInfo> query = Dapper.DapperBase.Instance.Query<MenuInfo>(sql,null);
           if (query != null)
               return query.ToList();
           else return null;
       }

    }
}
