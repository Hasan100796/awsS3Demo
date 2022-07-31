using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCurd
{
    public class LambdaFunction : Amazon.Lambda.AspNetCoreServer.APIGatewayProxyFunction
    {
        //protected override void Init(IWebHostBuilder builder)
        //{
        //    builder
        //        .UseContentRoot(Directory.GetCurrentDirectory())
        //        .UseStartup()
        //        .UseApiGateway();
        //}
    }
}
