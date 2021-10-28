using Newtonsoft.Json;

namespace MasterMobile.Domain.DTO.User
{
    public class UserDto
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
    }
}
