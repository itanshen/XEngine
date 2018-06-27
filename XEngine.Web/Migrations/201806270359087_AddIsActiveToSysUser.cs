namespace XEngine.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsActiveToSysUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SysRole", "SysUser_ID", "dbo.SysUser");
            DropIndex("dbo.SysRole", new[] { "SysUser_ID" });
            AddColumn("dbo.SysUser", "IsActive", c => c.String());
            DropColumn("dbo.SysRole", "SysUser_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SysRole", "SysUser_ID", c => c.Int());
            DropColumn("dbo.SysUser", "IsActive");
            CreateIndex("dbo.SysRole", "SysUser_ID");
            AddForeignKey("dbo.SysRole", "SysUser_ID", "dbo.SysUser", "ID");
        }
    }
}
