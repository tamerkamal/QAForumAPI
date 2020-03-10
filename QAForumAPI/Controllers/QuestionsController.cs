using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QAForumAPI.BLL.Repositories;
using QAForumAPI.BOL.Models;

namespace QAForumAPI.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class QuestionsController : BaseController
    {
        private readonly IQuestionsRepository _repo;
        public QuestionsController(IQuestionsRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<Question> PostQuestion(Question question)
        {
            if (!base.IsAuthenticated())
            {
                throw new Exception(base.NotLoggedInMessage());
            }
            return await _repo.PostQuestion(question, base.GetCurrentUserId());
        }

        [HttpGet]
        public IEnumerable<Question> GetQuestions()
        {
            return _repo.GetQuestions();
        }

        [HttpGet("{questionId}")]
        public async Task<Question> GetQuestion(Guid questionId)
        {
            return await _repo.GetQuestion(questionId);
        }
    }
}
