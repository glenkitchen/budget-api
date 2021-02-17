using Application.Responses;
using Application.Services.BudgetYears;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetYearController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<ListResponse<BudgetYearListDto>>> Get([FromQuery] BudgetYearsQuery query) {
            return Ok(await Mediator.Send(query));
        }  
        
        [HttpGet("{id}")]
        public async Task<ActionResult<DataResponse<BudgetYearDto>>> GetById([FromRoute] BudgetYearQuery query) {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost()]
        public async Task<ActionResult<OperationResponse>> Create([FromBody] CreateBudgetYearCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut()]
        public async Task<ActionResult<OperationResponse>> Update([FromBody] UpdateBudgetYearCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationResponse>> Delete([FromRoute] DeleteBudgetYearCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
