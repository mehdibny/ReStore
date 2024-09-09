using e_commece_API_React.Entities;
using e_commece_API_React.RequestHelpers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace e_commece_API_React.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, MetaData metaData)
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            response.Headers.Append("Pagination", JsonSerializer.Serialize(metaData, options));
            response.Headers.Append("Access-Control-Expose-Headers", "Pagination");

        }
    }
}
