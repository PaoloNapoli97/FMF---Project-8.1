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

        [HttpGet]
        [Route("Admin/GetAllUsers")]
        public ActionResult<List<User>> GetUsers()
        {   
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

        [HttpGet]
        [Route("{Id}/StartLogin")]
        public ActionResult<string> StartLogin (string Id){
            userService.ReadUsers();
            var user = userService.ReadUserById(Id);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(user.CreateChallenge());
        }

        [HttpGet]
        [Route("{Id}/{Challenge}")]
        public ActionResult<string> Login(string Id, string Challenge){
            userService.ReadUsers();
            var user = userService.ReadUserById(Id);
            if (user == null)
            {
                return BadRequest();
            }
            if (user.VerifyChallenge(Challenge)) 
            {
                return Ok(user.CreateToken());
            }
            else
            {
                return Unauthorized();
            }
        }


        [HttpPost]
        [Route("Admin/CreateUser")]
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
