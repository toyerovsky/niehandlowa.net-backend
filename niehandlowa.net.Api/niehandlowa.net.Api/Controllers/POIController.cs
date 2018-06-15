using Microsoft.AspNetCore.Mvc;
using niehandlowa.net.Bll.Models;
using niehandlowa.net.Bll.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace niehandlowa.net.Api.Controllers
{
    public class POIController : Controller
    {
        private readonly IPOIService _POIService;

        public POIController(IPOIService POIService)
        {
            _POIService = POIService;
        }

        [HttpGet]
        public async Task<IActionResult> Create([FromBody] POIModel model)
        {
            await _POIService.Create(model);
            return StatusCode(201);
        }
    }
}
