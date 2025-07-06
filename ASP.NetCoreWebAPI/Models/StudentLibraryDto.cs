using System;
namespace ASP.NetCoreWebAPICRUD.Models
{
    public class StudentLibraryDto
    {
        public int id { get; set; }  // ✅ Now acts as Primary Key
        public string Stu_LibName { get; set; }
        public int Stu_libStandard { get; set; }
        public DateTime from_date { get; set; }
        public DateTime to_date { get; set; }
    }

}
