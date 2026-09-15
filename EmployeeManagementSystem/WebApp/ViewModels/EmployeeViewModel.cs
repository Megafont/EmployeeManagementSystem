using WebApp.Models;

namespace WebApp.ViewModels
{
	/// <summary>
	/// This class is used as the view model instead of Employee, because we need to supply extra information to the front end that
	/// does not belong in the Employee class. Another reason to have a view model class like this, would be if the Employee class
	/// contains sensitive information that should not be supplied to the front-end.
	/// </summary>
	public class EmployeeViewModel
	{
		public Employee? Employee { get; set; }
		public List<Department>? Departments { get; set; }
	}
}
