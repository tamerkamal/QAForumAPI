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
    #region Answers
    public partial class AnswersRepository : IAnswersRepository
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
    #endregion

    #region Answer Voting
    public partial class AnswersRepository
    {
        private enum VoteEnum
        {
            UpVoted = 1,
            DownVoted = -1,
            UnVoted = 0
        }
        public async Task<JsonResult> VoteAnswer(Guid answerId, string voteType, Guid userId)
        {
            try
            {
                short voteValue = 0;
                string voteText = "";

                if (voteType.ToLower() == "upvote" ||
                    voteType.ToLower() == "up-vote")
                {
                    voteValue = (short)VoteEnum.UpVoted;
                    voteText = " Up voted ";
                }
                else if (voteType.ToLower() == "downvote" ||
                         voteType.ToLower() == "down-vote")
                {
                    voteValue = (short)VoteEnum.DownVoted;
                    voteText = " Down voted ";
                }
                else if (voteType.ToLower() == "unvote" ||
                         voteType.ToLower() == "un-vote")
                {
                    voteValue = (short)VoteEnum.UnVoted;
                    voteText = " Unvoted ";
                }

                Answer answer = await _context.Answers.FindAsync(answerId);
                if (answer == default)
                {
                    throw new KeyNotFoundException();
                }
                AnswerVote answerVote = _context.AnswerVotes.Where(m => m.AnswerId == answerId && m.UserId == userId)
                                         .SingleOrDefault();

                if (answerVote == default)
                {
                    await CreateVote(answerId, voteValue, userId);
                }
                else
                {
                    await UpdateVote(answerVote, voteValue);
                }
                await UpdateAnswerVoteStatus(answer);
                return new JsonResult(new { message = "Answer" + voteText + "successfully." });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> CreateVote(Guid answerId, short voteValue, Guid userId)
        {
            try
            {
                AnswerVote answerVote = new AnswerVote
                {
                    AnswerId = answerId,
                    VoteValue = voteValue,
                    UserId = userId
                };
                _context.AnswerVotes.Add(answerVote);
                await _context.SaveChangesAsync();
                return "vote created";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> UpdateVote(AnswerVote answerVote, short voteValue)
        {
            try
            {
                answerVote.VoteValue = voteValue;
                _context.Entry(answerVote).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return "vote updated";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> UpdateAnswerVoteStatus(Answer answer)
        {
            try
            {
                int score = GetAnswerVoteScore(answer.AnswerId);
                if (score >= (int)VoteEnum.UpVoted)
                {
                    answer.VoteStatus = "UpVoted";
                }
                else if (score <= (int)VoteEnum.DownVoted)
                {
                    answer.VoteStatus = "DownVoted";
                }
                else if (score == (int)VoteEnum.UnVoted)
                {
                    answer.VoteStatus = "UnVoted";
                }
                _context.Entry(answer).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return "Answer vote status updated";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int GetAnswerVoteScore(Guid answerId)
        {
            try
            {
                IQueryable<AnswerVote> answerVotes = _context.AnswerVotes.Where(m => m.AnswerId == answerId);
                if (answerVotes != default)
                {
                    int answerVoteScore = answerVotes.Sum(m => m.VoteValue);
                    return answerVoteScore;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
    #endregion
}
