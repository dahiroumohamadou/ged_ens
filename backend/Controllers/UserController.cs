using backend.Model;
using backend.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUser _userRepo;
        public UserController(IUser userRepo) { 
            _userRepo = userRepo; 
        }
        [HttpPost]
        //[AllowAnonymous]
        [Route("/login")]
        public IActionResult Login(User user)
        {
            IActionResult response = Unauthorized();
            var u = _userRepo.AuthenticateUser(user);
            if (u != null)
            {
                var token = _userRepo.GenerateToken(u);
                response = Ok(new { token = token });
            }
            return response;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var usrs = _userRepo.GetAll();
            if (usrs == null)
                return NotFound();
            return Ok(usrs);

        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var usr = _userRepo.GetById(id);
            if (usr == null)
                return NotFound();
            return Ok(usr);
        }
        [HttpGet]
        [Route("mail/{email}")]
        public IActionResult GetByEmail(string email)
        {
            var usr = _userRepo.GetByEmail(email);
            if (usr == null)
                return NotFound();
            return Ok(usr);
        }
        [HttpPost]
        [Route("Add")]
        public IActionResult Add(User u)
        {
            int res = _userRepo.Add(u);
            if (res <= 0)
                return NotFound();
            return Ok("Added " + res);

        }


        [HttpPut]
        public IActionResult Edit(User u)
        {
            int res = _userRepo.Update(u);
            if (res <= 0)
                return NotFound();
            return Ok("Updated" + res);
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            int res = _userRepo.Delete(id);
            if (res <= 0)
                return NotFound();
            return Ok("Deleted" + res);
        }
    }
}
