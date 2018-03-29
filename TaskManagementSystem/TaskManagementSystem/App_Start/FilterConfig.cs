using System;
using System.Web;
using System.Web.Mvc;

namespace TaskManagementSystem
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {           
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new RequiredSecureConnectionAttribute());
        }
    }

    public class RequiredSecureConnectionAttribute : RequireHttpsAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext can not be null");
            }

            if (filterContext.HttpContext.Request.IsLocal)
            {
                return; // do nothing. leave the request to go through.
            }

            base.OnAuthorization(filterContext);
        }
    }
}
