using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace XEngine.Web.Models
{
    [XmlRoot("MvcSiteMaps")]
    public class MvcSiteMaps
    {
        [XmlElement("MvcSiteMap")]
        public MvcSiteMap[] Items { get; set; }

    }

    public class MvcSiteMap
    {
        [XmlAttribute(AttributeName = "ID")]
        public int ID { get; set; }
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "Url")]
        public string Url { get; set; }
        [XmlAttribute(AttributeName = "ParentID")]
        public int ParentID { get; set; }
        public MvcSiteMap Parent { get; set; }
    }

}