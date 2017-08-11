using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CS.Model
{
    public class BllDbContext:DbContext
    {
        public BllDbContext()
            : base("BLLCon")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }

        public BllDbContext(string connection)
            : base(connection)
        {
            this.Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<CustomerBankGuarantee> CustomerBankGuarantees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CustomerRoi> CustomerRois { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<MarketGroup> MarketGroups { get; set; } 

    }
}