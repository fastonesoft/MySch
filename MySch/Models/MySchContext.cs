using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MySch.Models
{
    public partial class MySchContext : DbContext
    {
        static MySchContext()
        {
            Database.SetInitializer<MySchContext>(null);
        }

        public MySchContext()
            : base("Name=SchMvcContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}