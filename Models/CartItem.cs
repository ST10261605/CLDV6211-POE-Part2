using MVCKhumaloCraftFinal2.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCKhumaloCraftFinal2.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemID { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

    }
}
