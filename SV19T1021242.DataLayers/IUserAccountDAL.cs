using System;
using SV19T1021242.DomainModels;

namespace SV19T1021242.DataLayers
{
    public interface IUserAccountDAL
    {
        UserAccount? Authorize(string userName, string password);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }
}

