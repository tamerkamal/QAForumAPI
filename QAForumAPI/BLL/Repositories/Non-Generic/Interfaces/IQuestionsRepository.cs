using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QAForumAPI.BOL.Models;

namespace QAForumAPI.BLL.Repositories
{
    public interface IQuestionsRepository
    {
        Task<Question> PostQuestion(Question question, Guid currentUserId);
        IEnumerable<Question> GetQuestions();
        Task<Question> GetQuestion(Guid questionId);
    }
}
