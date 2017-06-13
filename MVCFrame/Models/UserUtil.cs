using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCFrame.Models
{
   
/// <summary>
    /// 类名：UserInfoUtil
    /// 描述：提供与用户相关的一些属性
    /// 
    /// 作者：钱锋锋
    /// 创建时间： 2012.07.18
    /// 最后修改时间： 2012.07.18 
    /// </summary>
    ///
    [Serializable]
    public class UserUtil  
    {

        #region Model
        private Guid _id;
        private string _username;
        private string _userpwd;
        private string _employeename;
        private Guid _roleid;
        private string _rolename;
        private int? _issys;
        private bool _isRememberMe;
        private string _comment;
        private DateTime _entrydate;
        private Guid _entrypersonnel;
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string UserPwd
        {
            set { _userpwd = value; }
            get { return _userpwd; }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string EmployeeName
        {
            set { _employeename = value; }
            get { return _employeename; }
        }
        /// <summary>
        /// 关联角色
        /// </summary>
        public Guid RoleId
        {
            set { _roleid = value; }
            get { return _roleid; }
        }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName
        {
            set { _rolename = value; }
            get { return _rolename; }
        }
        /// <summary>
        /// 是否系统预置
        ///0  否
        ///1  是
        /// </summary>
        public int? IsSys
        {
            set { _issys = value; }
            get { return _issys; }
        }
        /// <summary>
        /// 是否停用
        /// 0 启用
        /// 1 停用
        /// </summary>
        public bool IsRememberMe
        {
            set { _isRememberMe = value; }
            get { return _isRememberMe; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Comment
        {
            set { _comment = value; }
            get { return _comment; }
        }
        /// <summary>
        /// 入档人
        /// </summary>
        public Guid EntryPersonnel
        {
            set { _entrypersonnel = value; }
            get { return _entrypersonnel; }
        }
        /// <summary>
        /// 入档时间
        /// </summary>
        public DateTime EntryDate
        {
            set { _entrydate = value; }
            get { return _entrydate; }
        }
        #endregion Model

    }
}
