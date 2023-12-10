using Core.Dtos;
using Core.Mapping;
using DataLayer;
using DataLayer.Entities;
using Infrastructure.Exceptions;

namespace Core.Services
{
    public class UserService
    {
        private readonly UnitOfWork unitOfWork;
        private readonly AuthorizationService authorizationService;

        public UserService(UnitOfWork unitOfWork, AuthorizationService authorizationService)
        {
            this.unitOfWork = unitOfWork;
            this.authorizationService = authorizationService;
        }

        public async Task<List<User>> GetAllAsync()
        {
            var customers = await unitOfWork.Users.GetAll();
            return customers;
        }

        public async Task<bool> RegisterAsync(RegisterDto registerUser)
        {
            var foundUser = await unitOfWork.Users.GetUserByEmailAsync(registerUser.Email);

            var passwordHash = authorizationService.HashPassword(registerUser.PasswordHash);

            registerUser.PasswordHash = passwordHash;

            if (foundUser != null)
            {
                throw new ForbiddenException("Email is already in use");
            }

            User newUser = registerUser.ToUser();

            await unitOfWork.Users.AddUserAsync(newUser);
            await unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<string> ValidateLoginAsync(LoginDto loginData)
        {
            var user = await unitOfWork.Users.GetUserByEmailAsync(loginData.Email);
            if (user == null)
            {
                throw new ForbiddenException("Wrong email or password");
            }

            var isPasswordOk = authorizationService.VerifyHashedPassword(user.PasswordHash, loginData.Password);
            if (isPasswordOk)
            {
                var role = user.Role;
                return authorizationService.GetToken(user, role);
            }
            else
            {
                throw new ForbiddenException("Wrong email or password");
            }
        }
    }
}
