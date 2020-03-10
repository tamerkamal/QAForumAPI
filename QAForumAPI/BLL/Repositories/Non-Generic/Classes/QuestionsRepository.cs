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
                var exMessage = ex.Message;
                throw;
            }
        }
        public IEnumerable<Question> GetQuestions()
        {
            try
            {
                return _context.Questions.ToList();
            }
            catch (Exception ex)
            {
                var exMessage = ex.Message;
                throw;
            }
        }
        public async Task<Question> GetQuestion(Guid questionId)
        {
            try
            {
                Question question = await _context.Questions.FindAsync(questionId);
                if (question == null)
                {
                    throw new KeyNotFoundException();
                }
                return question;
            }
            catch (Exception ex)
            {
                var exMessage = ex.Message;
                throw;
            }
        }
    }
}
