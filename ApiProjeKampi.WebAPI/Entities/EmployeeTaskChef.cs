using System.ComponentModel.DataAnnotations.Schema;

namespace ApiProjeKampi.WebAPI.Entities;

public class EmployeeTaskChef
{
    public int Id { get; set; }

    [ForeignKey("Chef")]
    public int ChefId { get; set; }
    public Chef Chef { get; set; }

    [ForeignKey("EmployeeTask")]
    public int EmployeeTaskId { get; set; }
    public EmployeeTask EmployeeTask { get; set; }
}