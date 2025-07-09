using Microsoft.AspNetCore.Mvc;
using WebAPI.Repository;
using WebAPI.Domain;
using WebAPI.Domain.Module;
using NuGet.Protocol;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserRep _IUserRep;

        public UserController(IUserRep URep) { _IUserRep = URep; }
        // GET: api/<UserController>
        [HttpGet("GetAll")]
        public IActionResult GetAll()// This is 2nd code changes for dev pipeline
        {
            var usr = _IUserRep.GetAll();
            return Ok(usr);
        }

        // GET api/<UserController>/5
        [HttpGet()]
        public IActionResult Get(int id)
        {
            var User = _IUserRep.Get(id);
            return Ok(_IUserRep.Get(id));
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult CreateUser(User usr)
        {
            Domain.Module.User emp = new Domain.Module.User
            {
                Name = usr.Name,
                Age = usr.Age,
                Mobile = usr.Mobile
            };
            _IUserRep.insert(emp);

            return Ok(usr);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] User updatedUser)
        {
             _IUserRep.update(updatedUser);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _IUserRep.delete(id);
        }
    }
}
