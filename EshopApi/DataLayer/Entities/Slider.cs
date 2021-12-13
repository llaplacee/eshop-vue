using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Entities
{
    public class Slider
    {
        [Key]
        public int SliderId { get; set; }

        [Display(Name = "عنوان")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(2000, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }

        [Display(Name = "نام تصویر")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ImageName { get; set; }
    }
}
