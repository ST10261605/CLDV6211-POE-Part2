using System.ComponentModel.DataAnnotations;

namespace MVCKhumaloCraftFinal2.Models
{
    public class Admin
    {
        [Key]
        public int adminID { get; set; }

        public string adminEmail { get; set; }

        public string adminPassword { get; set; }
    }
}
