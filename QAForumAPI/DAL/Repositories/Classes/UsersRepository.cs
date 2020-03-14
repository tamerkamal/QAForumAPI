using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QAForumAPI.BOL.Models;
using QAForumAPI.BOL.ViewModels;
using QAForumAPI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace QAForumAPI.DAL.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly QAForumContext _context;
        private readonly DbSet<User> _dbset;
        public UsersRepository(QAForumContext context)
        {
            _context = context;
            _dbset = _context.Set<User>();
        }
        public async Task<JsonResult> Login(LoginViewModel loginViewModel)
        {
            try
            {
                if (!IsExistingUsername(loginViewModel))
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
                    UserId = loginViewModel.UserId,
                    Username = loginViewModel.Username,
                    Password = loginViewModel.Password
                };
                _dbset.Add(user);
                await _context.SaveChangesAsync();
                return new JsonResult(new { status = "Registration Succeeded" });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool IsExistingUsername(LoginViewModel viewModel)
        {
            try
            {
                if (_dbset.Any(m => m.Username == viewModel.Username))
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
                if (_dbset.Any(m => m.UserId == loginViewModel.UserId))
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
                return _dbset.Where(m => m.Username == loginViewModel.Username)
                             .Select(m => m.UserId)
                             .Single();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
