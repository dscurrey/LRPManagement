using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LRPManagement.Models
{
    public class PreReqSkill
    {
        public int SkillId { get; set; }
        public int PreReqId { get; set; }

        [ForeignKey("SkillId")]
        public virtual Skill Skill { get; set; }
        [ForeignKey("PreReqId")]
        public virtual Skill PreReq { get; set; }
    }
}
