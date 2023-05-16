namespace OptimizationLabs.Shared.Entities
{
    public class Grade : BaseEntity
    {
        public Guid JudgeId { get; set; }
        
        public Guid CandidateId { get; set; }
        
        public int GradeValue { get; set; }
        
        public virtual Judge? Judge { get; set; }
        
        public virtual Candidate? Candidate { get; set; }
    }
}