using System.Text.Json;

namespace Shopping.Aggregator.Extensions;

public static class HttpClientExtensions
{
    public static async Task<T?> ReadContentAs<T>(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException($"Something went wrong calling Api: {response.ReasonPhrase}");
        }

        var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        if (dataAsString != null)
        {
            return JsonSerializer.Deserialize<T>(dataAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        throw new ApplicationException($"Unable to read Json from Api: {response.ReasonPhrase}");
    }
}
