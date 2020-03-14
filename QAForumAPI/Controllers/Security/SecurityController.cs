using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QAForumAPI.Controllers.Security
{
    public class SecurityController : ControllerBase
    {
        protected JsonResult AddUserIdSession(Guid userId)
        {
            try
            {
                HttpContext.Session.SetString("userIdSession", userId.ToString());
                return new JsonResult(new { message = "Logged in successfully" });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
            }
        }
        private string GetUserIdSession()
        {
            try
            {
                return HttpContext.Session.GetString("userIdSession"); ;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
