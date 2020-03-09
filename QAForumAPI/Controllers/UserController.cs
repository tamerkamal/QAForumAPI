using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QAForumAPI.BLL.Repositories;
using QAForumAPI.BOL.Models;
using QAForumAPI.BOL.ViewModels;
using QAForumAPI.DAL;

namespace QAForumAPI.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [Route("[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {        
        private readonly IUserRepository _repo;                
        public UsersController(IUserRepository repo)
        {            
            _repo = repo;           
        }        
     
        [HttpPost]
        public async Task<JsonResult> Login(LoginViewModel loginViewModel)
        {
            await _repo.Login(loginViewModel);
            Guid currentUserId = _repo.GetCurrentUserId(loginViewModel);
           return AddUserIdSession(currentUserId);              
        }

        public JsonResult AddUserIdSession(Guid userId)
        {
            try
            {
                if (HttpContext.Session.GetString("userIdSession") == default)
                {
                    HttpContext.Session.SetString("userIdSession", userId.ToString());
                    var userIdSessionValue = HttpContext.Session.GetString("userIdSession");
                    return new JsonResult(new { result = "added userId in userIdSession Successfully" });
                }
                else
                {
                    return new JsonResult(new { result = " userIdSession already has value" });
                }
              
            }
            catch (Exception ex)
            {
                var innerEx = ex.InnerException;
                throw;
            }
        }
    }
}


