using Microsoft.EntityFrameworkCore;
using QAForumAPI.BOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QAForumAPI.DAL
{
    public class QAForumContext:DbContext
    {
        public QAForumContext(DbContextOptions<QAForumContext> options) 
            : base(options) 
        { }        
             public DbSet<User> Users { get; set; }
             public DbSet<Question> Questions { get; set; }
             public DbSet<Answer> Answers { get; set; }
             public DbSet<AnswerVote> AnswerVotes { get; set; }    
    }
}
