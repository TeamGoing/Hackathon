using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Xsl;
using System.Xml;

namespace HackVTProject.Models
{
    public class ReadSkiInfo
    {
        XmlDocument skiInfoXML = new XmlDocument();
        skiInfoXML.Load("txt");
    }
}