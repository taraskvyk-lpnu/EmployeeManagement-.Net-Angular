using Microsoft.AspNetCore.Mvc.Filters;

namespace EmployeeManagment.Filters;

public class ResourseFilter : IResourceFilter
{
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        Console.WriteLine("ResourceFilter Executing");
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        Console.WriteLine("ResourceFilter Executed");
    }
}