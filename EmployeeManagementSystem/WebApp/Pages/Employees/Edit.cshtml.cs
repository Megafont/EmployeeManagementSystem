using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Helpers;
using WebApp.Repositories;
using WebApp.ViewModels;

namespace WebApp.Pages.Employees
{
	/// <summary>
	/// This is the model class for the edit employee page. Razor pages uses page model classes
	/// rather than controller classes like Views do.
	/// </summary>
	public class EditModel : PageModel
    {
	    private readonly IDepartmentsRepository _departmentsRepository;
	    private readonly IEmployeesRepository _employeesRepository;

	    // This attribute makes it so that when the form is submitted, its data will get written into the corresponding fields of this property.
	    [BindProperty]
		public EmployeeViewModel? EmployeeViewModel { get; set; }


	    public EditModel(
		    IDepartmentsRepository departmentsRepository,
		    IEmployeesRepository employeesRepository)
	    {
		    _departmentsRepository = departmentsRepository;
		    _employeesRepository = employeesRepository;
	    }

        public void OnGet(long id)
        {
	        EmployeeViewModel = new();
	        EmployeeViewModel.Employee = _employeesRepository.GetEmployeeById(id);
	        EmployeeViewModel.Departments = _departmentsRepository.GetDepartments();
        }

        public IActionResult OnPost()
        {
	        if (!ModelState.IsValid)
	        {
		        var errors = ModelStateHelper.GetErrors(ModelState);
		        return RedirectToPage("/Error", new { errors });
	        }


	        if (EmployeeViewModel != null && EmployeeViewModel.Employee != null)
	        {
		        _employeesRepository.UpdateEmployee(EmployeeViewModel.Employee);
	        }

	        return RedirectToPage("Index");
        }

        public IActionResult OnPostDeleteEmployee(long id)
        {
	        var employee = _employeesRepository.GetEmployeeById(id);
	        if (employee == null)
	        {
				ModelState.AddModelError("id", "Employee not found!");
				
				var errors = ModelStateHelper.GetErrors(ModelState);

				RedirectToPage("/Error", new { errors });
	        }


	        _employeesRepository.DeleteEmployee(employee);

	        return RedirectToPage("Index");
        }
    }
}
