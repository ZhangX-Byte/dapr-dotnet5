using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Dapr.Client;
using Dapr.Client.Http;
using Dtos;

namespace ClientA
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var jsonOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
            };

            var client = new DaprClientBuilder()
                .UseJsonSerializationOptions(jsonOptions)
                .Build();

            var data = new { Time = DateTime.Now.ToLongDateString(), Id="This is Client A" };
            HTTPExtension httpExtension = new HTTPExtension()
            {
                Verb = HTTPVerb.Post
            };
            while (true)
            {
                var a = await client.InvokeMethodAsync<object, SomeResponseBody>("dotnet-server-b", "talk", data, httpExtension);
                Console.WriteLine(a.Msg);
                await Task.Delay(5 * 1000);
            }
        }
    }
}
