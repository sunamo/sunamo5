using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public interface IXParser
{
    void Parse(XElement node);
    string ToXml();
}