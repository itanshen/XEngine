namespace XEngine.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSysMenuClass2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SysMenu",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ParentId = c.Int(),
                        Name = c.String(maxLength: 50),
                        Action = c.String(),
                        Controller = c.String(),
                        IconImage = c.String(),
                        MenuType = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SysMenu");
        }
    }
}
