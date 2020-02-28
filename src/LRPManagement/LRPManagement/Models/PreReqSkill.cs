using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRPManagement.Models
{
    public class PreReqSkill
    {
        public int SkillId { get; set; }
        public int PreReqId { get; set; }

        public virtual Skill Skill { get; set; }
        public virtual Skill PreReq { get; set; }
    }
}
