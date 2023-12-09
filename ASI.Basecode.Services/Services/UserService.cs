using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.Manager;
using ASI.Basecode.Services.ServiceModels;
using AutoMapper;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static ASI.Basecode.Resources.Constants.Enums;

namespace ASI.Basecode.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IForgotPass _forgotPass;

        public UserService(IUserRepository repository, IMapper mapper, IForgotPass forgotPass)
        {
            _mapper = mapper;
            _repository = repository;
            _forgotPass = forgotPass;
        }

        public LoginResult AuthenticateUser(string userId, string password, ref User user)
        {
            user = new User();
            var passwordKey = PasswordManager.EncryptPassword(password);
            user = _repository.GetUsers().Where(x => x.UserId == userId &&
                                                     x.Password == passwordKey).FirstOrDefault();

            return user != null ? LoginResult.Success : LoginResult.Failed;
        }

        public List<UserViewModel> GetUsers()
        {
            var data = _repository.GetUsers().Select(u => new UserViewModel
            {
                Id = u.Id,
                UserId = u.UserId,
                Name = u.Name,
                CreatedBy = u.CreatedBy,
                CreatedTime = u.CreatedTime,
                UpdatedBy= u.UpdatedBy,
                UpdatedTime = u.UpdatedTime,
                Email = u.Email,

            }).ToList();
            return data;
        }

        public UserViewModel GetUser(int Id)
        {
            var data = _repository.GetUser(Id);
            if (data != null)
            {
                UserViewModel user = new()
                {
                    Id = data.Id,
                    UserId = data.UserId,
                    Name = data.Name,
                    Password = PasswordManager.DecryptPassword(data.Password),
                    CreatedBy = data.CreatedBy,
                    CreatedTime = data.CreatedTime,
                    UpdatedBy = data.UpdatedBy,
                    UpdatedTime = data.UpdatedTime,
                    Email = data.Email,

                };
                return user;
            }
            return null;
        }

        public bool CheckEmail(UserViewModel userViewModel, string host)
        {
            User user= _repository.GetUsers().Where(x =>x.Email == userViewModel.Email).FirstOrDefault();
            if (user != null)
            {
                user.Code = Guid.NewGuid().ToString();
                _repository.UpdateUser(user);
                _forgotPass.ChangePass(user.Email, host, user.Name, user.Code);
                return true;
            }
            return false;
        }

        public bool PasswordParameter(string email, string code)
        {
            User user = _repository.GetUsers().Where(x => x.Email== email && x.Code == code).FirstOrDefault();

            if (user != null)
            {
                return true;
            }
            return false;
        }

        public bool PasswordReset(UserViewModel userViewModel)
        {
            User user = _repository.GetUsers().Where(x => x.Email== userViewModel.Email && x.Code == userViewModel.Code).FirstOrDefault();
            if (user != null)
            {
                user.Password = PasswordManager.EncryptPassword(userViewModel.Password);
                user.Code = null;
                _repository.UpdateUser(user);
                return true;
            }
            return false;
        }

        public void AddUser(UserViewModel model)
        {
            var user = new User();
            if (!_repository.UserExists(model.UserId))
            {
                _mapper.Map(model, user);
                user.Name = model.Name;
                user.Password = PasswordManager.EncryptPassword(model.Password);
                user.CreatedTime = DateTime.Now;
                user.UpdatedTime = DateTime.Now;
                user.CreatedBy = System.Environment.UserName;
                user.UpdatedBy = System.Environment.UserName;

                _repository.AddUser(user);
            }
            else
            {
                throw new InvalidDataException(Resources.Messages.Errors.UserExists);
            }
        }

        public void UpdateUser(UserViewModel model, string name)
        {
            User user = _repository.GetUser(model.Id);
            if (user != null)
            {
                user.UserId = model.UserId;
                user.Name = model.Name;
                user.UpdatedBy = name;
                user.UpdatedTime = DateTime.Now;
                user.Email= model.Email;
                
                _repository.UpdateUser(user);
            }
            
        }
        public void DeleteUser(UserViewModel model)
        {
            User user = _repository.GetUser(model.Id);
            if (user != null)
            {
                _repository.DeleteUser(user);
            }
        }
    }
}
