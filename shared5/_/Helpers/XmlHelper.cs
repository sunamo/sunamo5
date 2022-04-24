using System;
using System.Xml;
public static partial class XmlHelper
{
    /// <summary>
    /// WOrkaround for error The node to be removed is not a child of this node.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    public static XmlNode ReplaceChildNodeByOuterHtml(XmlNode from, XmlNode to)
    {
        
        var pn = from.ParentNode;
        var chn = pn.ChildNodes;

        if (chn.Contains(from))
        {
            from = from.ParentNode.ReplaceChild( to, from);
        }
        else
        {
            var toOx = to.OuterXml;
            for (int i = 0; i < chn.Count; i++)
            {
                var ox = chn[i].OuterXml;
                if (ox == toOx)
                {
                    from = pn.ReplaceChild( to, chn[i]);
                    break;
                }
            }
        }

        return from;
    }
}