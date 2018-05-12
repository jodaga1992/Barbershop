

namespace Barbershop.Models
{
    using Newtonsoft.Json;

    public class BarberResponse
    {
        [JsonProperty("BarberId")]
        public long BarberId { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Telephone")]
        public string Telephone { get; set; }

        [JsonProperty("ImagePath")]
        public string ImagePath { get; set; }

        [JsonProperty("Password")]
        public object Password { get; set; }

        [JsonProperty("ImageFullPath")]
        public string ImageFullPath { get; set; }

        [JsonProperty("FullName")]
        public string FullName { get; set; }
    }
}
