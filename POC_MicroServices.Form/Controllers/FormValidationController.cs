using Microsoft.AspNetCore.Mvc;
using POC_MicroServices.Form.DTO;
using MassTransit;
using System.IO.Pipes;

namespace POC_MicroServices.Form.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormValidationController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly string _busURI;

        public FormValidationController(IBus bus, IConfigurationManager configuration)
        {
            _bus = bus;

        }

        [HttpPost]
        public IActionResult ValidForm(SendMailDTO sendMailDTO)
        {
            // Will automatically return err 400 if sendMailDTO is not valid, due to [ApiCOntroller]

            try
            {


                return Ok();
            }
            catch (Exception e)
            {
                return Problem(
                    title: "Server error",
                    detail: e.Message,
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        }
    }
}
