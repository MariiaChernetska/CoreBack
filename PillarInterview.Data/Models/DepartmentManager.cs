namespace PillarInterview.Data.Models
{
    public class DepartmentManager
    {
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}