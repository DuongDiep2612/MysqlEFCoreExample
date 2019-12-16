using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TempProject3Scaffold.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;


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
            // Student student = _studentContext.Student.FirstOrDefault(s => s.Id == id);


            // --------------------------------------

            var student = _studentContext.Student
                    .FromSql("SELECT * FROM student WHERE firstname = 'duong'")
                    .ToList();

            //var student2 =  _studentContext.Student.FromSql("CALL AddUser(7, 'hoang cong', 'hung 2', 'bac giang', 'itg')");
            //var student3 =  _studentContext.Student.FromSql("CALL AddUser(7, 'hoang cong', 'hung 2', 'bac giang', 'itg')");
            var student4 =  _studentContext.Student.FromSql("CALL AddUser(41, 'hoang cong', 'hung 2', 'bac giang', 'itg')");
            var student5 =  _studentContext.Student.FromSql("CALL AddUser(19, 'hoang cong', 'hung 2', 'bac giang', 'itg')");

            // Student student1 = _studentContext.Query.
            // var test = _studentContext.Student.Add
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
            // await _studentContext.Student.AddAsync(student);
            // var student2 =  _studentContext.Student.FromSql("CALL AddUser(7, 'hoang cong', 'hung 2', 'bac giang', 'itg');//");
            // var student3 =  _studentContext.Student.FromSql("CALL AddUser(7, 'hoang cong', 'hung 2', 'bac giang', 'itg');//");
            var student4 =  _studentContext.Student.FromSql("CALL AddUser(11, 'hoang cong', 'hungr 2', 'bac ggiang', 'igtg');" + 
                                                            "CALL AddUser(12, 'ehoang cong', 'hungg 2', 'bac giang', 'itgg');" +
                                                            "CALL AddUser(13, 'hggoang cong', 'hgung 2', 'bgac giang', 'gitg');"
                                                            );
            // await _studentContext.SaveChangesAsync();
            await _studentContext.SaveChangesAsync();
            return Ok(student4);
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