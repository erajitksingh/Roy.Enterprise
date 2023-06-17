using System.ComponentModel.DataAnnotations;

namespace Roy.Enterprise.API.Entities
{
    public class Order
    {
        [Key] 
        public Guid Id { get; set; }
        public string OrderId { get; set; }
        public string Notes { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public int Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string DeliveredLocationStart { get; set; }
        public string DeliveredLocationEnd { get; set; }
    }

    public class OrderItem
    {
        [Key]
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public Item Items { get; set; }
    }
}