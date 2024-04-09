using Application.Models.ModelsOfUser;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Context;
using Infrastructure.Repositories;
using JwtAuthenticationManager;
using JwtAuthenticationManager.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public interface IUserService
    {
        Tuple<string, CombineJwtModel?> GetJwtUser(string email, string password);
        Tuple<string, User?> CreatedUser(RegisterModel registerModel);
        Tuple<string, User?> EditUser(EditUserModel editUserModel);
        Tuple<string, bool> VerifyEmail(string email);
        Tuple<string, User?> SetIsLockUser(string idUser);
        Tuple<string, User?> ResetPassword(string idUser, string newPassword);
        Tuple<string, List<User?>?> GetAllUser();
        Tuple<string, List<User?>?> GetListUser();
        Tuple<string, User?> GetUserById(string id);
        Tuple<string, User?> GetUserByEmail(string email);
        Tuple<string, bool> CheckIsAdmin(string userId);
        Tuple<string, bool> CheckIsMaanager(string userId);
        Tuple<string, bool> CheckIsStudent(string userId);
        Tuple<string, bool> CheckIsStaff(string userId);
    }
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        public UserService(UserDbContext context, IMemoryCache cache)
        {
            unitOfWork = new UnitOfWork(context, cache);
        }

        public Tuple<string, CombineJwtModel?> GetJwtUser(string email, string password)
        {
            var user = unitOfWork.userRepository.Find(e => e.PresentEmail == email).FirstOrDefault();
            if (user == null) { return new Tuple<string, CombineJwtModel?>($"Not exist user with email {email}", null); }
            if (!SecurityMethods.CheckPassword(password, user.PasswordHash))
            {
                return new Tuple<string, CombineJwtModel?>($"Password invalid", null);
            }
            if (user.IsLock == true)
            {
                return new Tuple<string, CombineJwtModel?>($"User with email {email} have been lock", null);
            }
            if (user.IsVerified == false)
            {
                return new Tuple<string, CombineJwtModel?>($"User with email {email} haven't been verify", null);
            }
            user.Role = unitOfWork.roleRepository.GetRoleById(user.RoleId);
            var jwtUserInfor = new UserInfomationModel()
            {
                Id = user.Id,
                Email = user.PresentEmail,
                Avatar = user.Avatar,
                Fullname = user.FirstName + " " + user.LastName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Role = user.Role.Name
            };
            var jwtModel = JwtTokenHandler.GenerateJwtToken(jwtUserInfor);
            return new Tuple<string, CombineJwtModel?>("Login Successful", jwtModel);
        }

        public Tuple<string, User?> CreatedUser(RegisterModel registerModel)
        {
            try
            {
                var role = unitOfWork.roleRepository.GetRoleByName("USER");
                var checkEmailExist = unitOfWork.userRepository.Find(e => e.PresentEmail == registerModel.Email || e.FirstEmail == registerModel.Email).FirstOrDefault();
                if (checkEmailExist != null)
                {
                    return new Tuple<string, User?>("Email have existed", null);
                }
                var user = new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = registerModel.UserName,
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    FirstEmail = registerModel.Email,
                    PhoneNumber = registerModel.PhoneNumber,
                    Avatar = registerModel.AvatarFile,
                    PresentEmail = registerModel.Email,
                    Birthday = registerModel.Birthday,
                    PasswordHash = SecurityMethods.HashPassword(registerModel.Password),
                    CreatedDate = DateTime.Now,
                    IsLock = false,
                    RoleId = role.Id,
                    TokenAccess = SecurityMethods.CreateRandomToken(),
                    VerifiedDate = null,
                    IsVerified = false,
                    Role = role
                };
                unitOfWork.userRepository.Add(user);
                unitOfWork.SaveChange();
                //var result = GetJwtUser(user.PresentEmail, registerModel.Password);
                return new Tuple<string, User?>("Register successful", user);
            }
            catch (Exception e)
            {
                return new Tuple<string, User?>(e.Message, null);
            }
        }

        public Tuple<string, User?> GetUserById(string id)
        {
            try
            {
                if (id.IsNullOrEmpty()) return new Tuple<string, User?>("parameter was null or empty", null);
                var user = unitOfWork.userRepository.GetById(id);
                if (user == null) return new Tuple<string, User?>($"User with id {id} is not exist", null);
                return new Tuple<string, User?>(string.Empty, user);
            }
            catch (Exception e)
            {
                return new Tuple<string, User?>(e.Message, null);
            }
        }

        public Tuple<string, User?> GetUserByEmail(string email)
        {
            try
            {
                if (email.IsNullOrEmpty()) return new Tuple<string, User?>("parameter was null or empty", null);
                var user = unitOfWork.userRepository.Find(e => e.PresentEmail == email).FirstOrDefault();
                if (user == null) return new Tuple<string, User?>($"User with email {email} is not exist", null);
                return new Tuple<string, User?>(string.Empty, user);
            }
            catch (Exception e)
            {
                return new Tuple<string, User?>(e.Message, null);
            }
        }

        public Tuple<string, User?> EditUser(EditUserModel eum)
        {
            try
            {
                if (eum is null) return new Tuple<string, User?>("parameter was null or empty", null);
                var user = unitOfWork.userRepository.GetById(eum.IdUser);
                if (user == null) return new Tuple<string, User?>($"User with email {eum.IdUser} is not exist", null);

                if (!eum.Email.IsNullOrEmpty()) { user.PresentEmail = eum.Email; }
                if (!eum.UserName.IsNullOrEmpty()) { user.UserName = eum.UserName; }
                if (!eum.FirstName.IsNullOrEmpty()) { user.FirstName = eum.FirstName; }
                if (!eum.LastName.IsNullOrEmpty()) { user.LastName = eum.LastName; }
                if (!eum.PhoneNumber.IsNullOrEmpty()) { user.PhoneNumber = eum.PhoneNumber; }
                if (!eum.LinkAvatar.IsNullOrEmpty()) { user.PresentEmail = eum.LinkAvatar; }
                if (!eum.Avatar.IsNullOrEmpty()) { user.Avatar = eum.Avatar; }

                unitOfWork.userRepository.Update(user);
                unitOfWork.SaveChange();
                return new Tuple<string, User?>("Update profile is successful", user);
            }
            catch (Exception e)
            {
                return new Tuple<string, User?>(e.Message, null);
            }
        }

        public Tuple<string, User?> SetIsLockUser(string idUser)
        {
            if (idUser.IsNullOrEmpty()) return new Tuple<string, User?>("parameter was null or empty", null);
            var user = unitOfWork.userRepository.GetById(idUser);
            if (user == null) return new Tuple<string, User?>($"User with id {idUser} is not exist", null);
            user.IsLock = !user.IsLock;
            unitOfWork.userRepository.Update(user);
            unitOfWork.SaveChange();
            return new Tuple<string, User?>($"Locking User with email {user.PresentEmail} is successful", user);
        }

        public Tuple<string, User?> ResetPassword(string idUser, string newPassword)
        {
            try
            {
                if (idUser.IsNullOrEmpty() || newPassword.IsNullOrEmpty()) return new Tuple<string, User?>("parameter was null or empty", null);
                var user = unitOfWork.userRepository.GetById(idUser);
                if (user == null) return new Tuple<string, User?>($"User with id {idUser} is not exist", null);
                user.PasswordHash = SecurityMethods.HashPassword(newPassword);
                unitOfWork.userRepository.Update(user);
                unitOfWork.SaveChange();
                return new Tuple<string, User?>($"{user.UserName} changing password is successful", user);
            }
            catch (Exception e)
            {
                return new Tuple<string, User?>(e.Message, null);
            }
        }

        public Tuple<string, bool> VerifyEmail(string id)
        {
            try
            {
                if (id.IsNullOrEmpty()) return new Tuple<string, bool>("parameter is null", false);
                var result = GetUserById(id);
                if (result.Item2 == null) return new Tuple<string, bool>(result.Item1, false);
                var user = result.Item2;
                user.VerifiedDate = DateTime.Now;
                user.IsVerified = true;
                unitOfWork.userRepository.Update(user);
                unitOfWork.SaveChange();
                return new Tuple<string, bool>("successful", true);
            }
            catch (Exception)
            {
                return new Tuple<string, bool>("Error", false);
            }
        }

        public Tuple<string, bool> CheckIsAdmin(string userId)
        {
            if (userId.IsNullOrEmpty()) return new Tuple<string, bool>("parameter was null or empty", false);
            var user = unitOfWork.userRepository.GetById(userId);
            if (user == null) return new Tuple<string, bool>($"User with id {userId} is not exist", false);

            var role = unitOfWork.roleRepository.Find(r => r.Name == "ADMIN").FirstOrDefault();
            if (role == null) return new Tuple<string, bool>("Role not found", false);
            if (user.Role != role) return new Tuple<string, bool>($"You are not {role.NormalizeName}", false);
            return new Tuple<string, bool>(string.Empty, true);
        }

        public Tuple<string, bool> CheckIsMaanager(string userId)
        {
            if (userId.IsNullOrEmpty()) return new Tuple<string, bool>("parameter was null or empty", false);
            var user = unitOfWork.userRepository.GetById(userId);
            if (user == null) return new Tuple<string, bool>($"User with id {userId} is not exist", false);

            var role = unitOfWork.roleRepository.Find(r => r.Name == "MANAGER").FirstOrDefault();
            if (role == null) return new Tuple<string, bool>("Role not found", false);
            if (user.Role != role) return new Tuple<string, bool>($"You are not {role.NormalizeName}", false);
            return new Tuple<string, bool>(string.Empty, true);
        }

        public Tuple<string, List<User?>?> GetAllUser()
        {
            var allUser = unitOfWork.userRepository.GetAll();
            if (allUser == null) return new Tuple<string, List<User?>?>("No user", null);
            return new Tuple<string, List<User?>?>("", allUser);
        }

        public Tuple<string, bool> CheckIsStudent(string userId)
        {
            if (userId.IsNullOrEmpty()) return new Tuple<string, bool>("parameter was null or empty", false);
            var user = unitOfWork.userRepository.GetById(userId);
            if (user == null) return new Tuple<string, bool>($"User with id {userId} is not exist", false);

            var role = unitOfWork.roleRepository.Find(r => r.Name == "STUDENT").FirstOrDefault();
            if (role == null) return new Tuple<string, bool>("Role not found", false);
            if (user.Role != role) return new Tuple<string, bool>($"You are not {role.NormalizeName}", false);
            return new Tuple<string, bool>(string.Empty, true);
        }

        public Tuple<string, bool> CheckIsStaff(string userId)
        {
            if (userId.IsNullOrEmpty()) return new Tuple<string, bool>("parameter was null or empty", false);
            var user = unitOfWork.userRepository.GetById(userId);
            if (user == null) return new Tuple<string, bool>($"User with id {userId} is not exist", false);

            var role = unitOfWork.roleRepository.Find(r => r.Name == "STAFF").FirstOrDefault();
            if (role == null) return new Tuple<string, bool>("Role not found", false);
            if (user.Role != role) return new Tuple<string, bool>($"You are not {role.NormalizeName}", false);
            return new Tuple<string, bool>(string.Empty, true);
        }

        public Tuple<string, List<User?>?> GetListUser()
        {
            var allUser = unitOfWork.userRepository.Find(u => u.Role.Name == "USER");
            if (allUser == null || allUser.Count <= 0) return new Tuple<string, List<User?>?>("No user", null);
            return new Tuple<string, List<User?>?>("", allUser);
        }
    }
}
