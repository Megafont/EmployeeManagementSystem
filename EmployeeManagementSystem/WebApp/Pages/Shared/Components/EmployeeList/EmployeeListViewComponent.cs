using Microsoft.AspNetCore.Mvc;
using WebApp.Repositories;

namespace WebApp.Pages.Shared.Components.EmployeeList
{
	public class EmployeeListViewComponent : ViewComponent
	{
		private readonly IEmployeesRepository _employeesRepository;

		public EmployeeListViewComponent(IEmployeesRepository employeesRepository)
		{
			_employeesRepository = employeesRepository;
		}

		public IViewComponentResult Invoke(string? filter, long? departmentId)
		{
			return View(_employeesRepository.GetEmployees(filter, departmentId));
		}
	}

}
