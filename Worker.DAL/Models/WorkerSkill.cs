namespace Worker.DAL.Models
{
    public class WorkerSkill
    {
        public int Id { get; set; }
        public WorkerProfile WorkerProfile { get; set; }
        public Skill Skill { get; set; }
    }
}