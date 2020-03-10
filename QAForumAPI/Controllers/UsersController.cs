using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QAForumAPI.BLL.Repositories;
using QAForumAPI.BOL.ViewModels;

namespace QAForumAPI.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUsersRepository _repo;
        public UsersController(IUsersRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        [Route("/login")]
        public async Task<JsonResult> Login(LoginViewModel loginViewModel)
        {
            await _repo.Login(loginViewModel);
            Guid currentUserId = _repo.GetCurrentUserId(loginViewModel);
            return base.AddUserIdSession(currentUserId);
        }
    }
}


