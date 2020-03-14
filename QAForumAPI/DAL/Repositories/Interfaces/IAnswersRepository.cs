using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QAForumAPI.BOL.Models;

namespace QAForumAPI.DAL.Repositories
{
    public interface IAnswersRepository
    {
        Task<Answer> PostAnswer(Answer answer);
        Task<JsonResult> DeleteAnswer(Guid answerId);
    }
}
