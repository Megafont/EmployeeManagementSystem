using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Helpers;
using WebApp.Repositories;
using WebApp.ViewModels;

namespace WebApp.Pages.Employees
{
    public class CreateModel : PageModel
    {
        // This attribute makes it so that when the form is submitted, its data will get written into the corresponding fields of this property.
        [BindProperty]
        public EmployeeViewModel? EmployeeViewModel { get; set; }


        public void OnGet()
        {
            EmployeeViewModel = new();
            EmployeeViewModel.Employee = new();
            EmployeeViewModel.Departments = DepartmentsRepository.GetDepartments();
        }

        public IActionResult OnPost()
        {
	        if (!ModelState.IsValid)
	        {
		        var errors = ModelStateHelper.GetErrors(ModelState);
		        return RedirectToPage("/Error", new { errors });
	        }


	        if (EmployeeViewModel != null && EmployeeViewModel.Employee != null)
                EmployeesRepository.AddEmployee(EmployeeViewModel.Employee);

	        return RedirectToPage("Index");
        }
    }
}
