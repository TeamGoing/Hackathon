using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;


namespace HackVTProject.Models
{
    public class Default1Controller : Controller
    {
        //
        // GET: /Default1/

        public ActionResult Index()
        {
            return View();
        }
        /*
         * System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader("c:\\IntroToVCS.xml");
         string contents = "";
         while (reader.Read()) 
         {
            reader.MoveToContent();
            if (reader.NodeType == System.Xml.XmlNodeType.Element)
               contents += "<"+reader.Name + ">\n";
            if (reader.NodeType == System.Xml.XmlNodeType.Text)
               contents += reader.Value + "\n";
         }
Console.Write(contents);
         * */
        public string loadXMLFile(string fileName)
        {
            XmlTextReader skiReader = new XmlTextReader(fileName);
            string contents = "";

            while (skiReader.Read())
            {
                skiReader.MoveToContent();
                if (skiReader.NodeType == XmlNodeType.Element)
                {
                    contents += "<" + skiReader.Name + ">\n";
                }
                if (skiReader.NodeType == System.Xml.XmlNodeType.Text)
                    contents += skiReader.Value + "\n";
            }
            return contents;
        }

    }
}
