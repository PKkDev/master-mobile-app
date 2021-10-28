namespace MasterMobile.Services
{
    public interface IBiometricPieAuthenticate
    {
        void RegisterOrAuthenticate();
        bool CheckSDKGreater29();
    }
}
