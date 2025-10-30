using ApiProjeKampi.WebAPI.Context;
using ApiProjeKampi.WebAPI.Dtos.EmployeeTaskDtos;
using ApiProjeKampi.WebAPI.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProjeKampi.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeTasksController : ControllerBase
{
    private readonly ApiContext _context;
    private readonly IMapper _mapper;

    public EmployeeTasksController(ApiContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> EmployeeTaskList(CancellationToken cancellationToken = default)
    {
        List<EmployeeTask> employeeTasks = await _context.EmployeeTasks.Include(x=> x.EmployeeTaskChefs).ThenInclude(x=> x.Chef).ToListAsync(cancellationToken);

        return Ok(_mapper.Map<List<ResultEmployeeTaskDto>>(employeeTasks));
    }

    [HttpGet("GetByTask/{id:int}")]
    public async Task<IActionResult> GetEmployeeTaskById(int id, CancellationToken cancellationToken = default)
    {
        EmployeeTask? employeeTask = await _context.EmployeeTasks.FindAsync(id, cancellationToken);

        if (employeeTask == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<GetEmployeeTaskByIdDto>(employeeTask));
    }

    [HttpPost]
    public async Task<IActionResult> AddEmployeeTask(CreateEmployeeTaskDto createEmployeeTaskDto, CancellationToken cancellationToken = default)
    {
        EmployeeTask employeeTask = _mapper.Map<EmployeeTask>(createEmployeeTaskDto);

        await _context.EmployeeTasks.AddAsync(employeeTask, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Created(string.Empty, employeeTask);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEmployeeTask(int id, CancellationToken cancellationToken = default)
    {
        EmployeeTask? EmployeeTask = await _context.EmployeeTasks.FindAsync(id, cancellationToken);
        if (EmployeeTask == null)
        {
            return NotFound();
        }
        _context.EmployeeTasks.Remove(EmployeeTask);
        await _context.SaveChangesAsync(cancellationToken);
        return Ok("Silme İşlemi Başarılı.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateEmployeeTask(UpdateEmployeeTaskDto updateEmployeeTaskDto, CancellationToken cancellationToken = default)
    {
        EmployeeTask? employeeTask = _mapper.Map<EmployeeTask>(updateEmployeeTaskDto);

        _context.EmployeeTasks.Update(employeeTask);
        await _context.SaveChangesAsync(cancellationToken);
        return Ok("Güncelleme İşlemi Başarılı.");
    }
}