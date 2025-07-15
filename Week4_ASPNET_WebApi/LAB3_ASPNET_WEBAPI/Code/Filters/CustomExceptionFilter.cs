using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;

namespace Lab3_CustomWebApi.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exceptionMessage = $"Exception occurred at {DateTime.Now}: {context.Exception.Message}";
            File.AppendAllText("logs.txt", exceptionMessage + "\n");

            context.Result = new ObjectResult("An internal error occurred")
            {
                StatusCode = 500
            };
        }
    }
}