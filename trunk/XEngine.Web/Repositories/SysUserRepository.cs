using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using XEngine.Web.DAL;
using XEngine.Web.Models;

namespace XEngine.Web.Repositories
{
    public class SysUserRepository : ISysUserRepository
    {
        private XEngineContext context;
        public SysUserRepository(XEngineContext context)
        {
            this.context = context;
        }

        public IEnumerable<SysUser> GetUsers()
        {
            return context.SysUsers.ToList();
        }

        public SysUser GetUserByID(int userID)
        {
            return context.SysUsers.Find(userID);
        }

        public void InsertUser(SysUser user)
        {
            context.SysUsers.Add(user);
        }

        public void DeleteUser(int userID)
        {
            SysUser user = context.SysUsers.Find(userID);
            context.SysUsers.Remove(user);
        }

        public void UpdateUser(SysUser user)
        {
            context.Entry(user).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}