using Microsoft.AspNetCore.Mvc.Filters;

namespace EmployeeManagment.Filters;

public class ResultFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        Console.WriteLine("ResultFilter Executing");
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
        Console.WriteLine("ResultFilter Executed");
    }
}