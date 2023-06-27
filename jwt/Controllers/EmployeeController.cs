using jwt.Model;
using jwt.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jwt.Controllers
{
   [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employee;
        public EmployeeController(IEmployee employee)
        {
            _employee = employee;
        }

        [HttpGet]
        [ProducesResponseType(type: typeof(List<employee>), 200)]
        [ProducesResponseType(400)]
        public ActionResult list() 
        {
           var list = _employee.list();
           if (list == null) 
            { 
                return BadRequest(); 
            }

           return Ok(list);
        }

        [HttpGet("GetById")]
        [ProducesResponseType(type: typeof(employee), 200)]
        [ProducesResponseType(400)]
        public ActionResult Get(int id)
        { 
            var employee = _employee.GetEmployee(id);
            if(employee == null)
            {
                return BadRequest();    
            }

            return Ok(employee);
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult Create([FromBody]employee entity)
        { 
            bool x=_employee.create(entity);

            return(x)? Ok(entity):BadRequest();
        
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult Update([FromBody]employee entity , [FromQuery]int id) 
        {  
            bool x =_employee.update(entity, id);

            return(x)?Ok(entity):BadRequest();
        
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult Delete(int id)
        {
            var x = _employee.remove(id);

            return (x)? Ok():BadRequest();
        }
    }
}
