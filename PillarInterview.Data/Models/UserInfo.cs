namespace PillarInterview.Data.Models
{
    public class UserInfo
    {
        public string UserId { get; set; }

        public virtual User User { get; set; }
        
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}