using System.ComponentModel.DataAnnotations;

namespace MVCKhumaloCraftFinal2.Models
{
    public class Order
    {
        [Key]
        public int orderID { get; set; }

        public int userID { get; set; }

        public User User { get; set; }

        public int productID { get; set; }

        public Product Product { get; set; }

        public string Status { get; set; }

        public DateTime OrderDate { get; set; }

        public string shippingMethod { get; set; }

        public double shippingAmount { get; set; }

        public string shippingCurrency { get; set; }

        public string shippingAddress { get; set; }
    }
}
