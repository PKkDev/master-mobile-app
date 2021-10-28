using Newtonsoft.Json;

namespace MasterMobile.Domain.Query.User
{
    public class LogInQuery
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        public LogInQuery() { }

        public LogInQuery(string login, string password)
        {
            this.UserName = login;
            this.Password = password;
        }
    }
}
