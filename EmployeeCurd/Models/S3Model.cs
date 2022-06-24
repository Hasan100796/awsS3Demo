using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCurd.Models
{
    public class S3Model
    {
        // bucket names in Amazon S3 must be globally unique and lowercase
       //public  static string bucketName = $"bucket-{Guid.NewGuid().ToString("n").Substring(0, 8)}";
       //public static string key = $"key-{Guid.NewGuid().ToString("n").Substring(0, 8)}";
       public string bucketName { get; set; }
       public string key { get; set; }
    }
    public class Buckets
    {
        public string bucketName { get; set; }
        public DateTime createdDate { get; set; }
    }
}
