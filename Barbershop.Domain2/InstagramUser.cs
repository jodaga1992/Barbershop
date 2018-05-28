namespace Barbershop.Domain2
{
    using Newtonsoft.Json;
    public class InstagramUser
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("profile_picture")]
        public string ProfilePicture { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("website")]
        public string WebSite { get; set; }

        [JsonProperty("is_business")]
        public bool IsBusiness { get; set; }
    }
}
