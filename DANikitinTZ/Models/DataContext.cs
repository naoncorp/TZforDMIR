using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DANikitinTZ.Models
{
    public class DataContext : DbContext
    {
        //public DbSet<Bid> Bids { get; set; }
        public DbSet<Person> People { get; set; }
    }
}