using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XEngine.Web
{
    public interface IItem<T>
    {
        IList<T> GetChildren();
    }
}