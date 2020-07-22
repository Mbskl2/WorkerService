using System.Collections.Generic;

namespace Worker.DAL.Models
{
    public class Skill
    {
        public int SkillId { get; set; }
        public string Name { get; set; }
        public IList<WorkerSkill> WorkerSkills { get; set; }
    }
}