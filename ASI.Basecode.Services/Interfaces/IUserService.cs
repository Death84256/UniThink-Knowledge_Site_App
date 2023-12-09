using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.ServiceModels;
using System.Collections.Generic;
using static ASI.Basecode.Resources.Constants.Enums;

namespace ASI.Basecode.Services.Interfaces
{
    public interface IUserService
    {
        public LoginResult AuthenticateUser(string userid, string password, ref User user);
        public void AddUser(UserViewModel model);
        public List<UserViewModel> GetUsers();
        public bool CheckEmail(UserViewModel userViewModel, string host);
        public bool PasswordParameter(string email, string code);
        public bool PasswordReset(UserViewModel userViewModel);
        void UpdateUser(UserViewModel model, string name);
        UserViewModel GetUser(int Id);
        void DeleteUser(UserViewModel model);
    }
}
