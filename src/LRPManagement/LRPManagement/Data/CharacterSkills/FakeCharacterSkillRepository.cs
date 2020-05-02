using LRPManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRPManagement.Data.CharacterSkills
{
    public class FakeCharacterSkillRepository : ICharacterSkillRepository
    {
        private readonly List<CharacterSkill> _list;

        public FakeCharacterSkillRepository(List<CharacterSkill> list)
        {
            _list = list;
        }

        public void AddSkillToCharacter(int skillId, int charId)
        {
            var charSkill = new CharacterSkill
            {
                CharacterId = charId,
                SkillId = skillId
            };

            _list.Add(charSkill);
        }

        public async Task Delete(int id)
        {
            var charSkill = _list.FirstOrDefault(c => c.Id == id);
            await Task.FromResult(_list.Remove(charSkill));
        }

        public async Task<CharacterSkill> Get(int id)
        {
            return await Task.FromResult(_list.FirstOrDefault(c => c.Id == id));
        }

        public async Task<List<CharacterSkill>> Get()
        {
            return await Task.FromResult(_list.ToList());
        }

        public async Task<CharacterSkill> GetMatch(int charId, int skillId)
        {
            return await Task.FromResult
                (_list.Where(c => c.SkillId == skillId).FirstOrDefault(c => c.CharacterId == charId));
        }

        public Task Save()
        {
            return Task.CompletedTask;
        }
    }
}