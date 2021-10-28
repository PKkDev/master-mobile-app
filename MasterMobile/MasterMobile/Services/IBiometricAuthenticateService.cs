using System.Threading.Tasks;

namespace MasterMobile.Services
{
    public interface IBiometricAuthenticateService
    {
        string GetAuthenticationType();
        Task<bool> AuthenticateUserIDWithTouchID();
        bool FingerprintEnabled();
    }
}
