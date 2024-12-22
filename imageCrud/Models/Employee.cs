

using System.ComponentModel.DataAnnotations;

namespace imageCrud.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpImg { get; set; }
    }
}