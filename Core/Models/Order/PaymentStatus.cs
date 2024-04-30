using System.Text.Json.Serialization;

namespace Core.Models.Order
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentStatus
    {
        Pending , Failed , Received
    }
}