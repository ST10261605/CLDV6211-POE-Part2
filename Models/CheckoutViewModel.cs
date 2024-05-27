using System.ComponentModel.DataAnnotations;
using MVCKhumaloCraftFinal2.Models;

namespace MVCKhumaloCraftFinal2.ViewModels
{
    public class CheckoutViewModel
    {
        public IEnumerable<CartItem> CartItems { get; set; }

        [Required]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [Required]
        [Display(Name = "Preferred Shipping Method")]
        public string shippingMethod { get; set; }

        [Required]
        [Display(Name = "User Currency")]
        public string shippingCurrency { get; set; }

        [Required]
        [Display(Name = "Shipping Address")]
        public string shippingAddress { get; set; }
    }
}
