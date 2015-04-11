namespace DnsTestApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dsdsd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Nodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Parent = c.Int(nullable: false),
                        Folder = c.Boolean(nullable: false),
                        Sort = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Nodes");
        }
    }
}
