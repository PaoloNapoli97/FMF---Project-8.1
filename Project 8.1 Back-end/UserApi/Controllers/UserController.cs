using Microsoft.AspNetCore.Mvc;
using UserModel;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UserController : ControllerBase
    {
        private static List<User> _users = new List<User> {
            new User ("michele.malgeri@unict.it","Michele Malgeri"),
            new User ("vincenza.carchiolo@unict.it", "Vincenza Carchiolo"),
            new User ("marco.grassia@unict.it", "Marco Grassia")
        };

        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            return Ok(_users);
        }
    }
}
