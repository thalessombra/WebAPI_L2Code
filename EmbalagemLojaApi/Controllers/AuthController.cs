using EmbalagemLojaApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EmbalagemLojaApi.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]

    public class AuthController : Controller
    {
        [HttpPost]
        public IActionResult Auth(string username, string password)
        {
            if(username == "pedido" && password == "123456")
            {
                var token = TokenService.GenerateToken(new Models.Pedido());
                return Ok(token);
            }

            return BadRequest("usuario ou senha inválidos");
        }
    }
}
