using System;
using System.Threading.Tasks;
using Dtos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceC.Controllers
{
    [ApiController]
    public class HelloController : Controller
    {
        [HttpPost("talk")]
        public async Task<SomeResponseBody> Talk(SomeRequestBody someRequestBody)
        {
            Console.WriteLine(string.Format("{0}:{1}", someRequestBody.Id, someRequestBody.Time));
            return await Task.FromResult(new SomeResponseBody
            {
                Msg = "This is ServiceC"
            });
        }
    }
}
