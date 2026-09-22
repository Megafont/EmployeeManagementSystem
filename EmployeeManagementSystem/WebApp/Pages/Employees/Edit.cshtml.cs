using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Helpers;
using WebApp.Repositories;
using WebApp.ViewModels;

namespace WebApp.Pages.Employees
{
    public class EditModel : PageModel
    {
	    // This attribute makes it so that when the form is submitted, its data will get written into the corresponding fields of this property.
	    [BindProperty]
		public EmployeeViewModel? EmployeeViewModel { get; set; }


        public void OnGet(long id)
        {
	        EmployeeViewModel = new();
	        EmployeeViewModel.Employee = EmployeesRepository.GetEmployeeById(id);
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
	        {
		        EmployeesRepository.UpdateEmployee(EmployeeViewModel.Employee);
	        }

	        return RedirectToPage("Index");
        }

        public IActionResult OnPostDeleteEmployee(long id)
        {
	        var employee = EmployeesRepository.GetEmployeeById(id);
	        if (employee == null)
	        {
				ModelState.AddModelError("id", "Employee not found!");
				
				var errors = ModelStateHelper.GetErrors(ModelState);

				RedirectToPage("/Error", new { errors });
	        }


	        EmployeesRepository.DeleteEmployee(employee);

	        return RedirectToPage("Index");
        }
    }
}
