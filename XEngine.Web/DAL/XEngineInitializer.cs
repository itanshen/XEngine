using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XEngine.Web.Models;

namespace XEngine.Web.DAL
{
    public class XEngineInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<XEngineContext>
    {
        protected override void Seed(XEngineContext context)
        {
            var sysUsers = new List<SysUser> { 
                new SysUser{ID=1,UserName="ZS",CName="张三",Email="zs@xengine.com",Password="1",ModifiedDate=DateTime.Now},
                new SysUser{ID=2,UserName="LS",CName="李四",Email="ls@xengine.com",Password="1",ModifiedDate=DateTime.Now},
                new SysUser{ID=3,UserName="WW",CName="王五",Email="ww@xengine.com",Password="1",ModifiedDate=DateTime.Now}
            };
            sysUsers.ForEach(s => context.SysUsers.Add(s));
            context.SaveChanges();

            var sysRoles = new List<SysRole> { 
                new SysRole{ID=1,Name="Administrators",CName="管理员",Description="Administrators have fuu authorization to perform system administration.",ModifiedDate=DateTime.Now},
                new SysRole{ID=2,Name="General Users",CName="一般用户",Description="General Users can access the shared data they are authorized for.",ModifiedDate=DateTime.Now}
            };
            sysRoles.ForEach(r => context.SysRoles.Add(r));
            context.SaveChanges();

            var sysUserRoles = new List<SysUserRole> { 
                new SysUserRole{SysUserID=1,SysRoleID=1,ModifiedDate=DateTime.Now},
                new SysUserRole{SysUserID=1,SysRoleID=2,ModifiedDate=DateTime.Now},

                new SysUserRole{SysUserID=2,SysRoleID=1,ModifiedDate=DateTime.Now},
                new SysUserRole{SysUserID=3,SysRoleID=2,ModifiedDate=DateTime.Now}
            };
            sysUserRoles.ForEach(ur => context.SysUserRoles.Add(ur));
            context.SaveChanges();

        }
    }
}