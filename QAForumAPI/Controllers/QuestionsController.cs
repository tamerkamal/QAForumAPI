using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QAForumAPI.DAL.Repositories;
using QAForumAPI.BOL.Models;
using QAForumAPI.Filters.Security;

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

        [Authenticated]
        [HttpPost]
        // Post: /questions
        public async Task<Question> PostQuestion(Question question)
        {
            return await _repo.PostQuestion(question, base.GetCurrentUserId());
        }

        // Get: /questions
        [HttpGet]
        public IEnumerable<Question> GetQuestions()
        {
            return _repo.GetQuestions();
        }

        // Get: /questions/{questionId}
        [HttpGet("{questionId}")]
        public async Task<Question> GetQuestion(Guid questionId)
        {
            return await _repo.GetQuestion(questionId);
        }

        [Authenticated]
        // Delete: /questions/questionId
        [HttpDelete("{questionId}")]
        public async Task<JsonResult> DeleteQuestion(Guid questionId)
        {
            return await _repo.DeleteQuestion(questionId);
        }
    }
}
