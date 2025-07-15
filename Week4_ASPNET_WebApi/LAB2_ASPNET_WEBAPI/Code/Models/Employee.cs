using System.ComponentModel.DataAnnotations;

namespace SwaggerWebAPIDemo.Models
{
    public class Employee
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Department { get; set; } = string.Empty;
        
        [Range(0, double.MaxValue)]
        public decimal Salary { get; set; }
        
        public DateTime JoinDate { get; set; } = DateTime.Now;
        
        public bool IsActive { get; set; } = true;
    }
}
