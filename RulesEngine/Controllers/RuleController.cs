using Microsoft.AspNetCore.Mvc;
using RulesEngine.Core.Models;
using RulesEngine.Core.Services;
using System;
using System.Threading.Tasks;

namespace RulesEngine.API.Controllers
{
    [ApiController]
    public class RuleController : ControllerBase
    {
        private IRulesActionCoordinatorService _rulesActionCoordinatorService;

        public RuleController(IRulesActionCoordinatorService rulesActionCoordinatorService)
        {
            _rulesActionCoordinatorService = rulesActionCoordinatorService;
        }

        [Route("api/ApplyRules")]
        [HttpPost]
        public async Task<ActionResult> ApplyRules(Payment payment)
        {
            if (payment is null) return BadRequest();
            try
            {
                await _rulesActionCoordinatorService.ApplyActionsAsync(payment);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
