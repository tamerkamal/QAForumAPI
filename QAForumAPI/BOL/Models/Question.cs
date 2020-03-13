using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QAForumAPI.BOL.Models
{
    public class Question
    {
        [Key]
        public Guid QuestionId { get; set; }
        [Required]
        public string QuestionText { get; set; }
        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
