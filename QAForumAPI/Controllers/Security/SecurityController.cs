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
                return new JsonResult(new { result = "Logged in successfully" });
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
        private string GetUserIdSession()
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
    }
}
