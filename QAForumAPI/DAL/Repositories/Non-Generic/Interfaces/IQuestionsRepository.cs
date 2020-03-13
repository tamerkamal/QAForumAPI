using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QAForumAPI.BOL.Models;

namespace QAForumAPI.DAL.Repositories
{
    public interface IQuestionsRepository
    {
        Task<Question> PostQuestion(Question question, Guid currentUserId);
        IEnumerable<Question> GetQuestions();
        Task<Question> GetQuestion(Guid questionId);
        Task<JsonResult> DeleteQuestion(Guid questionId);
    }
}
