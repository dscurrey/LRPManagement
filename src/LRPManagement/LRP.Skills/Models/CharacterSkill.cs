namespace LRP.Skills.Models
{
    public class CharacterSkill
    {
        public int Id { get; set; }
        public int CharacterId { get; set; }
        public int SkillId { get; set; }

        public virtual Skill Skill { get; set; }
    }
}
