using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public DateTime? PaidOn { get; set; }

        public bool IsFinaly { get; set; }

        public User User { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
