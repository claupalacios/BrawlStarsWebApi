using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using WebApi.Models;

//TodoA Agregar Git
//ToDo Agregar capa de repository.
//ToDo Meter todo codigo que se repite en algun lado, lo de la conexcion a la Bd
//ToDo Sacar New como me enseñaron Nico y Azu
//ToDo Agregar loggeo
//ToDo Add Sass

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public RoleController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        { //ToDo Usar entity framework
            string query = @"select RoleId, RoleName from dbo.Role";

            DataTable table = new DataTable();

            //ToDo traerlo en constructor como CreditOne
            string sqlDataSource = _configuration.GetConnectionString("CharacterAppCon");
            
            SqlDataReader myReader;
            using(SqlConnection myCon=new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand myCommand = new SqlCommand(query, myCon))
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
        public JsonResult Post(Role role)
        { //ToDo Usar entity framework
            string query = @"insert into dbo.Role values ('" + role.RoleName + @"')";

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
        public JsonResult Update(Role role)
        { //ToDo Usar entity framework
            string query = @"update dbo.Role set 
                            RoleName = '"+ role.RoleName+@"'
                            where RoleId = "+role.RoleId +@"";

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
            string query = @"delete from dbo.Role 
                            where RoleId = " + id + @"";

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

        [Route("GetAllRoleNames")]
        public JsonResult GetAllRoleNames()
        {
            string query = @"select CharacterName from dbo.Role";

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
    }
}
