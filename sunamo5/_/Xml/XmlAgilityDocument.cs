using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using sunamo.Html;
using sunamo.Xml;

public class XmlAgilityDocument
{
    public HtmlDocument hd = null;
    public string path = null;

    public void Load(string file)
    {
        path = file;
        hd = HtmlAgilityHelper.CreateHtmlDocument();
        var c = TF.ReadFile(file);
        c = XH.RemoveXmlDeclaration(c);
        hd.LoadHtml(c);
    }

    public void Save()
    {
        TF.SaveFile(XmlTemplates.xml + Consts.nl2 + hd.DocumentNode.OuterHtml, path);
    }
}

