namespace Barbershop.Models
{
    using Newtonsoft.Json;
    public class LinkedinTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
