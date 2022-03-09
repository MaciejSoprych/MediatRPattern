using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Currency : ControllerBase
    {
        private readonly IMediator _mediatR;

        public Currency(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        [HttpGet("api/currencies")]
        public async Task<List<CurrencyDto>> Get()
        {
            return await _mediatR.Send(new Query());
        }
        public class Query : IRequest<List<CurrencyDto>>
        {

        }

        public class CurrencyDto
        {
            public CurrencyDto(string name)
            {
                Name = name;
            }

            public string Name { get; set; }
        }

        public class GetCurrencyQueryHandler : IRequestHandler<Query, List<CurrencyDto>>
        {
            public async Task<List<CurrencyDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var currencies = new List<CurrencyDto>
                {
                    new CurrencyDto("PLN"),
                    new CurrencyDto("EUR"),
                    new CurrencyDto("USD")
                };

                return currencies;
            }
        }
    }
}
