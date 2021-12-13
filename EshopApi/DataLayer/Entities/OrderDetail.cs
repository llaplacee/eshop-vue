using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        [Display(Name = "تعداد")]
        public int Count { get; set; }

        [Display(Name = "قیمت")]
        public int Price { get; set; }

        public Order Order { get; set; }

        public Product Product { get; set; }
    }
}
