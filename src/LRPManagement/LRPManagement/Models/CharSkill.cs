using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LRPManagement.Models
{
    public class CharSkill
    {
        [ForeignKey("Character")]
        public int CharId { get; set; }
        [ForeignKey("Skill")]
        public int SkillId { get; set; }

        public virtual Character Character { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
