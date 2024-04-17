using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
[Route("api/[controller]")]
public class NewApiController : ControllerBase
{
    // GET api/NewApi/test
    [HttpGet("test")]
    public ActionResult<string> GetTest()
    {
        return "The API is working!";
    }
}
}