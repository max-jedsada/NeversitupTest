using Business.Interfaces;
using Business.ViewModel;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Project.Utility;
using Swashbuckle.AspNetCore.Annotations;

namespace NET7_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly ITestService _testService;

        public HomeController(ILogger<HomeController> logger, ITestService testService, IConfiguration configuration)
        {
            _testService = testService;
        }

        [HttpGet]
        [Route("GetPerson")]
        [SwaggerResponse(StatusCodes.Status200OK, "Get person.", typeof(Wrapper<PersonViewModel>))]
        public object GetPerson()
        {
            var preson = _testService.GetPerson();
            var response = ResponseWrapper.Success(System.Net.HttpStatusCode.OK, preson);
            return response;
        }

        [HttpGet()]
        [Route("GetPerson/{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Get person by id.", typeof(Wrapper<PersonViewModel>))]
        public object GetPerson(int id)
        {
            var preson = _testService.GetPerson(id);
            var response = ResponseWrapper.Success(System.Net.HttpStatusCode.OK, preson);
            return response;
        }


        [HttpGet()]
        [Route("Test2")]
        [SwaggerResponse(StatusCodes.Status200OK,"", typeof(Wrapper<PersonViewModel>))]
        public object Test2(string inputText)
        {
            var preson = _testService.Test2(inputText);
            var response = ResponseWrapper.Success(System.Net.HttpStatusCode.OK, preson);
            return response;
        }

        [HttpGet()]
        [Route("Test3")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(Wrapper<PersonViewModel>))]
        public object Test3(string inputNumber)
        {
            var preson = _testService.Test3(inputNumber);
            var response = ResponseWrapper.Success(System.Net.HttpStatusCode.OK, preson);
            return response;
        }

        [HttpPost()]
        [Route("Test4")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(Wrapper<PersonViewModel>))]
        public object Test4([FromBody]List<string> inputText)
        {
            var preson = _testService.Test4(inputText);
            var response = ResponseWrapper.Success(System.Net.HttpStatusCode.OK, preson);
            return response;
        }


    }
}