using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

public class XHDuo
{
    public static string FormatXml(string xml, string path = ConstsDuo.se)
    {
        string result = "";

        MemoryStream mStream = new MemoryStream();
        XmlTextWriter writer = new XmlTextWriter(mStream, Encoding.Unicode);

        //XmlNamespacesHolder h = new XmlNamespacesHolder();

        XmlDocument document = null;
        //document = h.ParseAndRemoveNamespacesXmlDocument(xml);

        document = new XmlDocument();
        

        try
        {
            document.LoadXml(xml);

            writer.Formatting = Formatting.Indented;

            // Write the XML into a formatting XmlTextWriter
            document.WriteContentTo(writer);
            writer.Flush();
            mStream.Flush();

            // Have to rewind the MemoryStream in order to read
            // its contents.
            mStream.Position = 0;

            // Read MemoryStream contents into a StreamReader.
            StreamReader sReader = new StreamReader(mStream);

            // Extract the text from the StreamReader.
            string formattedXml = sReader.ReadToEnd();

            result = formattedXml;
        }
        catch (XmlException ex)
        {
            var nl = Environment.NewLine;



            return ConstsDuo.Exception + path + nl+nl + ex.Message;
            //ThrowExceptions.CustomWithStackTrace(ex);
        }

        mStream.Close();
        // 'Cannot access a closed Stream.'
        //writer.Close();

        return result;
    }
}