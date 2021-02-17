using Application.Responses;
using Application.Services.BudgetPeriods;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetPeriodController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<ListResponse<BudgetPeriodListDto>>> Get([FromQuery] BudgetPeriodsQuery query) {
            return Ok(await Mediator.Send(query));
        }  
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ListResponse<BudgetPeriodListDto>>> GetById([FromRoute] BudgetPeriodQuery query) {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost()]
        public async Task<ActionResult<OperationResponse>> Create([FromBody] CreateBudgetPeriodCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut()]
        public async Task<ActionResult<OperationResponse>> Update([FromBody] UpdateBudgetPeriodCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationResponse>> Delete([FromRoute] DeleteBudgetPeriodCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
