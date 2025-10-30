using ApiProjeKampi.WebUI.Dtos.ChefDtos;
using ApiProjeKampi.WebUI.Dtos.EmployeeTaskDtos;

namespace ApiProjeKampi.WebUI.Dtos.EmployeeTaskShefDtos;

public class ResultEmployeeTaskShefDto
{
    public ResultChefDto Chef { get; set; }
    public int ChefId { get; set; }
    public int EmployeeTaskId { get; set; }
    public ResultEmployeeTaskDto EmployeeTask { get; set; }
}