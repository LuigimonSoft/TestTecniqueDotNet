using ExamTestAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTestAPI.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetEmployees();
        Task<EmployeeDto?> GetEmployeeById(int id);
        Task<EmployeeDto?> AddEmployee(EmployeeDto employee);
        Task<bool> UpdateEmployee(EmployeeDto employee);
        Task<bool> DeleteEmployee(int id);
    }
}
