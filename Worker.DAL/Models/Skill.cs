using System.Collections.Generic;
using Worker.Models;

namespace Worker.DAL.Models
{
    public class Skill : ISkill
    {
        public int SkillId { get; set; }
        public string Name { get; set; }

    }
}