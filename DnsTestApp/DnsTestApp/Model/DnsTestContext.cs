namespace DnsTestApp.Model
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DnsTestContext : DbContext
    {
        public DnsTestContext()
            : base("name=DnsTestContext")
        {
        }

        public DbSet<Node> Nodes { get; set; }

    }
}