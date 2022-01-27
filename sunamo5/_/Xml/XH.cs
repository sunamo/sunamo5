using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace sunamo.Xml
{
    /// <summary>
    /// XH = XmlElement
    /// XHelper = XElement
    /// </summary>
    public partial class XH
    {
        public static void RemoveFirstElement(string xml, string elem)
        {
            var xd = XDocument.Parse(xml);
            //xd.Descendants("")
        }

        public static void AddXmlns(string csproj, XNamespace ns, bool add)
    {
        if (add)
            {
                XDocument xml = XDocument.Load(csproj);
                AddNs(ns, xml);

                xml.Save(csproj);
            }
            else
            {
                var text = TF.ReadAllText(csproj);
                text = RemoveNs(ns, text);
                TF.WriteAllText(text, csproj);
            }
        }

        private static void AddNs(XNamespace ns, XDocument xml)
        {
            foreach (var element in xml.Descendants().ToList())
            {
                element.Name = ns + element.Name.LocalName;
            }
            xml.Root.SetAttributeValue(Consts.xmlns, ns.ToString());
        }

        private static string RemoveNs(XNamespace ns, string text)
        {
            var xmlns = "xmlns=\"" + ns.ToString() + "\"";
            text = SH.ReplaceOnce(text, xmlns, string.Empty);
            return text;
        }

        public static string AddXmlnsContent(string content, XNamespace ns, bool add)
    {
        if (add)
        {
            XDocument xml = XDocument.Parse(content);
            AddNs(ns, xml);
            return XH.OuterXml(xml); 
        }
        else
        {
            return RemoveNs(ns, content);
        }
    }

        private static string OuterXml(XDocument xml)
        {
            StringBuilder sb = new StringBuilder();
                
                XmlWriter xml2 = XmlTextWriter.Create(sb);
                xml.Document.WriteTo(xml2);
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        public static string InnerXml(string xml)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xml);
            return xdoc.DocumentElement.InnerXml;
        }

        /// <summary>
        /// 
        /// </summary>
        public static string ReplaceSpecialHtmlEntity(string vstup)
        {
            vstup = vstup.Replace("&rsquo;", "'");//

            vstup = vstup.Replace("&lsquo;", "'"); //¢
            return vstup;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        public static string ReplaceAmpInString(string xml)
        {
            Regex badAmpersand = new Regex("&(?![a-zA-Z]{2,6};|#[0-9]{2,4};)");
            const string goodAmpersand = "&amp;";
            return badAmpersand.Replace(xml, goodAmpersand);
        }

        /// <summary>
        /// Do A2 se vklzda jiz hotove xml, nikoliv soubor.
        /// G posledni dite, to znamena ze pri parsovani celeho dokumentu vraci root.
        /// </summary>
        /// <param name="soubor"></param>
        public static XmlNode ReturnXmlRoot(string xml)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xml);
            return (XmlNode)xdoc.LastChild;
        }

        /// <summary>
        /// Vraci FirstChild, pri parsaci celeho dokumentu tak vraci xml deklaraci.
        /// A2 should be entered otherwise can occur error "different XmlDocument context"
        /// </summary>
        /// <param name="soubor"></param>
        /// <param name="xnm"></param>
        public static XmlNode ReturnXmlNode(string xml, XmlDocument xdoc2 = null)
        {
            XmlDocument xdoc = null;
            //XmlTextReader xtr = new XmlTextReader(
            if (xdoc == null)
            {
                xdoc = new XmlDocument();
            }

            xdoc.PreserveWhitespace = true;
            xdoc.LoadXml(xml);

            //xdoc.Load(soubor);
            return (XmlNode)xdoc.FirstChild;
        }

        static Type type = typeof(XH);

        /// <summary>
        /// Remove illegal XML characters from a string.
        /// </summary>
        public static string SanitizeXmlString(string xml)
        {
            if (xml == null)
            {
                ThrowExceptions.IsNull(Exc.GetStackTrace(), type, Exc.CallingMethod(),sess.i18n(XlfKeys.AtributteXmlIsNull));
            }
            //xml = xml.Replace("&", " and ");
            StringBuilder buffer = new StringBuilder(xml.Length);

            foreach (char c in xml)
            {
                if (IsLegalXmlChar(c))
                {
                    buffer.Append(c);
                }
            }

            return buffer.ToString();
        }

        public static XmlDocument xd = new XmlDocument();



        /// <summary>
        /// Whether a given character is allowed by XML 1.0.
        /// </summary>
        static bool IsLegalXmlChar(int character)
        {
            return
            (
                 character == 0x9 /* == '\t' == 9   */          ||
                 character == 0xA /* == '\n' == 10  */          ||
                 character == 0xD /* == '\r' == 13  */          ||
                (character >= 0x20 && character <= 0xD7FF) ||
                (character >= 0xE000 && character <= 0xFFFD) ||
                (character >= 0x10000 && character <= 0x10FFFF)
            );
        }

        /// <summary>
        /// A1 can be XML or path
        /// </summary>
        /// <param name="xml"></param>
        public static XmlDocument LoadXml(string xml)
        {
            if (FS.ExistsFile(xml))
            {
                xml = TF.ReadFile(xml);
            }

            XmlDocument xd = new XmlDocument();
            try
            {
                xd.LoadXml(xml);
            }
            catch (Exception ex)
            {
                ThrowExceptions.CustomWithStackTrace(ex);
                return null;
            }
            return xd;
        }

        public static string RemoveXmlDeclaration(string vstup)
        {
            vstup = Regex.Replace(vstup, @"<\?xml.*?\?>", "");
            vstup = Regex.Replace(vstup, @"<\?xml.*?\>", "");
            vstup = Regex.Replace(vstup, @"<\?xml.*?\/>", "");
            return vstup;
        }

        
    }
}