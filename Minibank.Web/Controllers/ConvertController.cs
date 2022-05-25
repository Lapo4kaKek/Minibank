using Microsoft.AspNetCore.Mvc;
using Minibank.Core;

namespace inibank.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConvertController : ControllerBase
    {
        private readonly ICalculator _calculator;

        public ConvertController(ICalculator calculator)
        {
            _calculator = calculator;
        }
        
        [HttpGet]
        public double Get(string currency,string balance)
        {
            return _calculator.ConvertCurrency(currency,balance);
        }
    }
}