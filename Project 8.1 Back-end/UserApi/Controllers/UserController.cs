using Microsoft.AspNetCore.Mvc;
using Service;
using UserModel;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UserController : ControllerBase
    {
        UserService userService = new();
        
        // private static List<User> _users = new List<User> {
        //     new User ("michele.malgeri@unict.it","Michele Malgeri"),
        //     new User ("vincenza.carchiolo@unict.it", "Vincenza Carchiolo"),
        //     new User ("marco.grassia@unict.it", "Marco Grassia")
        // };

        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {   
            // var response = credetials.GetAll();
            // return Ok(response);
            userService.CreateUserDb();
            return Ok(userService.ReadUsers());
        }

        [HttpGet]
        [Route("{Id}")]
        public ActionResult<User> GetUserById(string Id){
            userService.ReadUsers();
            var user = userService.ReadUserById(Id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public ActionResult PostUser(string Id, string Name, string Password){
            userService.ReadUsers();
            var checkIfExist = userService.ReadUserById(Id);
            if (checkIfExist != null)
            {
                return BadRequest("Id not unique, unable to add user");
            }
            else
            {
                User user = new User(Id, Name, Password);
                userService.ReadUsers().Add(user);
                var pathToUrl = Request.Path.ToString() + '/' + user.Id;
                userService.WriteUsers();
                return Created(pathToUrl, user);
            }
        }



    }
}
