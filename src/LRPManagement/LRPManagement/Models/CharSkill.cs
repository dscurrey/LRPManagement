using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRPManagement.Models
{
    public class CharSkill
    {
        public int CharId { get; set; }
        public int SkillId { get; set; }

        public virtual Character Character { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
