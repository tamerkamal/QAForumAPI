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
    public class AnswerVotesController : BaseController
    {
        private readonly IAnswerVotesRepository _repo;
        public AnswerVotesController(IAnswerVotesRepository repo)
        {
            _repo = repo;
        }

        [Authenticated]
        // HttpPut: /answerVotes/{answerId}/{voteType}
        [HttpPut("{answerId}/{voteType}")]
        public async Task<Vote> VoteAnswer(Guid answerId, string voteType)
        {
            return await _repo.VoteAnswer(answerId, voteType, base.GetCurrentUserId());
        }
    }
}

