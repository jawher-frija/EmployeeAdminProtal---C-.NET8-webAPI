using EmployeeAdminProtal.Data;
using EmployeeAdminProtal.Models;
using EmployeeAdminProtal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminProtal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public EmployeesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult GetAllEmployees()
        {
          var allEmployees=  dbContext.Employees.ToList();
            return Ok(allEmployees);
        }
        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            var employeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary,
            };




            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();
            return Ok(employeeEntity);

        }
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
           var employee =  dbContext.Employees.Find(id);
            if(employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id,UpdateEmplyeeDto updateEmplyeeDto)
        {

            var employee = dbContext.Employees.Find(id);
            {
                if(employee == null)
                {
                    return NotFound();
                }
                employee.Name = updateEmplyeeDto.Name;
                employee.Email = updateEmplyeeDto.Email;
                employee.Phone = updateEmplyeeDto.Phone;
                employee.Salary = updateEmplyeeDto.Salary;

                dbContext.SaveChanges();
                return Ok(employee);

            }
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();
            return Ok("Employee detled !");
        }


    }
}
