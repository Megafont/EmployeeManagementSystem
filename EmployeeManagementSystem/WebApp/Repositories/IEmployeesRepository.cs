using WebApp.Models;

namespace WebApp.Repositories;

public interface IEmployeesRepository
{
	List<Employee> GetEmployees(string? filter = null, long? departmentId = null);
	Employee? GetEmployeeById(long id);
	void AddEmployee(Employee? employee);
	bool UpdateEmployee(Employee? employee);
	bool DeleteEmployee(Employee? employee);
}