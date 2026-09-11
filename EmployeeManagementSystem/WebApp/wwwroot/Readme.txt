
This is a readme. Here's some code:



// This service is needed for app.MapControllers() to work below.
// You can also use builder.Services.AddControllersWithViews() to support having razor pages views connected to the controllers.
builder.Services.AddControllers()
	.AddXmlSerializerFormatters(); // This call enables support for Http requests containing XML data.


var app = builder.Build();

app.UseRouting();

// This is the recommended way to map all our controller classes.
// However, controller mapping requires several services to be added above first.
app.MapControllers();

// If you don't want to use attribute-based routing in the controller classes, you can enable
// conventional routing like this:
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}" // The ? means the id parameter is optional.
);

// NOTE: You can also enable conventional routing like this in the commented out app.UseEndpoints()
//	     block above, though this is not the recommended approach.

app.Run();

