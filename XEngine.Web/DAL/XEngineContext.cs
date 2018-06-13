using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using XEngine.Web.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace XEngine.Web.DAL
{
    public class XEngineContext : DbContext
    {
        public XEngineContext()
            : base("XEngineContext")
        { }

        public DbSet<SysUser> SysUsers { get; set; }
        public DbSet<SysRole> SysRoles { get; set; }
        public DbSet<SysUserRole> SysUserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // 禁用默认表名复数形式
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}