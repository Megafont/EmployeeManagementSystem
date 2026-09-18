using System.Reflection;
using System.Security.AccessControl;
using System.Xml.Linq;
using WebApp.Models;

namespace WebApp.Repositories
{
    public static class DepartmentsRepository
    {
        private static List<Department> _Departments = new List<Department>
        {
            new Department(1, "Sales", "Sales Department", "sales@company.com"),
            new Department(2, "Engineering", "Engineering Department", "engineering@company.com"),
            new Department(3, "QA", "Quality Assurance Department", "qa@company.com"),
            new Department(4, "IT", "IT Department", "it@company.com"),
        };

        public static List<Department> GetDepartments(string? filter = null)
        {
	        if (string.IsNullOrWhiteSpace(filter))
		        return _Departments;

	        return _Departments.Where(x => x.Name != null && x.Name.ToLower().Contains(filter.ToLower())).ToList();
        }

        public static Department? GetDepartmentById(int id)
        {
            return _Departments.FirstOrDefault(x => x.Id == id);
        }

        public static void AddDepartment(Department? Department)
        {
            if (Department is not null)
            {
                if (_Departments.Any())
                {
                    int maxId = _Departments.Max(x => x.Id);
                    Department.Id = maxId + 1;
                }
                else
                {
                    Department.Id = 1;
                }

                _Departments.Add(Department);
            }
        }

        public static bool UpdateDepartment(Department? Department)
        {
            if (Department is not null)
            {
                var emp = _Departments.FirstOrDefault(x => x.Id == Department.Id);
                if (emp is not null)
                {
                    emp.Name = Department.Name;
                    emp.Description = Department.Description;
                    emp.Email = Department.Email;

                    return true;
                }
            }

            return false;
        }

        public static bool DeleteDepartment(Department? Department)
        {
            if (Department is not null)
            {
                _Departments.Remove(Department);
                return true;
            }

            return false;
        }
    }
}
