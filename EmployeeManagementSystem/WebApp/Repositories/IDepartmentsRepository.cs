using WebApp.Models;

namespace WebApp.Repositories;

public interface IDepartmentsRepository
{
	List<Department> GetDepartments(string? filter = null);
	Department? GetDepartmentById(long id);
	void AddDepartment(Department? Department);
	bool UpdateDepartment(Department? Department);
	bool DeleteDepartment(Department? Department);
}