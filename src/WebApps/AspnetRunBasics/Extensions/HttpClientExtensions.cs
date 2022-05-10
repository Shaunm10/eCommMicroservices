using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace AspnetRunBasics.Extensions
{
    public static class HttpClientExtensions
    {
        private static string ApplicationJson = "application/json";
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

        public static Task<HttpResponseMessage> PostAsJson<T>(this HttpClient httpClient, string url, T data)
        {
            // serialize the object
            var dataAsString = JsonSerializer.Serialize(data);

            // create string content from it.
            var content = new StringContent(dataAsString);

            // add the content type header
            content.Headers.ContentType = new MediaTypeHeaderValue(ApplicationJson);

            // post the data to the url
            return httpClient.PostAsync(url, content);
        }

        public static Task<HttpResponseMessage> PutAsJson<T>(this HttpClient httpClient, string url, T data)
        {
            // serialize the object
            var dataAsString = JsonSerializer.Serialize(data);

            // create string content from it.
            var content = new StringContent(dataAsString);

            // add the content type header
            content.Headers.ContentType = new MediaTypeHeaderValue(ApplicationJson);

            return httpClient.PostAsync(url, content);
        }
    }
}
