using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QAForumAPI.BOL.Models;

namespace QAForumAPI.BLL.Repositories
{
    public interface IAnswersRepository
    {
        Task<JsonResult> PostAnswer(Answer answer);
        Task<JsonResult> DeleteAnswer(Guid answerId);
        Task<JsonResult> VoteAnswer(Guid answerId, short voteValue, Guid userId);
    }
}
