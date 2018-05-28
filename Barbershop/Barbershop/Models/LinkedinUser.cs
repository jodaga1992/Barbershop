namespace Barbershop.Models
{
    using Newtonsoft.Json;

    public class LinkedinUser
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

    }
}
