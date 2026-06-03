using ManagementService.Business;
using ManagementService.DataAccess.Repository;
using ManagementService.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagementService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class Users : ControllerBase
    {
        private readonly IUserBl _userBl;
        
        public Users(IUserBl userBl)
        {
            _userBl = userBl;
        }
        
        [AllowAnonymous]
        // GET: api/<Users>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userBl.GetUser();
        }

        // GET api/<Users>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Users>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Users>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Users>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
