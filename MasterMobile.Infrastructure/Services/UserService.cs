using Refit;
using MasterMobile.Domain.DTO.User;
using MasterMobile.Domain.Query.User;
using MasterMobile.Infrastructure.ApiContract;
using System;
using System.Threading.Tasks;

namespace MasterMobile.Infrastructure.Services
{
    public class UserService
    {
        public UserService() { }

        public async Task<UserDto> Authorize(string login, string password)
        {
            try
            {
                var logInQuery = new LogInQuery(login, password);
                var api = RestService.For<IPaymentApiController>("https://shake.azurewebsites.net");
                var userCred = await api.LogInAsync(logInQuery);
                return userCred;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
