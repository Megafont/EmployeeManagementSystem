using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
	public class Employee
	{
		// Since we've updated our HTML input tags to use the asp-for attribute, we need to add this HiddenInput attribute.
		// That way it nose Id should be a hidden field on the HTML form.
		[HiddenInput]
		public int Id { get; set; }

		[Required]
		public string? Name { get; set; }

		[Required]
		public string? Position { get; set; }

		public decimal? Salary { get; set; }

		[EmailAddress]
		// [Display(Name = "Email Address")]  // This attribute lets us set the text to display for this fields label in the HTML form since we changed the label tags to also use the asp-for attribute.
		public string? Email { get; set; }

		[Display(Name = "Department")]
		public int DepartmentId { get; set; }

		// In Entity Framework, this would be called a Navigation property.
		public Department? Department { get; set; }


		public Employee()
		{

		}

		public Employee(int id, string name, string position, decimal salary, int departmentId, string? email)
		{
			Id = id;
			Name = name;
			Position = position;
			Salary = salary;
			DepartmentId = departmentId;
			Email = email;
		}
	}
}
