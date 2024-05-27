using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCKhumaloCraftFinal2.Models;

namespace MVCKhumaloCraftFinal2.Data
{
    public class MVCKhumaloCraftFinal2Context : DbContext
    {
        public MVCKhumaloCraftFinal2Context (DbContextOptions<MVCKhumaloCraftFinal2Context> options)
            : base(options)
        {
        }

        public DbSet<MVCKhumaloCraftFinal2.Models.User> User { get; set; } = default!;
        public DbSet<MVCKhumaloCraftFinal2.Models.Product> Product { get; set; } = default!;
        public DbSet<MVCKhumaloCraftFinal2.Models.Order> Order { get; set; } = default!;
        public DbSet<MVCKhumaloCraftFinal2.Models.Admin> Admin { get; set; } = default!;
        public DbSet<MVCKhumaloCraftFinal2.Models.CartItem> CartItem { get; set; } = default!;
        public DbSet<MVCKhumaloCraftFinal2.Models.Checkout> Checkout { get; set; } = default!;
    }
}
