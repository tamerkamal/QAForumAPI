using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QAForumAPI.BOL.Models
{
    public abstract class AuditableEntity: IAuditableEntity
    {
        [ScaffoldColumn(false)]
        public DateTime CreatedOn { get; set; }
       
        [ScaffoldColumn(false)]
        public Guid CreatedBy { get; set; }

        [ScaffoldColumn(false)]
        public DateTime UpdatedOn { get; set; }
        
        [ScaffoldColumn(false)]
        public Guid UpdatedBy { get; set; }
    }
}
