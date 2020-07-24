using System;
using System.Collections.Generic;
using Worker.DAL.Entities;

namespace Worker.DAL
{
#pragma warning disable 8767
    class SkillEqualityComparer : IEqualityComparer<Entities.Skill>
    {
        public bool Equals(Skill x, Skill y)
        {
            return x.Name.Equals(y.Name, StringComparison.InvariantCultureIgnoreCase);
        }

        public int GetHashCode(Skill obj)
        {
            return obj.Name.ToLowerInvariant().GetHashCode();
        }
    }
}