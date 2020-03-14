using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QAForumAPI.BOL.Models;
using QAForumAPI.DAL;

namespace QAForumAPI.DAL.Repositories
{
    public partial class AnswersRepository : IAnswersRepository
    {
        private readonly QAForumContext _context;
        private readonly DbSet<Answer> _dbset;
        public AnswersRepository(QAForumContext context)
        {
            _context = context;
            _dbset = _context.Set<Answer>();
        }
        public async Task<Answer> PostAnswer(Answer answer)
        {
            try
            {
                //answer.AnswerId = Guid.NewGuid();
                _dbset.Add(answer);
                await _context.SaveChangesAsync();
                return answer;
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
                Answer answer = await _dbset.FindAsync(answerId);
                if (answer == default)
                {
                    throw new KeyNotFoundException();
                }
                _dbset.Remove(answer);
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
