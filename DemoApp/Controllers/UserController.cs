using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController
    {
        private static List<string> users = new List<string> { "Duy", "Nhung", "Lam", "Hưởng" };

        [HttpGet]
        public IActionResult GetUsers()
        {
            return new OkObjectResult(users);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] string name)
        {        
            if (string.IsNullOrWhiteSpace(name))
                return new BadRequestObjectResult(new { message = "Name cannot be empty" });
            users.Add(name);
            return new OkObjectResult(new { message = "User added", data = users });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (id < 0 || id >= users.Count)
                // return not found

                return new NotFoundObjectResult(new { message = "User not found" });

            users.RemoveAt(id);
            return new OkObjectResult(new { message = "User deleted", data = users });
        }


    }
}
