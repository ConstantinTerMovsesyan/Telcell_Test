using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace PasteWebApiApp.DataAccess
{
    public class TelcellDbContext : DbContext
    {
        public DbSet<Paste> Pastes { get; set; }


        public TelcellDbContext()
            :
            base("TelcellDb")
        {
        }

        public TelcellDbContext(string connectionString)
            :
            base(connectionString)
        {
        }
    }
}