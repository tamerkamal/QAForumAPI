﻿using Microsoft.AspNetCore.Mvc;
using QAForumAPI.BOL.Models;
using QAForumAPI.BOL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QAForumAPI.BLL.Repositories
{
   public interface IUserRepository
    {
        Task<JsonResult> Login(LoginViewModel loginViewModel);      
        Guid GetCurrentUserId(LoginViewModel loginViewModel);
    }
}
