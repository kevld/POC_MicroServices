using Microsoft.AspNetCore.Mvc;
using POC_MicroServices.Form.DTO;
using MassTransit;
using System.IO.Pipes;
using POC_MicroServices.Form.Services;

namespace POC_MicroServices.Form.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormValidationController : ControllerBase
    {
        private readonly IBus _bus;

        public FormValidationController(IBus bus)
        {
            _bus = bus;

        }

        [HttpPost("ValidForm")]
        public async Task<IActionResult> ValidForm(SendMailDTO sendMailDTO)
        {
            // Will automatically return err 400 if sendMailDTO is not valid, due to [ApiCOntroller]

            try
            {
                SendMailService sms = new SendMailService(_bus);
                await sms.SendMailAsync(sendMailDTO.To, sendMailDTO.Content);

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
