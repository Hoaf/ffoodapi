using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataService.Enum
{
 
    public enum RoleEnum
    {
        [Display(Name = "Người dùng")]
        User = 1,
        [Display(Name = "Quản trị hệ thống")]
        Admin = 2,
    }
    public static class StatusMethod
    {
        public const string SUCCESS = "Success";
        public const string FAILED = "Failed"; 
    }
    public enum Sex
    {
        [Display(Name = "Nam")]
        Male = 1,
        [Display(Name = "Nữ")]
        Female = 2,
        [Display(Name = "Chưa xác định")]
        Undefined = 3,
    }

}
