using System;
using SV19T1021242.DataLayers;
using SV19T1021242.DataLayers.SQLServer;
using SV19T1021242.DomainModels;

namespace SV19T1021242.BusinessLayers
{
    public class UserAccountService
    {
        private static readonly IUserAccountDAL employeeAccountDB;
        static UserAccountService()
        {
            string connectionString = Configuration.ConnectionString;

            employeeAccountDB = new EmployeeAccountDAL(connectionString);
        }
        public static UserAccount? Authorize(string userName, string password)
        {
            //TODO: Kiểm tra thông tin đăng nhập của Employee
            return employeeAccountDB.Authorize(userName, password);

        }
        public static bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            //TODO: Thay đổi mật khẩu của Employee
            return employeeAccountDB.ChangePassword(userName, oldPassword, newPassword);
        }

    }
}

