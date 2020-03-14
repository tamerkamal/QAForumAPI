using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QAForumAPI.BOL.Models;

namespace QAForumAPI.DAL.Repositories
{
    public interface IAnswerVotesRepository
    {
        Task<Vote> VoteAnswer(Guid answerId, string voteType, Guid userId);
    }
}
