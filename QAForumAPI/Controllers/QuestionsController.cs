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
    public class QuestionsController : BaseController
    {
        private readonly IQuestionsRepository _repo;
        public QuestionsController(IQuestionsRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        //[Route("/questions")]     
        public async Task<Question> PostQuestion(Question question)
        {
            if (!base.IsAuthenticated())
            {
                throw new Exception(base.NotLoggedInMessage());
            }
            return await _repo.PostQuestion(question, base.GetCurrentUserId());
        }

        [HttpGet]
        [Route("/")]
        [Route("/questions")]
        public IEnumerable<Question> GetQuestions()
        {
            if (!base.IsAuthenticated())
            {
                throw new Exception(base.NotLoggedInMessage());
            }
            return _repo.GetQuestions();
        }

        [HttpGet("{questionId}")]
        public async Task<Question> GetQuestion(Guid questionId)
        {
            return await _repo.GetQuestion(questionId);
        }

        //[HttpPost]
        //public async Task<Question> PostQuestion(Question question)
        //{            
        //    _repo.Add(question);
        //    await _repo.SaveAsync(question);
        //    return question;
        //}


        //// GET: Questions
        //[HttpGet]
        //[Route("/questions")]
        //public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
        //{
        //    return await _context.Questions.ToListAsync();
        //}

        //// GET: Questions/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Question>> GetQuestion([FromRoute] Guid id)
        //{
        //    var question = await _context.Questions.FindAsync(id);

        //    if (question == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(question);
        //}

        //// PUT: Questions/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutQuestion([FromRoute] Guid id, [FromBody] Question question)
        //{
        //    if (id != question.QuestionId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(question).State = EntityState.Modified;

        //    try
        //    {
        //        _repo.Update(question);
        //        var save = await _repo.SaveAsync(question);
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!QuestionExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Questions
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPost]
        //public async Task<Question> PostQuestion(Question question)
        //{            
        //    _repo.Add(question);
        //    await _repo.SaveAsync(question);
        //    return question;
        //}

        //// DELETE: api/Questions/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Question>> DeleteQuestion(Guid id)
        //{
        //    var question = await _context.Questions.FindAsync(id);
        //    if (question == null)
        //    {
        //        return NotFound();
        //    }
        //    _repo.Delete(question);
        //    var save = await _repo.SaveAsync(question);
        //    return question;
        //}

        //private bool QuestionExists(Guid id)
        //{
        //    return _context.Questions.Any(e => e.QuestionId == id);
        //}
    }
}
