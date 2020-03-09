using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QAForumAPI.BLL.Repositories;
using QAForumAPI.BOL.ViewModels;

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
        [Route("/login")]
        public async Task<JsonResult> Login(LoginViewModel loginViewModel)
        {
            await _repo.Login(loginViewModel);
            Guid currentUserId = _repo.GetCurrentUserId(loginViewModel);
            return AddUserIdSession(currentUserId);
        }
        private JsonResult AddUserIdSession(Guid userId)
        {
            try
            {
                if (HttpContext.Session.GetString("userIdSession") == default)
                {
                    HttpContext.Session.SetString("userIdSession", userId.ToString());
                    string userIdSessionValue = HttpContext.Session.GetString("userIdSession");
                    return new JsonResult(new { result = "added userId to userIdSession Successfully" });
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


