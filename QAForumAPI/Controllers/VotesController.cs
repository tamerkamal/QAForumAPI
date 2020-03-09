using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QAForumAPI.BLL.Repositories;
using QAForumAPI.BOL.Models;
using QAForumAPI.DAL;

namespace QAForumAPI.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class AnswerVotesController : ControllerBase
    {
        private readonly QAForumContext _context;
        private readonly DataRepository<AnswerVote> _repo;

        public AnswerVotesController(QAForumContext context, DataRepository<AnswerVote> repo)
        {
            _context = context;
            _repo = repo;
        }

        // GET: AnswerVotes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnswerVote>>> GetAnswerVotes()
        {
            return await _context.AnswerVotes.ToListAsync();
        }

        // GET: AnswerVotes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnswerVote>> GetAnswerVote([FromRoute] Guid id)
        {
            var answerVote = await _context.AnswerVotes.FindAsync(id);

            if (answerVote == null)
            {
                return NotFound();
            }

            return  Ok(answerVote);
        }

        // PUT: AnswerVotes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnswerVote([FromRoute] Guid id,[FromBody] AnswerVote answerVote)
        {
            if (id != answerVote.VoteId)
            {
                return BadRequest();
            }

            _context.Entry(answerVote).State = EntityState.Modified;

            try
            {
                _repo.Update(answerVote);
                var save = await _repo.SaveAsync(answerVote);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnswerVoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AnswerVotes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<AnswerVote>> PostAnswerVote(AnswerVote answerVote)
        {
            _repo.Add(answerVote);
           var save=  await _repo.SaveAsync(answerVote);

            return CreatedAtAction("GetAnswerVote", new { id = answerVote.VoteId }, answerVote);
        }

        // DELETE: api/AnswerVotes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AnswerVote>> DeleteAnswerVote(Guid id)
        {
            var answerVote = await _context.AnswerVotes.FindAsync(id);
            if (answerVote == null)
            {
                return NotFound();
            }
            _repo.Delete(answerVote);
        var save= await _repo.SaveAsync(answerVote);

            return answerVote;
        }

        private bool AnswerVoteExists(Guid id)
        {
            return _context.AnswerVotes.Any(e => e.VoteId == id);
        }
    }
}
