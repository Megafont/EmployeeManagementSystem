using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Department
    {
        public Department()
        {
            
        }

        public Department(long id, string name, string? description = "", string? email = "")
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Email = email;
        }

        // Since we've updated our HTML input tags to use the asp-for attribute, we need to add this HiddenInput attribute.
        // That way it nose Id should be a hidden field on the HTML form.
        [HiddenInput]
        public long Id { get; set; }

        // NOTE: For the [Required] attribute and all validation attributes like [StringLength], you can also override the default error message with your own
        //       by specifying a value for the ErrorMessage param, like this: [StringLength(500, ErrorMessage="This is my custom error message")]
        [Required]
        public string? Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [EmailAddress]
        // [Display(Name = "Email Address")]  // This attribute lets us set the text to display for this fields label in the HTML form since we changed the label tags to also use the asp-for attribute.
        public string? Email { get; set; }
    }
}
