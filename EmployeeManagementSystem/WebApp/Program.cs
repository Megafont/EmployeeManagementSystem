var builder = WebApplication.CreateBuilder(args);

// This service is needed for app.MapControllers() to work below.
// You can also use builder.Services.AddControllersWithViews() instead of builder.Services.AddControllers() as
// we're doing here to support having razor pages views connected to the controllers.
builder.Services.AddControllersWithViews();
	//.AddXmlSerializerFormatters(); // This call enables support for Http requests containing XML data.

// This service is needed for the app.MapRazorPages() call below to work.
builder.Services.AddRazorPages();


var app = builder.Build();

// This middleware allows us to use static files in the wwwroot folder.
// Another newer middleware we could use here instead is app.MapStaticFiles(). This one enables cache and adding version number to the query string automatically.
// So it's a bit easier than doing it explicitly like we did by adding the asp-append-version attribute in Views/Home/Index.cshtml.
app.UseStaticFiles(new StaticFileOptions 
{
	OnPrepareResponse = ctx =>
	{
		// These settings are enabling browser caching on our website.
		ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=600");
		ctx.Context.Response.Headers.Append("Expires", DateTime.UtcNow.AddMinutes(10).ToString());
	}
});


app.UseRouting();


// CONTROLLER BINDING SOURCE PRIORITY
// -------------------------------------
// The binding priority for routing parameters in controllers is very similar to that for minimal APIs.
// If you compare this to that list in Program.cs of the section 5 app, you'll see this one is slightly
// different:
//
// 1. Explicit via the attributes (FromRoute, FromQuery, FromHeaders).
// 2. When an endpoint parameter is a complex type that implements the static BindAsync() method.
// 3. Bind any type (primitive or complex) to a form field
// 4. When an endpoint parameter is a primitive type.
// 5. When an endpoint parameter is bound to a query string.
// 6. When an array is bound to form fields, query string, or headers.
//
// Tell ASP.NET to map our controller classes containing most of our end points.
// However, this code is commented out, as it is not the recommended way to do this.
// This is the older way to do it.
//app.UseEndpoints(endpoints =>
//{
//	endpoints.MapControllers();
//
//	endpoints.MapRazorPages();
//});

// This is the recommended way to map all our controller classes.
// However, controller mapping requires several services to be added above first.
app.MapControllers();

// If you don't want to use attribute-based routing in the controller classes, you can enable
// conventional routing like this:
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}" // The ? means the id parameter is optional.
);

app.MapRazorPages();

// NOTE: You can also enable conventional routing like this in the commented out app.UseEndpoints()
//	     block above, though this is not the recommended approach.

app.Run();
