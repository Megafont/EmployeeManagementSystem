using System.Reflection;
using WebApp.Models;

namespace WebApp.Repositories
{
	public class EmployeesRepository : IEmployeesRepository
	{
		private readonly IDepartmentsRepository _departmentsRepository;

		private List<Employee> _Employees = new List<Employee>
		{
			new Employee(1, "John Doe", "Engineer", 60000, 1, "johndoe@company.com"),
			new Employee(2, "Jane Smith", "Manager", 75000, 1, "janesmith@company.com"),
			new Employee(3, "Sam Brown", "Technician", 50000, 1, "sambrown@company.com"),
			new Employee(4, "Alice Johnson", "Analyst", 55000, 2, "alicejohnson@company.com"),
			new Employee(5, "Bob Lee", "Developer", 65000, 2, "boblee@company.com"),
			new Employee(6, "Carol Wang", "Designer", 70000, 2, "carolwang@company.com"),
			new Employee(7, "David Kim", "Support", 48000, 3, "davidkim@company.com"),
			new Employee(8, "Eve Rogers", "Consultant", 72000, 3, "everogers@company.com"),
			new Employee(9, "Franklin Zhang", "Architect", 80000, 3, "franklinzhang@company.com"),
			new Employee(10, "Grace Liu", "Coordinator", 53000, 1, "graceliu@company.com"),
			new Employee(11, "Henry Thompson", "Specialist", 62000, 2, "henrythompson@company.com"),
			new Employee(12, "Isabelle Nguyen", "Technician", 57000, 3, "isabellenguyen@company.com"),
		};

		public EmployeesRepository(IDepartmentsRepository departmentsRepository)
		{
			_departmentsRepository = departmentsRepository;
		}

		public List<Employee> GetEmployees(string? filter = null, long? departmentId = null)
		{
			// This may look like a bad practice, but since we're using an in-memory repository for now, it is ok to load the department object
			// for each employee. We'll change this later.
			foreach (Employee employee in _Employees)
			{
				employee.Department = _departmentsRepository.GetDepartmentById(employee.DepartmentId);
			}

			if (departmentId.HasValue)
			{
				return _Employees.Where(x => x.DepartmentId == departmentId.Value).ToList();
			}
			else if (!string.IsNullOrWhiteSpace(filter))
			{
				return _Employees.Where(x => x.Name != null && x.Name.ToLower().Contains(filter.ToLower())).ToList();
			}

			return _Employees;
		}

		public Employee? GetEmployeeById(long id)
		{
			return _Employees.FirstOrDefault(x => x.Id == id);
		}

		public void AddEmployee(Employee? employee)
		{
			if (employee != null)
			{
				long maxId = _Employees.Max(x => x.Id);
				employee.Id = maxId + 1;
				_Employees.Add(employee);
			}
		}

		public bool UpdateEmployee(Employee? employee)
		{
			if (employee != null)
			{
				var repoEntry = _Employees.FirstOrDefault(x => x.Id == employee.Id);
				if (repoEntry != null)
				{
					repoEntry.Name = employee.Name;
					repoEntry.Position = employee.Position;
					repoEntry.Salary = employee.Salary;
					repoEntry.DepartmentId = employee.DepartmentId;
					repoEntry.Email = employee.Email;

					return true;
				}
			}

			return false;
		}

		public bool DeleteEmployee(Employee? employee)
		{
			if (employee != null)
			{
				var repoEntry = _Employees.FirstOrDefault(x => x.Id == employee.Id);
				if (repoEntry != null)
				{
					_Employees.Remove(repoEntry);
					return true;
				}
			}

			return false;
		}
	}
}
