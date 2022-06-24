using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using EmployeeCurd.Models;
using EmployeeCurd.Lib;
namespace EmployeeCurd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [Route("Auth")]
        public JsonResult Auth(Auth pobj)
        {
            try{
            AuthLib objAuth=new AuthLib();
            
            return new JsonResult(objAuth.Authenticate(pobj.email, pobj.password));
            }
            catch(Exception ex)
            {
                 return new JsonResult(ex.Message);
            }
        }


    }
}
