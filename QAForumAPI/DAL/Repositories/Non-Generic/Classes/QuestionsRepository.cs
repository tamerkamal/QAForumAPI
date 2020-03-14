using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QAForumAPI.BOL.Models;
using QAForumAPI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QAForumAPI.DAL.Repositories
{
    public class QuestionsRepository : IQuestionsRepository
    {
        private readonly QAForumContext _context;
        private readonly DbSet<Question> _dbset;

        public QuestionsRepository(QAForumContext context)
        {
            _context = context;
            _dbset = _context.Set<Question>();
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
                _dbset.Add(question);
                await _context.SaveChangesAsync();
                return question;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<Question> GetQuestions()
        {
            try
            {
                return _dbset.Include(m => m.Answers).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Question> GetQuestion(Guid questionId)
        {
            try
            {
                Question question = await _dbset.FindAsync(questionId);
                if (question == null)
                {
                    throw new KeyNotFoundException();
                }
                return question;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<JsonResult> DeleteQuestion(Guid questionId)
        {
            try
            {
                Question question = await _dbset.FindAsync(questionId);
                if (question == default)
                {
                    throw new KeyNotFoundException();
                }
                _dbset.Remove(question);
                await _context.SaveChangesAsync();
                return new JsonResult(new { message = "Question deleted successfully" });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
