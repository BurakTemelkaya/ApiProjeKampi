using ApiProjeKampi.WebAPI.Dtos.ChefDtos;
using ApiProjeKampi.WebAPI.Dtos.EmployeeTaskDtos;

namespace ApiProjeKampi.WebAPI.Dtos.EmployeeTaskShefDtos;

public class ResultEmployeeTaskShefDto
{
    public ResultChefDto Chef { get; set; }
    public int ChefId { get; set; }
    public int EmployeeTaskId { get; set; }
    public ResultEmployeeTaskDto EmployeeTask { get; set; }
}