using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Display(Name = "نام محصول")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ProductName { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(2500, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }

        [Display(Name = "نام تصویر")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ProductImageName { get; set; }

        [Display(Name = "قیمت")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Price { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
