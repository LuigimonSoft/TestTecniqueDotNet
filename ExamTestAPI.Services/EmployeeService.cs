using ExamTestAPI.DTO;
using ExamTestAPI.Models;
using ExamTestAPI.Repositories.Interfaces;
using ExamTestAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTestAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        public readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDto?> AddEmployee(EmployeeDto employee)
        {

            if (!await EmailExist(employee.Email))
            {
                var newEmployee = MapEmployeeDtoToEmployee(employee);
                var newEmployeeCreated = await _employeeRepository.AddEmployee(newEmployee);
                if(newEmployeeCreated != null)
                    return MapEmployeeToEmployeeDto(newEmployeeCreated);
                else 
                    return null;
            }
            else
                RaiseEmailExistException();
            return null;
        }

        public async Task<EmployeeDto?> GetEmployeeById(int id)
        {
            var employee = await _employeeRepository.GetEmployeeById(id);
            return employee != null ? MapEmployeeToEmployeeDto(employee) : null;
        }

        public async Task<List<EmployeeDto>> GetEmployees()
        {
            var employees = await _employeeRepository.GetEmployees();
            return MapEmployeesToEmployeeDtos(employees);
        }

        public async Task<bool> UpdateEmployee(EmployeeDto employee)
        {
            if (!await EmailExist(employee.Email))
            {
                var employeeToUpdate = MapEmployeeDtoToEmployee(employee);
                return await _employeeRepository.UpdateEmployee(employeeToUpdate);
            }
            else
                RaiseEmailExistException();
            return false;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            return await _employeeRepository.DeleteEmployee(id);
        }

        private async Task<bool> EmailExist(string email)
        {
            return await _employeeRepository.EmailExist(email);
        }

        private void RaiseEmailExistException()
        {
            throw new Exception("Email already exists");
        }

        private EmployeeDto MapEmployeeToEmployeeDto(Employee employee)
        {
            return new EmployeeDto(employee.Id, employee.Name, employee.Email, employee.Position, employee.Salary);
        }

        private Employee MapEmployeeDtoToEmployee(EmployeeDto employeeDto)
        {
            return new Employee
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                Position = employeeDto.Position,
                Salary = employeeDto.Salary
            };
        }

        private List<EmployeeDto> MapEmployeesToEmployeeDtos(List<Employee> employees)
        {
            return employees.Select(e => new EmployeeDto(e.Id, e.Name, e.Email, e.Position, e.Salary)).ToList();
        }

        
    }
}
