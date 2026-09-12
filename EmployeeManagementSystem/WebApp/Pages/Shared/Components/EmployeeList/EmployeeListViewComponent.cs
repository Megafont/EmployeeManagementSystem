using Microsoft.AspNetCore.Mvc;
using WebApp.Repositories;

namespace WebApp.Pages.Shared.Components.EmployeeList
{
	public class EmployeeListViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke(string? filter)
		{
			return View(EmployeesRepository.GetEmployees(filter));
		}
	}

}
