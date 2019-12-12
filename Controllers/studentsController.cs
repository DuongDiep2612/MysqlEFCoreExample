using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TempProject3Scaffold.Models;
using System.Threading.Tasks;

namespace TempProject3Scaffold.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : Controller
    {
        private studentContext _studentContext;

        public StudentController(studentContext context)
        {
            _studentContext = context;
        }

        // GET api/students
        [HttpGet]
        public ActionResult<IEnumerable<Student>> Get()
        {
            return _studentContext.Student.ToList();
        }

        // GET api/students/getbyid/{id}
        [HttpGet]
        [Route("getbyid/{id}")]
        public ActionResult<Student> GetById(int id)
        {
            if (id <= 0)
            {
                return NotFound("Student id must be higher than zero");
            }
            // search student in databse with condition is id = id.
            Student student = _studentContext.Student.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound("Student not found");
            }
            return Ok(student);
        }

        // POST api/students   (insert into)
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Student student)
        {
            if (student == null)
            {
                return NotFound("Student data is not supplied");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _studentContext.Student.AddAsync(student);
            await _studentContext.SaveChangesAsync();
            return Ok(student);
        }

        // PUT api/students     (Update set)    update follow ID because ID is primary key.
        [HttpPut]
        public async Task<ActionResult> Update([FromBody]Student student)
        {
            if (student == null)
            {
                return NotFound("Stduent data is not supplied");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // search students have id = id then update data.
            Student existingStudent = _studentContext.Student.FirstOrDefault(s => s.Id == student.Id);
            if (existingStudent == null)
            {
                return NotFound("Student does not exist in the database");
            }
            existingStudent.Firstname = student.Firstname;
            existingStudent.Lastname = student.Lastname;
            existingStudent.State = student.State;
            existingStudent.City = student.City;
            _studentContext.Attach(existingStudent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _studentContext.SaveChangesAsync();
            return Ok(existingStudent);
        }

        // DELETE api/students/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound("Id is not supplied");
            }
            Student student = _studentContext.Student.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound("No student found with particular id supplied");
            }
            _studentContext.Student.Remove(student);
            await _studentContext.SaveChangesAsync();
            return Ok("Student is deleted sucessfully.");
        }

        ~StudentController()
        {
            _studentContext.Dispose();
        }
    }
}