using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QAForumAPI.BOL.Models;

namespace QAForumAPI.BLL.Repositories
{
    public interface IAnswersRepository
    {
        Task<JsonResult> PostAnswer(
            //Guid questionId,
            Answer answer);
    }
}
