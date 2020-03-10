using QAForumAPI.BOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QAForumAPI.BLL.Repositories
{
    public interface IQuestionsRepository
    {
        Task<Question> PostQuestion(Question question, Guid currentUserId);
        IEnumerable<Question> GetQuestions();
        Task<Question> GetQuestion(Guid questionId);
    }
}
