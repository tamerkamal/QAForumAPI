using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QAForumAPI.BOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QAForumAPI.DAL.Repositories
{
    public class AnswerVotesRepository : IAnswerVotesRepository
    {
        private readonly QAForumContext _context;
        private enum VoteEnum
        {
            UpVoted = 1,
            DownVoted = -1,
            UnVoted = 0
        }
        public AnswerVotesRepository(QAForumContext context)
        {
            _context = context;
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
                Vote vote = _context.Votes.Where(m => m.AnswerId == answerId && m.UserId == userId)
                                         .SingleOrDefault();

                if (vote == default)
                {
                    await CreateVote(answerId, voteValue, userId);
                }
                else
                {
                    await UpdateVote(vote, voteValue);
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
                Vote vote = new Vote
                {
                    AnswerId = answerId,
                    VoteValue = voteValue,
                    UserId = userId
                };
                _context.Votes.Add(vote);
                await _context.SaveChangesAsync();
                return "vote created";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> UpdateVote(Vote answerVote, short voteValue)
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
                IQueryable<Vote> votes = _context.Votes.Where(m => m.AnswerId == answerId);
                if (votes != default)
                {
                    int answerVoteScore = votes.Sum(m => m.VoteValue);
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
}
