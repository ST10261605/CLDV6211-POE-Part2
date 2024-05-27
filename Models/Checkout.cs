namespace MVCKhumaloCraftFinal2.Models
{
    public class Checkout
    {
        public int checkoutID { get; set; }

        public int CartItemID { get; set; }

        public CartItem CartItem { get; set; }
    }
}
