namespace XEngine.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SysRole",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CName = c.String(),
                        Description = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                        SysUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SysUser", t => t.SysUser_ID);
            
            CreateTable(
                "dbo.SysUserRole",
                c => new
                    {
                        SysUserID = c.Int(nullable: false),
                        SysRoleID = c.Int(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.SysUserID, t.SysRoleID })
                .ForeignKey("dbo.SysRole", t => t.SysRoleID, cascadeDelete: true)
                .ForeignKey("dbo.SysUser", t => t.SysUserID, cascadeDelete: true);
            
            CreateTable(
                "dbo.SysUser",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Email = c.String(),
                        Password = c.String(),
                        CName = c.String(),
                        Description = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SysUserRole", "SysUserID", "dbo.SysUser");
            DropForeignKey("dbo.SysRole", "SysUser_ID", "dbo.SysUser");
            DropForeignKey("dbo.SysUserRole", "SysRoleID", "dbo.SysRole");
            DropTable("dbo.SysUser");
            DropTable("dbo.SysUserRole");
            DropTable("dbo.SysRole");
        }
    }
}
