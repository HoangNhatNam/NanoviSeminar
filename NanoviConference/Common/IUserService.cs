using System.IdentityModel.Tokens.Jwt;
using NanoviConference.Catalog.Model.User;

namespace NanoviConference.Common
{
    public interface IUserService
    {
        Task<AuthResponse> Authencate(LoginRequest request);

        Task<(bool Succeeded, string[] Errors)> Register(RegisterRequest request);

        Task<List<UserViewModel>> GetAllUser();

        JwtSecurityToken Verify(string fwt);

        Task<bool> Delete(Guid id);

        Task<UserViewModel> GetById(Guid id);
    }
}