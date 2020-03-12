using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QAForumAPI.Filters.Security
{
    public class Authenticated : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("userIdSession") == default)
            {
                throw new Exception(NotLoggedInMessage());
            }
        }
        protected string NotLoggedInMessage()
        {
            return "User Not Logged In";
        }
    }
}
