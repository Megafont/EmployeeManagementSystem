using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Repositories;

namespace WebApp.Pages.Employees
{
	/// <summary>
	/// This is the model class for the department employees page. Razor pages uses page model classes
	/// rather than controller classes like Views do.
	/// </summary>
	public class DepartmentEmployeesModel : PageModel
    {
	    private readonly IDepartmentsRepository _departmentsRepository;

	    public DepartmentEmployeesModel(IDepartmentsRepository departmentsRepository)
	    {
		    _departmentsRepository = departmentsRepository;
	    }

        public string? DepartmentName { get; set; }

        [BindProperty(SupportsGet = true)]
        public long? DepartmentId { get; set; }


        public void OnGet()
        {
	        if (DepartmentId.HasValue)
	        {
		        var department = _departmentsRepository.GetDepartmentById(DepartmentId.Value);
		        DepartmentName = department?.Name;
	        }
        }
    }
}
