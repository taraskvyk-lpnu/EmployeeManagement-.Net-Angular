using Microsoft.AspNetCore.Mvc.Filters;

namespace EmployeeManagment.Filters;

public class ActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("ActionFilter Executing");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("ActionFilter Executed");
    }
}