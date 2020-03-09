﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QAForumAPI.BOL.Models
{
    public class AnswerVote
    {
        [Key]
        public Guid VoteId { get; set; }             
        //[Required]
        //public Guid QuestionId { get; set; }       
        [Required]
        public short VoteScore { get; set; }
        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        [Required]
        [ForeignKey("Answer")]
        public Guid AnswerId { get; set; }
        public Answer Answer { get; set; }
    }
}
