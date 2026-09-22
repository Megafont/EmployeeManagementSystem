using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace WebApp.DemoControllers
{
	//// This attribute tells ASP.NET that this class is a controller.However, convention is that you just name the class with the
	//// suffix "Controller". This also indicates to ASP.NET that this class is a controller, and is why the Controller attribute
	//// is commented out here.
	//// [Controller]
	//public class DepartmentController
	//{
	//	// NOTE: The end point handlers below are actually referred to as action methods when using the MVC workflow like we are here.

	//  // NOTE: When no [Http...] attribute is used to specify the type of Http requests an endpoint accepts, it can accept any type
	//  //		of Http request, which is usually not good.

	//	[HttpGet("/departments")]
	//	//[Route("/departments")] // This attribute is used when you do not specify the route in the Http... attribute above.
	//	// This attribute, when applied to the controller class, specifies the route prefix for all endpoints in this controller.
	//	// So [Route("/api")] applied to the class prepends all endpoints' paths with "/api", providing a way to reduce duplicate code.
	//	public string GetDepartments()
	//	{
	//		return "These are the departments.";
	//	}

	//	[HttpGet("/departments/{id}")]
	//	//[Route("/departments")] // This attribute is used when you do not specify the route in the Http... attribute above.
	//	// This attribute, when applied to the controller class, specifies the route prefix for all endpoints in this controller.
	//	// So [Route("/api")] applied to the class prepends all endpoints' paths with "/api", providing a way to reduce duplicate code.
	//	public string GetDepartmentById(long id)
	//	{
	//		return $"Department info: {id}";
	//	}


	//	// This attribute is used to tell ASP.NET that this method is not an end point handler (aka action method).
	//	//[NonAction] 
	//	//public string GetDepartmentByName(string name)
	//	//{
	//	//	return "abc";
	//	//}
	//}

}