using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiBasicAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="BasicAuth")]
    public class OrderController : ControllerBase
    {
        public IActionResult GetOrders()
        {
            return Ok("Siparişler Listesi");
        }
    }
}
