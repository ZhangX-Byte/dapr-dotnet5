using System;
using System.Threading.Tasks;
using Dapr.Client;
using Dapr.Client.Http;
using Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ServiceB.Controllers
{
    [ApiController]
    public class HelloController : ControllerBase
    {

        private readonly DaprClient daprClient;

        public HelloController(DaprClient daprClient)
        {
            this.daprClient = daprClient;
        }

        [HttpPost("talk")]
        public async Task<SomeResponseBody> Talk(SomeRequestBody someRequestBody)
        {
            var data = new { Time = DateTime.Now.ToLongDateString(), Id = "This is Service C." };
            HTTPExtension httpExtension = new HTTPExtension()
            {
                Verb = HTTPVerb.Post
            };
            SomeResponseBody responseBody = await daprClient.InvokeMethodAsync<object, SomeResponseBody>("dotnet-server-c", "talk", data, httpExtension);

            Console.WriteLine(string.Format("{0}:{1} \n recieve message:{2}", someRequestBody.Id, someRequestBody.Time, responseBody.Msg));
            return await Task.FromResult(new SomeResponseBody
            {
                Msg = "This is ServiceB"
            });
        }
    }
}
