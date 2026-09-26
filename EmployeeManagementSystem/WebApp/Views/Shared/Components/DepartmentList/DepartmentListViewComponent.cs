using Microsoft.AspNetCore.Mvc;
using WebApp.Repositories;

namespace WebApp.Views.Shared.Components.DepartmentList
{
	// You can make a class be treated as a view component by naming it with the suffix "ViewComponent".
	// Alternatively, you can apply the [ViewComponent] attribute to the class instead.
	// Lastly, the third way is that you could make this class inherit from the ViewComponent base class.
	public class DepartmentListViewComponent : ViewComponent
	{
		private readonly IDepartmentsRepository _departmentsRepository;

		public DepartmentListViewComponent(IDepartmentsRepository departmentsRepository)
		{
			_departmentsRepository = departmentsRepository;
		}

		public IViewComponentResult Invoke(string? filter)
		{
			var departments = _departmentsRepository.GetDepartments(filter);
			return View(departments);
		}
	}
}
