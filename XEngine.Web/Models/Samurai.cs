using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XEngine.Web.Models
{
    public class Samurai
    {
        readonly IWeapon iweapon;
        public Samurai(IWeapon weapon)
        {
            this.iweapon = weapon;
        }
        public string Hit(string target)
        {
            return this.iweapon.Hit(target);
        }
    }
}