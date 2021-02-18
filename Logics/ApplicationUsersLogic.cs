using AccountApi.Authentication;
using AccountApi.Authentication.Dto;
using System;

namespace AccountApi.Logics
{
    public class ApplicationUsersLogic
    {
        public static ApplicationUser MapRegisterToApplicationUser(RegisterDto register)
        {
            return new ApplicationUser()
            {
                Email = register.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = register.Username
            };
        }
    }
}
