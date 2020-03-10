using QAForumAPI.BOL.Models;
using QAForumAPI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QAForumAPI.BLL.Repositories
{
    public class QuestionsRepository : IQuestionsRepository
    {
        private readonly QAForumContext _context;
        private readonly IDataRepository<Question> _repo;
        public QuestionsRepository(QAForumContext context, IDataRepository<Question> repo)
        {
            _context = context;
            _repo = repo;
        }
        public async Task<Question> PostQuestion(Question question, Guid currentUserId)
        {
            try
            {
                if (question.QuestionId == default)
                {
                    question.QuestionId = Guid.NewGuid();
                }
                question.UserId = currentUserId;
                _repo.Add(question);
                await _repo.SaveAsync(question);
                return question;
            }
            catch (Exception ex)
            {
                var innerEx = ex.InnerException;
                throw;
            }
        }
        public IEnumerable<Question> GetQuestions()
        {
            return _context.Questions.ToList();
        }
        public async Task<Question> GetQuestion(Guid questionId)
        {
            return await _context.Questions.FindAsync(questionId);
        }
    }
}
