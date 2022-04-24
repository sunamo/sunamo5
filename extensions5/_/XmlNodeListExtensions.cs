using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

public static class XmlNodeListExtensions
{
    #region For easy copy from XmlNodeListExtensions.cs
    public static XmlNode First(this XmlNodeList e, string n)
    {
        foreach (XmlNode item in e)
        {
            if (item.Name == n)
            {
                return item;
            }
        }
        return null;
    }

    public static List<XmlNode> WithName(this XmlNodeList e, string n)
    {
        List<XmlNode> result = new List<XmlNode>();
        foreach (XmlNode item in e)
        {
            if (item.Name == n)
            {
                result.Add(item);
            }
        }
        return result;
    } 
    #endregion

    //
}