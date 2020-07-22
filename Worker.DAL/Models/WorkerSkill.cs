namespace Worker.DAL.Models
{
    public class WorkerSkill
    {
        public int WorkerSkillId { get; set; }
        public int WorkerProfileId { get; set; }
        public WorkerProfile WorkerProfile { get; set; }
        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}