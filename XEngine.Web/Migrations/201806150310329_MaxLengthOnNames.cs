namespace XEngine.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MaxLengthOnNames : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.SysUser", name: "LoginName", newName: "UserName");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.SysUser", name: "UserName", newName: "LoginName");
        }
    }
}
