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
    public class UsersRepository : IUsersRepository
    {
        private readonly QAForumContext _context;
        private readonly IDataRepository<User> _repo;
        public UsersRepository(
            IDataRepository<User> repo,
            QAForumContext context)
        {
            _repo = repo;
            _context = context;
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
                throw new Exception(ex.Message);
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
            }
        }

        public Guid GetCurrentUserId(LoginViewModel loginViewModel)
        {
            try
            {
                return _context.Users
                    .Where(m => m.Username == loginViewModel.Username)
                    .Select(m => m.UserId).Single();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
