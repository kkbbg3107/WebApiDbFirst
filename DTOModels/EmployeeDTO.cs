using System.ComponentModel.DataAnnotations;

namespace WebApiDBFirst.DTOModels
{
    public class EmployeeDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}