using Microsoft.AspNetCore.Mvc;
using BankingCreditSystem.Application.Features.CreditTypes.Commands.Create;
using BankingCreditSystem.Application.Features.CreditTypes.Queries.GetList;

namespace BankingCreditSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditTypesController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCreditTypeCommand createCreditTypeCommand)
        {
            var result = await Mediator.Send(createCreditTypeCommand);
            return Created("", result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListCreditTypeQuery getListCreditTypeQuery)
        {
            var result = await Mediator.Send(getListCreditTypeQuery);
            return Ok(result);
        }
    }
}