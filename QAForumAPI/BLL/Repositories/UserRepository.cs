using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QAForumAPI.BOL.Models;
using QAForumAPI.BOL.ViewModels;
using QAForumAPI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace QAForumAPI.BLL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly QAForumContext _context;
        private readonly IDataRepository<User> _repo;    
        const string userIdSession = "userIdSession";
        public UserRepository(
            IDataRepository<User> repo,
           // HttpContext httpContext,
            QAForumContext context)
        {
            _repo = repo;
            _context = context;
            //_httpContext = httpContext;
        }
        public async Task<JsonResult> Login(LoginViewModel loginViewModel)
        {
            try
            {
                if (!IsExistingUser(loginViewModel))
                {
                    await Register(loginViewModel);

                }               
                else if (!IsValidLogin(loginViewModel))
                {
                    return new JsonResult(new { result = "Wrong Username or Passord" });
                }
                return new JsonResult(new { result = "Login Succeeded" });
            }
            catch (Exception ex)
            {
                var innerEx = ex.InnerException;
                throw;
            }
        }

        public async Task<JsonResult> Register(LoginViewModel loginViewModel)
        {
            try
            {
                User user = new User()
                {
                    UserId = Guid.NewGuid(),
                    Username = loginViewModel.Username,
                    Password = loginViewModel.Password
                };
                _repo.Add(user);
                await _repo.SaveAsync(user);              
                return new JsonResult(new { status = "Registration Succeeded" });
            }
            catch(Exception ex)
            {
                var innerEx = ex.InnerException;
                throw;
            }
        }
      

        public bool IsExistingUser(LoginViewModel viewModel)
        {
            try
            {
                if (_context.Users.Where(m => m.Username == viewModel.Username).Any())
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                var innerEx = ex.InnerException;
                throw;
            }
        }

        public bool IsValidLogin(LoginViewModel loginViewModel)       
        {
            try
            {
                if (_context.Users.Where(m => m.Username == loginViewModel.Username && m.Password == loginViewModel.Password).Any())
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                var innerEx = ex.InnerException;
                throw;
            }
        }

        public Guid GetCurrentUserId(LoginViewModel loginViewModel)
        {
            try
            {
                return _context.Users.Where(m => m.Username == loginViewModel.Username).Select(m => m.UserId).Single();
            }
            catch (Exception ex)
            {
                var innerEx = ex.InnerException;
                throw;
            }
        }
    }
}
