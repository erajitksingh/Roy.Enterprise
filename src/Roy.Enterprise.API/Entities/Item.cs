using System.ComponentModel.DataAnnotations;

namespace Roy.Enterprise.API.Entities
{
    public class Item
    {
        [Key]
        public Guid Id { get; set; }
        public byte[] ProductLogo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
