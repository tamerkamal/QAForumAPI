using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QAForumAPI.BOL.Models;
using QAForumAPI.DAL;

namespace QAForumAPI.BLL.Repositories
{
    public class AnswersRepository : IAnswersRepository
    {
        private readonly QAForumContext _context;
        private readonly IDataRepository<Answer> _repo;
        public AnswersRepository(QAForumContext context, IDataRepository<Answer> repo)
        {
            _context = context;
            _repo = repo;
        }
        public async Task<JsonResult> PostAnswer(Answer answer)
        {
            try
            {
                answer.AnswerId = Guid.NewGuid();
                _repo.Add(answer);
                await _repo.SaveAsync(answer);
                return new JsonResult(new { message = "Answer sent successfully" });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<JsonResult> DeleteAnswer(Guid answerId)
        {
            try
            {
                Answer answer = await _context.Answers.FindAsync(answerId);
                if (answer == default)
                {
                    throw new KeyNotFoundException();
                }
                _repo.Delete(answer);
                await _context.SaveChangesAsync();
                return new JsonResult(new { message = "Answer deleted successfully" });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}