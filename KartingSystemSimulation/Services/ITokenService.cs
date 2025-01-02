namespace KartingSystemSimulation.Services
{
    public interface ITokenService
    {
        string GenerateJwtToken(string email, string role, string permissions);
    }
}