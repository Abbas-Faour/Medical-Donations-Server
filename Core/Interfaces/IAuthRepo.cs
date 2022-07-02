using Core.Entites.JWT;

namespace Core.Interfaces
{
    public interface IAuthRepo
    {
        Task<Response> RegisterAsync(RegisterModel model);
        Task<AuthModel> LoginAsync(LoginModel model);
        Task<AuthModel> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
        Task<bool> ValidateUserTokenAsync(string token, string email);
        bool ValidateAdminTokenAsync(string token);

    }
}