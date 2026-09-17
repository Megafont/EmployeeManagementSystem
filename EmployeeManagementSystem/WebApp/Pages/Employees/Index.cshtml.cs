using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Repositories;

namespace WebApp.Pages.Employees
{
    public class IndexModel : PageModel
    {
        public List<Employee>? Employees { get; set; }

        public void OnGet()
        {
            // This is no longer needed, since the employees list is loaded by the javascript in
            // Pages/Shared/Components/EmployeeList/Default.cshtml.
	        //Employees = EmployeesRepository.GetEmployees();
        }

        public void OnGetTest()
        {
            Console.WriteLine("OnGetTest hit!");
        }

        public IActionResult OnGetSearchEmployeesResult(string? filter)
        {
	        return ViewComponent("EmployeeList", new { filter });
        }
    }
}
