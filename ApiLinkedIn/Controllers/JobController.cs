using ApiLinkedIn.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiLinkedIn.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        // GET: api/<JobController>
        [HttpGet]
        public ApiResponse Get()
        {
            return new JobModel().GetAll();
        }
        
        // GET api/<JobController>/5
        [HttpGet("{id}")]
        public ApiResponse Get(int id)
        {
            return new JobModel().Get(id);
        }

        // POST api/<JobController>
        [HttpPost]
        public ApiResponse Post([FromBody] JobModel model)
        {
            return new JobModel().Add(model);
        }
        
        // PUT api/<JobController>/5
        [HttpPut("{id}")]
        public ApiResponse Put([FromBody] JobModel model)
        {
            return new JobModel().Update(model);
        }
        
        // DELETE api/<JobController>/5
        [HttpDelete("{id}")]
        public ApiResponse Delete(int id)
        {
            return new JobModel().Delete(id);
        }
    }
}
