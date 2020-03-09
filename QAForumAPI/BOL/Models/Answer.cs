using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QAForumAPI.BOL.Models
{
    public class Answer
    {
        [Key]
        public Guid AnswerId { get; set; }       
        [Required]      
        public string AnswerText { get; set; }
        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        [Required]
        [ForeignKey("Question")]
        public Guid QuestionId { get; set; }      
        public Question Question { get; set; }
    }
}
