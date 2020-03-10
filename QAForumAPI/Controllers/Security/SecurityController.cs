using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QAForumAPI.Controllers.Security
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        protected JsonResult AddUserIdSession(Guid userId)
        {
            try
            {
                HttpContext.Session.SetString("userIdSession", userId.ToString());
                return new JsonResult(new { result = "added userId to userIdSession Successfully" });
            }
            catch (Exception ex)
            {
                string exMessage = ex.Message;
                throw;
            }
        }
        protected Guid GetCurrentUserId()
        {
            try
            {
                return Guid.Parse(GetUserIdSession());
            }
            catch (Exception ex)
            {
                string exMessage = ex.Message;
                throw;
            }
        }
        protected string GetUserIdSession()
        {
            try
            {
                return HttpContext.Session.GetString("userIdSession"); ;
            }
            catch (Exception ex)
            {
                string exMessage = ex.Message;
                throw;
            }
        }
        protected bool IsAuthenticated()
        {
            try
            {
                if (GetUserIdSession() != default)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string exMessage = ex.Message;
                throw;
            }
        }
        protected string NotLoggedInMessage()
        {
            return "User Not Logged In";
        }
    }
}
