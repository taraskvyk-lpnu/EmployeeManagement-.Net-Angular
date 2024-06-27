using Microsoft.AspNetCore.Mvc.Filters;

namespace EmployeeManagment.Filters;

public class AuthorizationFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        Console.WriteLine("AuthorizationFilter Executed");
    }
}