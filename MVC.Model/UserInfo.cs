using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVC.Model
{
   /// <summary>
	/// 用户表
	/// </summary>
	[Serializable]
	public partial class UserInfo
	{
		public UserInfo()
		{}
		#region Model
		private Guid _id;
		private string _username;
		private string _userpwd;
		private string _employeename;
		private bool _isdisabled;
		private string _comment;
		private Guid _roleid;
		private int? _issys;
		private DateTime? _entrydate;
		private Guid _entrypersonnel;
		/// <summary>
		/// Id
		/// </summary>
		public Guid Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 用户名
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 密码
		/// </summary>
		public string UserPwd
		{
			set{ _userpwd=value;}
			get{return _userpwd;}
		}
		/// <summary>
		/// 用户姓名
		/// </summary>
		public string EmployeeName
		{
			set{ _employeename=value;}
			get{return _employeename;}
		}
		/// <summary>
		/// 是否停用
		/// </summary>
		public bool IsDisabled
		{
			set{ _isdisabled=value;}
			get{return _isdisabled;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Comment
		{
			set{ _comment=value;}
			get{return _comment;}
		}
		/// <summary>
		/// 关联角色
		/// </summary>
		public Guid RoleId
		{
			set{ _roleid=value;}
			get{return _roleid;}
		}
		/// <summary>
		/// 是否系统预置
        ///0  否
        ///1  是
		/// </summary>
		public int? IsSys
		{
			set{ _issys=value;}
			get{return _issys;}
		}
		/// <summary>
		/// 入档时间
		/// </summary>
		public DateTime? EntryDate
		{
			set{ _entrydate=value;}
			get{return _entrydate;}
		}
		/// <summary>
		/// 入档人
		/// </summary>
		public Guid EntryPersonnel
		{
			set{ _entrypersonnel=value;}
			get{return _entrypersonnel;}
		}
		#endregion Model

	}
}
