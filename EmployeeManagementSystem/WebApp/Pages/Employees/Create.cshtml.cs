using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Helpers;
using WebApp.Repositories;
using WebApp.ViewModels;

namespace WebApp.Pages.Employees
{
    /// <summary>
    /// This is the model class for the create employee page. Razor pages uses page model classes
    /// rather than controller classes like Views do.
    /// </summary>
    public class CreateModel : PageModel
    {
	    private readonly IDepartmentsRepository _departmentsRepository;
	    private readonly IEmployeesRepository _employeesRepository;

	    public CreateModel(
		    IDepartmentsRepository departmentsRepository,
		    IEmployeesRepository employeesRepository)
	    {
		    _departmentsRepository = departmentsRepository;
		    _employeesRepository = employeesRepository;
	    }

        // This attribute makes it so that when the form is submitted, its data will get written into the corresponding fields of this property.
        [BindProperty]
        public EmployeeViewModel? EmployeeViewModel { get; set; }


        public void OnGet()
        {
            EmployeeViewModel = new();
            EmployeeViewModel.Employee = new();
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
                _employeesRepository.AddEmployee(EmployeeViewModel.Employee);

	        return RedirectToPage("Index");
        }
    }
}
