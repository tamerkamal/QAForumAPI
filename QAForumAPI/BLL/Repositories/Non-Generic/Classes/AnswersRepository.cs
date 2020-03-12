using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QAForumAPI.BOL.Models;
using QAForumAPI.DAL;

namespace QAForumAPI.BLL.Repositories
{

    public class AnswersRepository : IAnswersRepository
    {
        private readonly QAForumContext _context;
        private readonly IDataRepository<Answer> _repo;
        private enum VoteEnum
        {
            UpVoted = 1,
            DownVoted = -1,
            UnVoted = 0
        }
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
        public async Task<JsonResult> VoteAnswer(Guid answerId, short voteValue, Guid userId)
        {
            try
            {
                Answer answer = await _context.Answers.FindAsync(answerId);
                if (answer == default)
                {
                    throw new KeyNotFoundException();
                }
                var answerVote = _context.AnswerVotes.Where(m => m.AnswerId == answerId && m.UserId == userId)
                                         .SingleOrDefault();

                if (answerVote == default)
                {
                    CreateVote(answerId, voteValue, userId);
                }
                else
                {
                    UpdateVote(answerVote, voteValue);
                }
                string voteText = "";
                if (voteValue == (short)VoteEnum.UpVoted)
                {
                    voteText = " Up voted ";
                }
                else if (voteValue == (short)VoteEnum.DownVoted)
                {
                    voteText = " Down voted ";
                }
                else if (voteValue == (short)VoteEnum.UnVoted)
                {
                    voteText = " Unvoted ";
                }
                return new JsonResult(new { message = "Answer" + voteText + "successfully." });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void CreateVote(Guid answerId, short voteValue, Guid userId)
        {
            try
            {
                AnswerVote answerVote = new AnswerVote
                {
                    AnswerId = answerId,
                    VoteScore = voteValue,
                    UserId = userId
                };
                _context.AnswerVotes.Add(answerVote);
                _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateVote(AnswerVote answerVote, short voteValue)
        {
            try
            {
                answerVote.VoteScore = voteValue;
                _context.Entry(answerVote).State = EntityState.Modified;
                _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
