using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace EmployeeCurd.Lib
{
    public class AuthLib
    {
        // private readonly IConfiguration _configuration;
        // private readonly IWebHostEnvironment _env;

        // public AuthLib(IConfiguration configuration, IWebHostEnvironment env)
        // {
        //     _configuration = configuration;
        //     _env = env;
        // }
        public static SqlConnection empCon=new SqlConnection("Data Source=LAP-142;user=sa;password=login@123;database=TestDb;");
        public DataTable Authenticate(string email,string password)
        {
            try
            {
               // string connection = this._configuration.GetConnectionString("EmployeeCon");

                SqlDataAdapter sda = new SqlDataAdapter("select * from userMaster where email='"+email+"' AND password='"+password+"'", empCon);

                DataTable dt = new DataTable();
                sda.Fill(dt);

                return dt;
            }
            catch (Exception)
            {
                throw;
            }
           
        }
    }
}
