using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApp.Helpers
{
	public static class ModelStateHelper
	{
		public static List<string> GetErrors(ModelStateDictionary modelState)
		{
			List<string> errorMessages = new List<string>();

			// Get the error messages from the model state.
			foreach (var value in modelState.Values)
			{
				foreach (var error in value.Errors)
				{
					errorMessages.Add(error.ErrorMessage);
				}
			}

			return errorMessages;
		}

		public static string GetErrorsAsHTML(ModelStateDictionary modelState)
		{
			List<string> errorMessages = GetErrors(modelState);

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
