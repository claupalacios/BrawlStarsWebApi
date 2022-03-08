using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.IO;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public CharacterController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        { //ToDo Usar entity framework
            string query = @"select CharacterId, CharacterName, Role, 
                            convert(varchar(10),DateofRelease,120) as DateOfRelease, 
                            Photo from dbo.Character";

            DataTable table = new DataTable();

            //ToDo traerlo en constructor como CreditOne
            string sqlDataSource = _configuration.GetConnectionString("CharacterAppCon");

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Character charac)
        { //ToDo Usar entity framework
            string query = @"insert into dbo.Character
            (CharacterName,Role,DateOfRelease,Photo) values 
            (
                '" + charac.CharacterName + @"',
                '" + charac.Role + @"',
                '" + charac.DateOfRelease + @"',
                '" + charac.Photo + @"'
            )";

            DataTable table = new DataTable();

            //ToDo traerlo en constructor como CreditOne
            string sqlDataSource = _configuration.GetConnectionString("CharacterAppCon");

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Update(Character charac)
        { //ToDo Usar entity framework
            string query = @"update dbo.Character set 
                            CharacterName = '" + charac.CharacterName + @"',
                            Role = '" + charac.Role + @"',
                            DateOfRelease = '" + charac.DateOfRelease + @"'
                            where CharacterId = " + charac.CharacterId + @"";

            DataTable table = new DataTable();

            //ToDo traerlo en constructor como CreditOne
            string sqlDataSource = _configuration.GetConnectionString("CharacterAppCon");

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        { //ToDo Usar entity framework
            string query = @"delete from dbo.Character 
                            where CharacterId = " + id + @"";

            DataTable table = new DataTable();

            //ToDo traerlo en constructor como CreditOne
            string sqlDataSource = _configuration.GetConnectionString("CharacterAppCon");

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted Successfully");
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + fileName;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(fileName);
            }
            catch (Exception e)
            {
                //Log excpetion
                return new JsonResult("anonymous.png");
            }
        }     
    }
}
