using System.ComponentModel.DataAnnotations;

namespace Roy.Enterprise.API.Entities
{
    public class UserAttendance
    {
        [Key]
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string LocationStart { get; set; }
        public string LocationEnd { get; set; }
        public DateTime? LoginDateTime { get; set; }
        public DateTime? LogoutDateTime { get; set; }
    }
}
