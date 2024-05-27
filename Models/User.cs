using System.ComponentModel.DataAnnotations;

namespace MVCKhumaloCraftFinal2.Models
{
    public class User
    {
        [Key]
        public int userID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

    }
}
