using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using EmployeeCurd.Models;
using EmployeeCurd.Lib;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace EmployeeCurd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private SqlConnection connection = AuthLib.empCon;
        public EmployeeController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        [Route("GetEmployee")]
        public JsonResult Get()
        {
            try
            {
                // string connection = _configuration.GetConnectionString("EmployeeCon");
                SqlDataAdapter sda = new SqlDataAdapter("select * from Employees", connection);

                DataTable dt = new DataTable();
                sda.Fill(dt);

                return new JsonResult(dt);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddEmployee")]
        public JsonResult Post(Employee pobj)
        {
            try
            {
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EmployeeCon"));
                SqlCommand cmd = new SqlCommand("INSERT INTO Employees(EmployeeName,Department,DateOfJoining)VALUES(@EmployeeName,@Department,@DateOfJoining)", con);
                cmd.Parameters.AddWithValue("@EmployeeName", pobj.EmployeeName);
                cmd.Parameters.AddWithValue("@Department", pobj.Department);
                cmd.Parameters.AddWithValue("@DateOfJoining", pobj.DateOfJoining);
                con.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return new JsonResult("Employee Added Successfully !!");
                }

                return new JsonResult("Something went wrong !!");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }


        [HttpPut]
        [Route("UpdateEmployee")]
        public JsonResult Put(Employee pobj)
        {
            try
            {
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EmployeeCon"));
                SqlCommand cmd = new SqlCommand("UPDATE Employees SET EmployeeName=@EmployeeName,Department=@Department,DateOfJoining=@DateOfJoining WHERE EmployeeId=@EmployeeId", con);
                cmd.Parameters.AddWithValue("@EmployeeId", pobj.EmployeeId);
                cmd.Parameters.AddWithValue("@EmployeeName", pobj.EmployeeName);
                cmd.Parameters.AddWithValue("@Department", pobj.Department);
                cmd.Parameters.AddWithValue("@DateOfJoining", pobj.DateOfJoining);
                con.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return new JsonResult("Employee Update Successfully !!");
                }
                con.Close();
                return new JsonResult("Something went wrong !!");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        [HttpPut]
        [Route("DeleteEmployee")]
        public JsonResult Delete(int id)
        {
            try
            {
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EmployeeCon"));
                SqlCommand cmd = new SqlCommand("DELETE FROM Employees  WHERE EmployeeId=@EmployeeId", con);
                cmd.Parameters.AddWithValue("@EmployeeId", id);
                con.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return new JsonResult("Employee Deleted Successfully !!");
                }

                return new JsonResult("Something went wrong !!");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }
        [HttpPost]
        [Route("UploadFile")]
        public JsonResult FileUpload()
        {
            try
            {
                var httpRequest = Request.Form;
                var postFile = httpRequest.Files[0];
                string fileName = postFile.FileName;

                var physicalPath = _env.ContentRootPath + "/Photo" + fileName;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postFile.CopyTo(stream);
                }
                return new JsonResult(fileName + " Uploaded Successfully !!");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }
    }
}
