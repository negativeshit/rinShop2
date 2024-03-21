using System;
namespace SV19T1021242.DomainModels
{
    /// <summary>
    /// Thông tin tài khoản trong CSDL
    /// </summary>
    public class UserAccount
    {
        public string UserID { get; set; } = "";
        public string UserName { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Photo { get; set; } = "";
        public string Password { get; set; } = "";
        public string RoleNames { get; set; } = "";
    }
}

