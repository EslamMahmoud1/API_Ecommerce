using System.Text.Json.Serialization;

namespace Core.Parameters
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ProductSortingValues
    {
        NameAsc,NameDesc,PriceAsc,PriceDesc
    }
}
