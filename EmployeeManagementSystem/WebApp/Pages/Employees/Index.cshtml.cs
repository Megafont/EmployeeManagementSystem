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
	        Employees = EmployeesRepository.GetEmployees();
        }
    }
}
