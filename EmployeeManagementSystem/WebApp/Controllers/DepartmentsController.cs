using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;

namespace WebApp.Controllers
{
	public class DepartmentsController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			//var departments = DepartmentsRepository.GetDepartments();

			// I had to do this to get around this line having two types of quotes, which isn't possible in the verbatim string var (html) below.
			// This was originally coded in the html verbatim string var below as "<a class='btn btn-Primary' href='/departments/create'>Add Department</a>".
			//string buttonOnClick = "onClick=\"window.location.href='/departments/create'\"";


			return View();   // This was return View(departments); before lesson 112 in the course.
		}

		// This could be [Route] or [HttpGet].
		/*
		[HttpGet("/department-list/{filter?}")]
		public IActionResult SearchDepartments(string? filter)
		{
			var departments = DepartmentsRepository.GetDepartments(filter);
			//return PartialView("_DepartmentList", departments);
		}
		*/

		// This could be [Route] or [HttpGet].
		[HttpGet("/department-list/{filter?}")]
		public IActionResult SearchDepartments(string? filter)
		{
			//return View("_DepartmentList", departments);
			return ViewComponent("DepartmentList", new { filter });
		}

		[HttpGet]
		public IActionResult Details(int id)
		{
			var department = DepartmentsRepository.GetDepartmentById(id);
			if (department == null)
			{
				return View("Error", new List<string> { "Department not found!" });
			}

			// I had to do this to get around this line having two types of quotes, which isn't possible in the verbatim string var (html) below.
			// This was originally coded in the html verbatim string var below as "<a href='/departments'>Cancel</a>".
			string cancelButtonOnClick = "onClick=\"window.location.href='/departments'\"";

			// We don't have to specify the view name here, since it is the same as the name of this endpoint handler (aka action method).
			return View(department);
		}

		// This is Post instead of Put because HTML forms only support Get and Post.
		[HttpPost]
		public IActionResult Edit(Department department) // NOTE: We can apply the [FromForm] attribute on this parameter, but this is optional.
		{
			if (!ModelState.IsValid)
			{
				return View("Error", GetErrors());
			}


			DepartmentsRepository.UpdateDepartment(department);

			// We are not specifying the controller name here, since we are redirecting to an endpoint in the same controller, so it is not necessary.
			return RedirectToAction(nameof(Index));
		}

		[HttpGet] // This is HttpGet rather than HttpPost because it simply returns the form html. The form then sends it to our separate HttpPost endpoint named Create.
		public IActionResult Create()
		{
			// I had to do this to get around this line having two types of quotes, which isn't possible in the verbatim string var (html) below.
			// This was originally coded in the html verbatim string var below as "<a href='/departments'>Cancel</a>".
			string buttonOnClick = "onClick=\"window.location.href='/departments'\"";


			return View(new Department());
		}

		[HttpPost]
		public IActionResult Create(Department department)
		{
			if (!ModelState.IsValid)
			{
				return View("Error", GetErrors());
			}

			DepartmentsRepository.AddDepartment(department);

			// We are not specifying the controller name here, since we are redirecting to an endpoint in the same controller, so it is not necessary.
			return RedirectToAction(nameof(Index));
		}

		[HttpPost] // This is Post instead of Delete because HTML forms only support Get and Post.
		public IActionResult Delete(int id)
		{
			Department department = DepartmentsRepository.GetDepartmentById(id);

			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("id", "Department not found!");

				return View("Error", GetErrors());
			}


			DepartmentsRepository.DeleteDepartment(department);

			return RedirectToAction(nameof(Index));
		}

		private List<string> GetErrors()
		{
			List<string> errorMessages = new List<string>();

			// Get the error messages from the model state.
			foreach (var value in ModelState.Values)
			{
				foreach (var error in value.Errors)
				{
					errorMessages.Add(error.ErrorMessage);
				}
			}


			return errorMessages;
		}

		private string GetErrorsAsHTML()
		{
			List<string> errorMessages = GetErrors();


			// Format the error messages in HTML.
			string html = string.Empty;
			if (errorMessages.Count > 0)
			{
				html = $@"
					<ul>
						{string.Join("", errorMessages.Select(error => $"<li style='color:red;'>{error}</li>"))}		
					</ul>";
			}

			return html;
		}
	}
}
