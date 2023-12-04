using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;

namespace AirlinesMvcApp.Filters
{
    public class CustomExceptionFilter : IExceptionFilter {
        private readonly ILogger<CustomExceptionFilter> _logger;
        private readonly IModelMetadataProvider _modelMetadataProvider;
        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger, IModelMetadataProvider modelMetadataProvider)
        {
            _logger = logger;
            _modelMetadataProvider = modelMetadataProvider;
        }
        public void OnException(ExceptionContext context)
        {
            var result = new ViewResult { ViewName = "Error" };
            result.ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState);
            result.ViewData.Add("ErrorMessage", context.Exception.Message);
            _logger.LogError("Error: " + context.Exception.Message);
            context.ExceptionHandled = true;
            context.Result = result;
        }
    }
}
