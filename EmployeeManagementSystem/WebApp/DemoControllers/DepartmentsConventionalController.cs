using System.IO.Enumeration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Net.Mime;
using WebApp.Models;

namespace WebApp.DemoControllers
{
	/*

	// IMPORTANT: To access the endpoints in this controller, the base URL is "localhost:<portnumber>/departmentsconventional"
	//			  This is because ASP.NET is making this route based on the controller's class name.
	//			  I tried to fix this by adding a [Route] parameter to the class to fix this, but that doesn't work, as you can
	//			  see by the note below about NOT adding a [Route] attribute to a conventional style controller class.
	//			  The conventional style of controller coding is better for MVC apps with views. Meanwhile, just using the
	//			  [Http...] attributes alone (attribute routing) as shown in DepartmentsController.cs is better for REST APIs.

	// NOTE: To use the conventional style of endpoint implementations shown here, you need to enable it in Program.cs
	//       via app.MapControllerRoute() instead of app.MapControllers();

	// This attribute tells ASP.NET that this class is a controller.However, convention is that you just name the class with the
	// suffix "Controller". This also indicates to ASP.NET that this class is a controller, and is why the Controller attribute
	// is commented out here.
	// [Controller]
	// NOTE: Do NOT put a [Route] attribute on a conventionally-routed controller class. Doing so makes ASP.NET treat it as
	//       an attribute-routed controller, which means all action methods without their own [Route] attributes will resolve
	//       to the same URL path, causing an ambiguous route exception.
	//
	// The ApiController attribute is used when the controller is being used to build an API. In otherwords, its being used similar
	// to a minimal API. In this case, the controller will throw errors immediately like minimal API endpoints do, rather than
	// waiting for you to handle it by looking at the ModelState data as shown in the Create() method below.
	//[ApiController]
	public class DepartmentsConventionalController : Controller // This base class is optional, but we need it to access errors for missing routing parameters, which are stored in the model state that it stores. It also provides lots of helper methods.
	{
		// NOTE: The end point handlers below are actually referred to as action methods when using the MVC workflow like we are here.

		// NOTE: When no [Http...] attribute is used to specify the type of Http requests an endpoint accepts, it can accept any type
		//		of Http request, which is usually not good.

		// The conventional way to implement this controller's route end point.
		[HttpGet]
		public IActionResult Index()
		{
			//return new ContentResult { Content = "Welcome to the website!", ContentType = "text/plain" };

			//return new ContentResult()
			//{
			//	Content = "<h1>Departments</h1> Welcome to the departments page!",
			//	ContentType = "text/html"
			//};

			// ASP.Net also has helper methods to simply the above code:
			return Content("<h1>Departments</h1> Welcome to the departments page!",
				"text/html");
		}

		// The conventional way to implement a GET endpoint that gets an item by Id.
		[HttpGet]
		public IActionResult Details(long? id)
		{
			// REDIRECT RESULTS
			// --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

			// This result type allows us to redirect the user to a different end point by specifying the endpoint name, the
			// controller its in, and any parameters it needs.
			//return new RedirectToActionResult("GetEmployeesByDepartment", "Employees", new { id = id });
			// Here's a better way to write this. It's a bit longer, but it won't break if the end point or controller's name gets changed.
			return new RedirectToActionResult(
				nameof(EmployeesController.GetEmployeesByDepartment), 
				nameof(EmployeesController).Replace("Controller", ""), new { id = id });


			// This result type allows us to redirect the user to a specific url.
			// In this case, we're redirecting to the same page as the RedirectToAction sample above.
			//return new LocalRedirectResult($"/employees/GetEmployeesByDepartment/{id}");

			// This result type allows us to redirect the user to a specific external url.
			// It could possibly also be used for an internal url on our own site, but this result type
			// is generally used for redirecting to an external url.
			return new RedirectResult("http://www.google.com");

			// Lastly, the three redirect result types also have convenience methods like most of the result types do:
			//return RedirectToAction(
			//	nameof(EmployeesController.GetEmployeesByDepartment),
			//	nameof(EmployeesController).Replace("Controller", ""), new { id = id }););
			//return LocalRedirect($"/employees/GetEmployeesByDepartment/{id}");
			//return Redirect("http://www.google.com");

			// IMPORTANT: These redirect result types are also used when an end point is retired to redirect the user to
			//			  the new end point that's replaced it, or to a page explaining the situation. All three of these
			//			  redirect results have an alternate permanent version that you would use in this case, which
			//			  would give an Http status code of 301 (moved permanently). 


			// JSON RESULT
			// --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

			// Using JsonResult forces this endpoint to return its response in JSON regardless
			// of what type the default formatter is.
			//return new JsonResult(new Department { Id = 1, Name = "Sales" });

			// As with the Content() convenience method shown in the previous endpoint, there is one for JSON as well:
			//return Json(new Department { Id = 1, Name = "Sales" });

			// If we did this instead, the response would likely be in JSON format since that is the normal default.
			// Note that this line will be an error until the return type of this function is changed back to string.
			//return new Department { Id = 1, Name = "Sales" };
		}

		// The conventional way to implement a POST endpoint.
		[HttpPost]
		public string Create([FromBody] Department department)
		{
			foreach (var value in ModelState.Values)
			{
				foreach (var error in value.Errors)
				{
					Console.WriteLine(error);
				}
			}

			// You can also add custom validation errors into the model state like this:
			//ModelState.AddModelError("Description", "Description is required!");


			return "Created a new department.";
		}

		// The conventional way to implement a DELETE endpoint.
		// IMPORTANT: When the user is deleting something from a form, there is a delete button setup as a submit button.
		//			  When the user clicks that button, it will generate an HttpPost request, so we'd actually use the
		//			  [HttpPost] attribute here in this case.
		[HttpDelete]
		public string Delete(long? id)
		{
			return $"Deleting department: {id}";
		}

		// The conventional way to implement a PUT endpoint.
		// IMPORTANT: When the user is editing something from a form, there is a save button setup as a submit button.
		//			  When the user clicks that button, it will generate an HttpPost request, so we'd actually use the
		//			  [HttpPost] attribute here in this case.
		[HttpPut]
		public string Edit(long? id)
		{
			return $"Editing department: {id}";
		}

		/// <summary>
		/// This endpoint demonstrates putting a file from the wwwroot folder in the response.
		/// </summary>
		[Route("/download_vf")]
		public IActionResult ReturnVirtualFile()
		{
			//return new VirtualFileResult("/readme.txt", "text/plain");

			// As with the previous examples like result types shown above like Content() and Json(),
			// this one also has a convenience method to make the code simpler than the line above.
			// This File() method has a ton of overloads as well. You may also notice this is using the same
			// convenience method as the ReturnContentFile() endpoint below:
			return File("/readme.txt", "text/plain");
		}

		/// <summary>
		/// This endpoint demonstrates putting a file from somewhere other than the wwwroot folder in the response.
		/// In this case, it tries to return a pdf file in c:\Font\Articles\Programming\Clean Code.pdf. If the file
		/// exists at that path, it'll return it in the response and the user's browser will open the .pdf file.
		/// This is because we set the content type to "application/pdf". If you set it to the generic type 
		/// "application/octet-stream", then this won't happen since the browser doesn't know its type, so it will
		/// download the file rather than open it in the browser window.
		/// </summary>
		[Route("/download_pf")]
		public IActionResult ReturnPhysicalFile()
		{
			// We're using the @ here to indicate that this string should be treated as a verbatim string literal.
			// This way it won't treat the \s as escape characters.
			//return new PhysicalFileResult(@"c:\Font\Articles\Programming/Clean Code.pdf", "application/pdf");

			// As with the previous content types demonstrated in this file, the PhysicalFileResult type also has
			// a convenience method:
			return PhysicalFile(@"c:\Font\Articles\Programming\Clean Code.pdf", "application/pdf");
		}

		/// <summary>
		/// This endpoint demonstrates copying the contents of a file somewhere other than the wwwroot folder
		/// into the response. Again, it'll return it in the response and the user's browser will open the .pdf file.
		/// This is because we set the content type to "application/pdf". If you set it to the generic type
		/// "application/octet-stream", then this won't happen since the browser doesn't know its type, so it will
		/// download the file rather than open it in the browser window.
		/// 
		/// This endpoint just uses the same .pdf file as the previous end point.
		/// </summary>
		[Route("/download_cf")]
		public IActionResult ReturnContentFile()
		{
			byte[] bytes = System.IO.File.ReadAllBytes(@"c:\Font\Articles\Programming\Clean Code.pdf");

			//return new FileContentResult(bytes, "application/pdf");

			// As with the previous content types demonstrated in this file, the FileContentResult type also has
			// a convenience method. You may also notice this is using the same convenience method as the ReturnVirtualFile()
			// endpoint above:
			return File(bytes, "application/pdf");
		}
	}

	*/
}