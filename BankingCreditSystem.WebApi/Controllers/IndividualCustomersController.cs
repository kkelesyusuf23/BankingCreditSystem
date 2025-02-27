using BankingCreditSystem.Application.Features.IndividualCustomers.Commands.Create;
using BankingCreditSystem.Application.Features.IndividualCustomers.Commands.Delete;
using BankingCreditSystem.Application.Features.IndividualCustomers.Commands.Update;
using BankingCreditSystem.Application.Features.IndividualCustomers.Queries.GetById;
using BankingCreditSystem.Application.Features.IndividualCustomers.Queries.GetList;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class IndividualCustomersController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateIndividualCustomerCommand createIndividualCustomerCommand)
    {
        var result = await Mediator.Send(createIndividualCustomerCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateIndividualCustomerCommand updateIndividualCustomerCommand)
    {
       var result = await Mediator.Send(updateIndividualCustomerCommand);
       return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
       var command = new DeleteIndividualCustomerCommand { Id = id };
       var result = await Mediator.Send(command);
       return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var query = new GetByIdIndividualCustomerQuery { Id = id };
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] GetListIndividualCustomerQuery getListIndividualCustomerQuery)
    {
        var result = await Mediator.Send(getListIndividualCustomerQuery);
        return Ok(result);
    }
} 