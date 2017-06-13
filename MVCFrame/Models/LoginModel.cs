using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCFrame.Models
{
    [Serializable]
    public class LoginModel
    {
 
        [StringLength(32)]
        [Required]
        [Display(Name = "用户名")]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [StringLength(32)]
        [Required]
        [Display(Name = "登录密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsRememberMe { get; set; }

       
    }
}