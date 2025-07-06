using ASP.NetCoreWebAPICRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NetCoreWebAPI.Controllers
{
    //very important --> to link your project to your database 
    //go to tools --> NuGet package manager ---> Package amanger console 
    //then console opens in the lowermost part of this page and paste this command 
    //and change some attributes , the command is 
    //Scaffold-DbContext "server=RANJANAPANDEY\SQLEXPRESS;database=MyDB;trusted_connection=true;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        //iss class me kya hua --> https://youtu.be/SszvaPH7IxM?si=founRluQ23nWBzW7 20:48 seconds 
        private readonly MyDbContext context;

        public StudentAPIController(MyDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            var data = await context.Student.ToListAsync();    
            return Ok(data);
        }
        //now we wwant to get data by using id 
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudents(int id)
        {
            var data1 = await context.Student.FindAsync(id);
            if(data1 == null)
            {
                return NotFound();  
            }
            return data1;
        }
        //Now we want to insert ata through web api
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudentDataInDB(Student std)
        {
            await context.Student.AddAsync(std);
            await context.SaveChangesAsync();
            return Ok(std); 
        }
        //Now we wantn to update the data in sql table
        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateStudentTableInDB(int id, Student std)
        {
            if (id != std.Id)
            {
                return BadRequest();
            }
            context.Entry(std).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(std);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudentTableInDB(int id)
        {
            var std = await context.Student.FindAsync(id);
            if (id == null)
            {
                return NotFound();
            }
            context.Student.Remove(std);
            await context.SaveChangesAsync();
            return Ok(std);
        }

        [HttpGet("byproc/{id}")]
        public async Task<ActionResult<Student>> GetStudentByStoredProcedure(int id)
        {
            var student = await context.GetStudentByStoredProcAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        //we are calling the stored procedure created by inner join of two tables and this byprocedure is used here because swagger API was confuse between 
        //byproc of below and above one Get method
        [HttpGet("byprocedure/{id}")]
        public async Task<ActionResult<StudentLibraryDto>> GetStudentLibraryRecordsStoredProcedure(int id)
        {
            var records = await context.GetStudentLibraryRecordsAsync(id);

            if (records == null)
                return NotFound("No records found.");

            return Ok(records);
        }
        
    }



}

