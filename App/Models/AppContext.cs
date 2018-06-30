using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class AppContext: DbContext
    {
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Protocol> Protocols { get; set; }
        public DbSet<Responsible> Responsibles { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }

    }
}