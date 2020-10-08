using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace MyPortfolio.Mock
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee() { Id = 1, Name = "Mary", Department = Dept.Hr, Email = "mary@alayditech.com"},
                new Employee() { Id = 2, Name = "John", Department = Dept.IT, Email = "john@alayditech.com"},
                new Employee() { Id = 3, Name = "Sam", Department = Dept.IT, Email = "sam@alayditech.com"},
            };
        }

        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeList;
        }

        public Employee Add(Employee employee)
        {
            employee.Id = _employeeList.Count == 0 ? 1 : _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        public void Delete(int id)
        {
            var employee = _employeeList.Where(e => e.Id == id).FirstOrDefault();
            _employeeList.Remove(employee);
        }
    }
}