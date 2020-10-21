using MyPortfolio.Models;
using System.Collections.Generic;
using System.Linq;

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

        public Employee Update(Employee employeeChanges)
        {
            var employee = _employeeList.Where(e => e.Id == employeeChanges.Id).FirstOrDefault();
            if (employee != null)
            {
                employee.Name = employeeChanges.Name;
                employee.Email = employeeChanges.Email;
                employee.Department = employeeChanges.Department;
            }

            return employee;
        }

        public Employee Delete(int id)
        {
            var employee = _employeeList.Where(e => e.Id == id).FirstOrDefault();
            if (employee != null)
            {
                _employeeList.Remove(employee);
            }
            return employee;
        }
    }
}