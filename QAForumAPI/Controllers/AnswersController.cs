//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
////using RouteAttribute = System.Web.Http.RouteAttribute;
////using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
////using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
//using QAForumAPI.BLL.Repositories;
//using QAForumAPI.BOL.Models;
//using QAForumAPI.DAL;
//using QAForumAPI.Filters.Security;
////using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QAForumAPI.BLL.Repositories;
using QAForumAPI.BOL.Models;
using QAForumAPI.Filters.Security;


namespace QAForumAPI.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class AnswersController : BaseController
    {
        private readonly IAnswersRepository _repo;
        public AnswersController(IAnswersRepository repo)
        {
            _repo = repo;
        }

        [Authenticated]
        // Post: /answers
        [HttpPost]
        public async Task<JsonResult> PostAnswer(Answer answer)
        {
            answer.UserId = base.GetCurrentUserId();
            return await _repo.PostAnswer(answer);
        }

        [Authenticated]
        // Delete: /answers/{answerId}
        [HttpDelete("{answerId}")]
        public async Task<JsonResult> DeleteAnswer(Guid answerId)
        {
            return await _repo.DeleteAnswer(answerId);
        }
    }
}

