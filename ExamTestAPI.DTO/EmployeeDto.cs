using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTestAPI.DTO
{
    public record EmployeeDto(int Id, string Name, string Email, string Position, decimal Salary);
}
