using Newtonsoft.Json;
namespace PhoneBook.Core.response
{
    public class ResponseMessage
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; }
    }
}
