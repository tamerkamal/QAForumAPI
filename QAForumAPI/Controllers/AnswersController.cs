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
        [HttpPost]
        //[Route("​/answers​/{questionId}​/answers")]
        public async Task<JsonResult> PostAnswer(
            // [FromUri()] Guid questionId,
            Answer answer)
        {
            answer.UserId = base.GetCurrentUserId();
            return await _repo.PostAnswer(
                // questionId, 
                answer);
        }
    }
}

