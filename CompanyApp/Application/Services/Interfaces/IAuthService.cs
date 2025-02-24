namespace CompanyApp.Application.Services.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(string username);

    }
}
