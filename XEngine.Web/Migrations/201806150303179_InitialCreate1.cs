namespace XEngine.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SysUser", "UserName", c => c.String(maxLength: 50));
            DropColumn("dbo.SysUser", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SysUser", "Name", c => c.String(maxLength: 50));
            DropColumn("dbo.SysUser", "UserName");
        }
    }
}
