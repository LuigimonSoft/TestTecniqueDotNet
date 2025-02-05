using ExamTestAPI.Models;
using ExamTestAPI.Repositories.Context;
using ExamTestAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTestAPI.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _employeeDbContext;
        public EmployeeRepository(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _employeeDbContext.Employees.ToListAsync();
        }

        public async Task<Employee?> GetEmployeeById(int id)
        {
            return await _employeeDbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            _employeeDbContext.Employees.Add(employee);
            await _employeeDbContext.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            _employeeDbContext.Employees.Update(employee);
            int res = await _employeeDbContext.SaveChangesAsync();
            return res > 0;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
           var employee = await _employeeDbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
                return false;
            
            _employeeDbContext.Employees.Remove(employee);
            return await _employeeDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> EmailExist(string email)
        {
            return (await _employeeDbContext.Employees.CountAsync(e => e.Email == email) )> 0;
        }
    }
}
