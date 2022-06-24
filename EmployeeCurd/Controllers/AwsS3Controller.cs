using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeCurd.Models;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Text;

namespace EmployeeCurd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwsS3Controller : ControllerBase
    {
        private readonly IConfiguration _configuration;
        // S3Model S3Model;
        public AwsS3Controller(IConfiguration configuration)
        {
            //  S3Model = s3Model;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetList")]
        public JsonResult Get()
        {
            try
            {
                using (var s3 = new AmazonS3Client(RegionEndpoint.USWest2))
                {
                    Task<ListBucketsResponse> res = s3.ListBucketsAsync();
                    Task.WaitAll(res);
                    List<object> bucketList = new List<object>();
                    Buckets obj = new Buckets();
                    //Console.WriteLine("List of S3 Buckets in your AWS Account");
                    foreach (var bucket in res.Result.Buckets)
                    {
                        obj = new Buckets();
                        obj.bucketName = bucket.BucketName;
                        obj.createdDate = bucket.CreationDate;
                        bucketList.Add(obj);
                      //  Console.WriteLine(bucket.BucketName);
                    }
                    return new JsonResult(bucketList);
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }
        [HttpPost]
        [Route("CreateNewBucket")]
        public JsonResult CreateNewBucket(string bucketName)
        {
            try
            {
                using (var s3 = new AmazonS3Client(RegionEndpoint.USWest2))
                {
                    var req = new PutBucketRequest
                    {
                        BucketName = bucketName,
                        BucketRegion = S3Region.USWest2
                    };
                    Task<PutBucketResponse> res = s3.PutBucketAsync(req);
                    Task.WaitAll(res);

                    if (res.IsCompletedSuccessfully)
                    {
                        //Console.WriteLine("New S3 bucket created: {0}", bucketName);
                        return new JsonResult("New S3 bucket created: {0}", bucketName);
                    }
                }
                return new JsonResult("Somethng went wrong !!");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }

        [HttpPost]
        [Route("WriteFile")]
        public JsonResult WriteFile(string bucketName,string key)
        {
            try
            {
                //string connection = _configuration.GetConnectionString("EmployeeCon");
                //SqlDataAdapter sda = new SqlDataAdapter("select * from department", connection);

                //DataTable dt = new DataTable();
                //sda.Fill(dt);
                using (var s3 = new AmazonS3Client(RegionEndpoint.USWest2))
                {

                    var jsonStr = "{'name':'John', 'age':30, 'car':null}";
                    var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonStr));
                    var req = new PutObjectRequest
                    {
                        BucketName = bucketName,
                        Key = key,
                        InputStream = ms
                    };

                    Task<PutObjectResponse> res = s3.PutObjectAsync(req);
                    Task.WaitAll(res);

                    if (res.IsCompletedSuccessfully)
                    {
                        return new JsonResult($"Created object '{key}' in bucket '{bucketName}'");
                       // Console.WriteLine("Created object '{0}' in bucket '{1}'", key, bucketName);
                    }
                }
                return new JsonResult("Somethng went wrong !!");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }

        [HttpGet]
        [Route("ReadFile")]
        public JsonResult ReadFile(string bucketName, string key)
        {
            try
            {
                using (var s3 = new AmazonS3Client(RegionEndpoint.USWest2))
                {

                    var req = new GetObjectRequest
                    {
                        BucketName = bucketName,
                        Key = key
                    };

                    Task<GetObjectResponse> res = s3.GetObjectAsync(req);
                    Task.WaitAll(res);

                    if (res.IsCompletedSuccessfully)
                    {
                        using (var reader = new StreamReader(res.Result.ResponseStream))
                        {
                            //  Console.WriteLine("Retrieved contents of object '{0}' in bucket '{1}'", key, bucketName);
                            return new JsonResult(reader.ReadToEnd());
                        }
                        
                        // Console.WriteLine("Created object '{0}' in bucket '{1}'", key, bucketName);
                    }
                }
                return new JsonResult("Somethng went wrong !!");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }
    }
}
