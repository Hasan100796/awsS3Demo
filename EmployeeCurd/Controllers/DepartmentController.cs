using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using EmployeeCurd.Models;
namespace EmployeeCurd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("GetDepartment")]
        public JsonResult Get()
        {
            try
            {
                string connection = _configuration.GetConnectionString("EmployeeCon");
                SqlDataAdapter sda = new SqlDataAdapter("select * from department", connection);

                DataTable dt = new DataTable();
                sda.Fill(dt);

                return new JsonResult(dt);
            }
            catch(Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddDepartment")]
        public JsonResult AddDepartment(Department pobj)
        {
            try
            {
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EmployeeCon"));
                SqlCommand cmd = new SqlCommand("INSERT INTO Department(DepartmentName)VALUES(@DepartmentName)", con);
                cmd.Parameters.AddWithValue("@DepartmentName", pobj.DepartmentName);
                con.Open();
                if(cmd.ExecuteNonQuery()>0)
                {
                    return new JsonResult("Department Added Successfully !!");
                }
                
                return new JsonResult("Something went wrong !!");
            }
            catch(Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }


        [HttpPut]
        [Route("UpdateDepartment")]
        public JsonResult UpdateDepartment(Department pobj)
        {
            try
            {
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EmployeeCon"));
                SqlCommand cmd = new SqlCommand("UPDATE Department SET DepartmentName=@DepartmentName WHERE DepartmentId=@DepartmentId", con);
                cmd.Parameters.AddWithValue("@DepartmentId", pobj.DepartmentId);
                cmd.Parameters.AddWithValue("@DepartmentName", pobj.DepartmentName);
                con.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return new JsonResult("Department Update Successfully !!");
                }

                return new JsonResult("Something went wrong !!");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        [HttpPut]
        [Route("DeleteDepartment")]
        public JsonResult DeleteDepartment(int id)
        {
            try
            {
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EmployeeCon"));
                SqlCommand cmd = new SqlCommand("DELETE FROM Department  WHERE DepartmentId=@DepartmentId", con);
                cmd.Parameters.AddWithValue("@DepartmentId", id);
                con.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return new JsonResult("Department Deleted Successfully !!");
                }

                return new JsonResult("Something went wrong !!");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }
    }
}
