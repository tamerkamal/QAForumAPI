using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QAForumAPI.BOL
{
    interface IAuditableEntity
    {
         DateTime CreatedOn { get; set; }          
         Guid CreatedBy { get; set; }        
         DateTime UpdatedOn { get; set; }        
         Guid UpdatedBy { get; set; }
    }
}
