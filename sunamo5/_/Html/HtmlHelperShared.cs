using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using HtmlAgilityPack;
using sunamo.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using sunamo.Html;
using System.Web;
using System.Net;

public static partial class HtmlHelper
{
    /// <summary>
    /// Problematic with auto translate
    /// </summary>
    /// <param name="vstup"></param>
    public static string ReplaceHtmlNonPairTagsWithXmlValid(string vstup)
    {
        List<string> jizNahrazeno = new List<string>();

        MatchCollection mc = Regex.Matches(vstup, RegexHelper.rNonPairXmlTagsUnvalid.ToString());
        List<string> col = new List<string>(AllLists.HtmlNonPairTags);

        foreach (Match item in mc)
        {
            string d = item.Value.Replace(" >", AllStrings.gt);
            string tag = "";
            if (item.Value.Contains(AllStrings.space))
            {
                tag = SH.GetFirstPartByLocation(item.Value, AllChars.space);
            }
            else
            {
                tag = d.Replace(AllStrings.slash, "").Replace(AllStrings.gt, "");
            }

            tag = tag.TrimStart(AllChars.lt).Trim().ToLower();
            if (col.Contains(tag))
            {
                if (!item.Value.Contains("/>"))
                {
                    if (!jizNahrazeno.Contains(item.Value))
                    {
                        jizNahrazeno.Add(item.Value);
                        string nc = item.Value.Substring(0, item.Value.Length - 1) + " />";
                        vstup = vstup.Replace(item.Value, nc);
                    }
                }
            }
        }
        return vstup;
    }

    public static string ConvertTextToHtml(string p)
    {
        p = p.Replace(Environment.NewLine, "<br />");
        p = p.Replace("\n", "<br />");
        return p;
    }

    public static string PrepareToAttribute(string title)
    {
        return title.Replace(AllChars.qm, AllChars.apostrophe);
    }

    public static string ReplaceAllFontCase(string r)
    {
        string za = "<br />";
        r = r.Replace("<BR />", za);
        r = r.Replace("<bR />", za);
        r = r.Replace("<Br />", za);

        r = r.Replace("<br/>", za);
        r = r.Replace("<BR/>", za);
        r = r.Replace("<bR/>", za);
        r = r.Replace("<Br/>", za);

        r = r.Replace("<br>", za);
        r = r.Replace("<BR>", za);
        r = r.Replace("<bR>", za);
        r = r.Replace("<Br>", za);
        return r;
    }

    public static string ClearSpaces(string dd)
    {
        return dd.Replace("&nbsp;", AllStrings.space).Replace(AllStrings.doubleSpace, AllStrings.space);
    }

    private static void RecursiveReturnTagWithAttr(List<HtmlNode> vr, HtmlNode htmlNode, string tag, string attr, string value)
    {
        foreach (HtmlNode item in htmlNode.ChildNodes)
        {
            if (item.Name == tag && HtmlHelper.GetValueOfAttribute(attr, item) == value)
            {
                //RecursiveReturnTagWithAttr(vr, item, tag, attr, value);
                vr.Add(item);
                return;
            }
            else
            {
                RecursiveReturnTagWithAttr(vr, item, tag, attr, value);
            }
        }
    }

    public static string GetValueOfAttribute(string p, HtmlNode divMain, bool _trim = false)
    {
        return HtmlAssistant.GetValueOfAttribute(p, divMain, _trim);
    }

    /// <summary>
    /// Pokud bude nalezen alespo?? jeden tag, vr??t?? ho, pokud ????dn??, GN
    /// </summary>
    /// <param name="htmlNode"></param>
    /// <param name="tag"></param>
    /// <param name="attr"></param>
    /// <param name="value"></param>
    public static HtmlNode ReturnTagWithAttr(HtmlNode htmlNode, string tag, string attr, string value)
    {
        List<HtmlNode> vr = new List<HtmlNode>();
        RecursiveReturnTagWithAttr(vr, htmlNode, tag, attr, value);
        if (vr.Count > 0)
        {
            return vr[0];
        }
        return null;
    }

    public static List<HtmlNode> GetWithoutTextNodes(HtmlNode htmlNode)
    {
        List<HtmlNode> vr = new List<HtmlNode>();
        foreach (HtmlNode item in htmlNode.ChildNodes)
        {
            string dd = item.ToString();
            if (dd != "HtmlAgilityPack.HtmlTextNode")
            {
                vr.Add(item);
            }
        }
        return vr;
    }

    public static HtmlNode GetTagOfAtributeRek(HtmlNode hn, string nameOfTag, string nameOfAtr, string valueOfAtr)
    {
        hn = HtmlHelper.TrimNode(hn);
        foreach (HtmlNode var in hn.ChildNodes)
        {
            //var.InnerHtml = var.InnerHtml.Trim();
            HtmlNode hn2 = var;//.FirstChild;
            foreach (HtmlNode item2 in var.ChildNodes)
            {
                if (HtmlHelper.GetValueOfAttribute(nameOfAtr, item2) == valueOfAtr)
                {
                    return item2;
                }
                HtmlNode hn3 = GetTagOfAtributeRek(item2, nameOfTag, nameOfAtr, valueOfAtr);
                if (hn3 != null)
                {
                    return hn3;
                }
            }
            if (hn2.Name == nameOfTag)
            {
                if (HtmlHelper.GetValueOfAttribute(nameOfAtr, hn2) == valueOfAtr)
                {
                    return hn2;
                }
                foreach (HtmlNode var2 in hn2.ChildNodes)
                {
                    if (HtmlHelper.GetValueOfAttribute(nameOfAtr, var2) == valueOfAtr)
                    {
                        return var2;
                    }
                }

                //}
            }
        }
        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="html"></param>
    /// <param name="nameOfTag"></param>
    public static string TrimOpenAndEndTags(string html, string nameOfTag)
    {
        html = html.Replace(AllStrings.lt + nameOfTag + AllStrings.gt, "");
        html = html.Replace("</" + nameOfTag + AllStrings.gt, "");
        return html;
    }

    public static List<HtmlNode> ReturnAllTagsWithAttr(HtmlNode table, string v1, string v2, string v3)
    {
        return null;
    }

    /// <summary>
    /// P??ed zavol??n??m t??to metody mus?? b??t v A1 p??evedeny b??l?? znaky na mezery - pouze tak budou ozna??eny v??echny v??skyty dan??ch slov
    /// </summary>
    /// <param name="celyObsah"></param>
    /// <param name="maxPocetPismenNaVetu"></param>
    /// <param name="pocetVet"></param>
    /// <param name="hledaneSlova"></param>
    public static string HighlightingWords(string celyObsah, int maxPocetPismenNaVetu, int pocetVet, List<string> hledaneSlova)
    {
        hledaneSlova = CA.ToLower(hledaneSlova);
        celyObsah = celyObsah.Trim();
        List<FromToWord> ftw = SH.ReturnOccurencesOfStringFromToWord(celyObsah, hledaneSlova.ToArray());
        if (ftw.Count > 0)
        {
            List<List<FromToWord>> dd = new List<List<FromToWord>>();
            List<FromToWord> fromtw = new List<FromToWord>();
            fromtw.Add(ftw[0]);
            int indexDDNaKteryVkladat = 0;
            int indexFromNaposledyVlozeneho = ftw[0].from;
            dd.Add(fromtw);

            for (int i = 1; i < ftw.Count; i++)
            {
                var item = ftw[i];
                if (item.to - indexFromNaposledyVlozeneho < maxPocetPismenNaVetu)
                {
                    dd[indexDDNaKteryVkladat].Add(item);
                }
                else
                {
                    List<FromToWord> ftw2 = new List<FromToWord>();
                    ftw2.Add(item);
                    dd.Add(ftw2);
                    if (dd.Count == pocetVet)
                    {
                        break;
                    }
                    indexDDNaKteryVkladat++;
                }
                indexFromNaposledyVlozeneho = item.from;
            }

            // Te?? vypo??tu pro ka??dou kolekci v DD prost??edn?? prvek a od toho vezmu v??dy znaky nalevo i napravo
            StringBuilder final = new StringBuilder();
            foreach (var item in dd)
            {
                int stred = 0;
                if (item.Count % 2 == 0)
                {
                    // Zjist??m 2 prost??edn?? slova
                    int from = item[item.Count / 2].from;
                    int to = 0;
                    if (item.Count != 2)
                    {
                        to = item[item.Count / 2 + 1].to;
                    }
                    else
                    {
                        to = item[item.Count / 2].to;
                    }

                    stred = (from + (to - from) / 2);
                }
                else if (item.Count == 1)
                {
                    stred = (item[0].from + (item[0].to - item[0].from) / 2);
                }
                else
                {
                    stred = item.Count / 2;
                    stred++;
                    stred = (item[stred].from + (item[stred].to - item[stred].from) / 2);
                }

                int naKazdeStrane = maxPocetPismenNaVetu / 2;

                string veta = SH.XCharsBeforeAndAfterWholeWords(SH.ReplaceAll(celyObsah, AllStrings.space, CA.ToListString(AllChars.whiteSpacesChars).ToArray()), stred, naKazdeStrane);

                // Te?? zv??razn??m nalezen?? slova
                List<string> slova = SH.SplitBySpaceAndPunctuationCharsLeave(veta);
                StringBuilder vetaSeZvyraznenimiCastmi = new StringBuilder();
                foreach (var item2 in slova)
                {
                    bool jeToHledaneSlovo = false;
                    string i2l = item2.ToLower();
                    foreach (var item3 in hledaneSlova)
                    {
                        if (i2l == item3)
                        {
                            jeToHledaneSlovo = true;
                        }
                    }

                    if (jeToHledaneSlovo)
                    {
                        vetaSeZvyraznenimiCastmi.Append("<b>" + item2 + "</b>");
                    }
                    else
                    {
                        vetaSeZvyraznenimiCastmi.Append(item2);
                    }
                }
                final.Append(vetaSeZvyraznenimiCastmi.ToString() + " ... ");
            }
            return final.ToString();
        }
        else
        {
            return SH.ShortForLettersCountThreeDotsReverse(celyObsah, pocetVet * maxPocetPismenNaVetu);
        }
    }

    public static string ConvertHtmlToText(string h)
    {
        h = WebUtility.HtmlDecode(h);
        h = SH.ReplaceAll(h, Environment.NewLine, "<br>", "<br />", "<br/>");
        h = StripAllTags(h);
        return h;
    }

    /// <summary>
    /// Nahrad?? ka??d?? text <*> za SE. Vnit??n?? ne-xml obsah nech?? b??t.
    /// </summary>
    /// <param name="p"></param>
    public static string StripAllTags(string p)
    {
        return StripAllTags(p, AllStrings.doubleSpace);
    }
    public static string StripAllTags(string p, string replaceFor)
    {
        string vr = Regex.Replace(p, @"<[^>]*>", replaceFor);
        vr = SH.ReplaceAllDoubleSpaceToSingle(vr);
        return vr;
    }

    public static HtmlNode TrimNode(HtmlNode hn2)
    {
        if (hn2.FirstChild == null)
        {
            return hn2;
        }
        if (string.IsNullOrWhiteSpace(hn2.FirstChild.InnerHtml))
        {
            return hn2;
        }
        hn2.InnerHtml = hn2.InnerHtml.Trim();
        hn2.FirstChild.InnerHtml = hn2.FirstChild.InnerHtml.Trim();
        hn2.InnerHtml = hn2.InnerHtml.Trim();
        return hn2;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="node"></param>
    /// <param name="atr"></param>
    /// <param name="hod"></param>
    public static void SetAttribute(HtmlNode node, string atr, string hod)
    {
        HtmlAssistant.SetAttribute(node, atr, hod);
    }

    /// <summary>
    /// Vratilo 15 namisto 10
    /// Pou????v?? metodu RecursiveReturnTags
    /// Do A2 se m????e vlo??it *
    /// </summary>
    /// <param name="htmlNode"></param>
    /// <param name="tag"></param>
    public static List<HtmlNode> ReturnTagsRek(HtmlNode htmlNode, string tag)
    {
        List<HtmlNode> vr = new List<HtmlNode>();
        RecursiveReturnTags(vr, htmlNode, tag);
        vr = TrimTexts(vr);
        return vr;
    }

    /// <summary>
    /// G null kdy?? tag nebude nalezen
    /// </summary>
    /// <param name="htmlNode"></param>
    /// <param name="tag"></param>
    /// <param name="attr"></param>
    /// <param name="value"></param>
    public static HtmlNode ReturnTagWithAttrRek(HtmlNode htmlNode, string tag, string attr, string value)
    {
        return ReturnTagWithAttr(htmlNode, tag, attr, value);
    }

    /// <summary>
    /// Do A2 se m????e zadat * pro z??skan?? v??ech tag??
    /// Do A4 se m????e vlo??it * pro vr??cen?? tag?? s hledan??m atributem s jakoukoliv hodnotou
    /// </summary>
    /// <param name="htmlNode"></param>
    /// <param name="tag"></param>
    /// <param name="atribut"></param>
    /// <param name="hodnotaAtributu"></param>
    public static List<HtmlNode> ReturnTagsWithAttrRek(HtmlNode htmlNode, string tag, string atribut, string hodnotaAtributu)
    {
        List<HtmlNode> vr = new List<HtmlNode>();

        RecursiveReturnTagsWithAttr(vr, htmlNode, tag, atribut, hodnotaAtributu);
        return vr;
    }

    /// <summary>
    /// Vratilo 0 misto 10
    /// A1 je uzel kter?? se bude rekurzivn?? porovn??vat
    /// A2 je n??zev tagu(div, a, atd.) kter?? chci vr??tit.
    /// </summary>
    /// <param name="htmlNode"></param>
    /// <param name="p"></param>
    public static List<HtmlNode> ReturnAllTags(HtmlNode htmlNode, params string[] p)
    {
        List<HtmlNode> vr = new List<HtmlNode>();
        RecursiveReturnAllTags(vr, htmlNode, p);
        return vr;
    }

    /// <summary>
    /// Have also override with List<HtmlNode>
    /// </summary>
    /// <param name="htmlNodeCollection"></param>
    public static List<HtmlNode> TrimTexts(HtmlNodeCollection htmlNodeCollection)
    {
        return HtmlAgilityHelper.TrimTexts(htmlNodeCollection);
    }

    public static List<HtmlNode> TrimTexts(List<HtmlNode> c2)
    {
        return HtmlAgilityHelper.TrimTexts(c2, true);
    }

    public static List<HtmlNode> TrimTexts(List<HtmlNode> c2, bool text, bool comments = false)
    {
        return HtmlAgilityHelper.TrimTexts(c2, text, comments);
    }

    /// <summary>
    /// It's calling by others
    /// Do A3 se m????e vlo??it *
    /// </summary>
    /// <param name="vr"></param>
    /// <param name="html"></param>
    /// <param name="p"></param>
    private static void RecursiveReturnTags(List<HtmlNode> vr, HtmlNode html, string p)
    {
        foreach (HtmlNode item in html.ChildNodes)
        {
            if (HasTagName(item, p))
            {
                //RecursiveReturnTags(vr, item, p);

                vr.Add(item);

                RecursiveReturnTags(vr, item, p);
            }
            else
            {
                RecursiveReturnTags(vr, item, p);
            }
        }
    }

    /// <summary>
    /// Rekurzivn?? vol?? metodu RecursiveReturnAllTags
    /// </summary>
    /// <param name="vr"></param>
    /// <param name="html"></param>
    /// <param name="p"></param>
    private static void RecursiveReturnAllTags(List<HtmlNode> vr, HtmlNode html, params string[] p)
    {
        foreach (HtmlNode item in html.ChildNodes)
        {
            bool contains = false;

            if (p.Length == 1)
            {
                if (item.Name == p[0])
                {
                    contains = true;
                }
            }
            else
            {
                foreach (var t in p)
                {
                    if (item.Name == t)
                    {
                        contains = true;
                    }
                }
            }

            if (contains)
            {
                RecursiveReturnAllTags(vr, item, p);
                if (!vr.Contains(item))
                {
                    vr.Add(item);
                }
            }
            else
            {
                RecursiveReturnAllTags(vr, item, p);
            }
        }
    }

    /// <summary>
    /// Do A2 se m????e zadat *
    /// </summary>
    /// <param name="hn"></param>
    /// <param name="tag"></param>
    private static bool HasTagName(HtmlNode hn, string tag)
    {
        if (tag == AllStrings.asterisk)
        {
            return true;
        }
        return hn.Name == tag;
    }

    /// <summary>
    /// Do A3 se m????e zadat * pro vr??cen?? v??ech tag??
    /// 
    /// Do A5 se m????e vlo??it * pro vr??cen?? tag?? s hledan??m atributem s jakoukoliv hodnotou
    /// </summary>
    /// <param name="vr"></param>
    /// <param name="htmlNode"></param>
    /// <param name="p"></param>
    /// <param name="atribut"></param>
    /// <param name="hodnotaAtributu"></param>
    private static void RecursiveReturnTagsWithAttr(List<HtmlNode> vr, HtmlNode htmlNode, string p, string atribut, string hodnotaAtributu)
    {
        foreach (HtmlNode item in htmlNode.ChildNodes)
        {
            if (HasTagName(item, p))
            {
                if (HasTagAttr(item, atribut, hodnotaAtributu, false))
                {
                    //RecursiveReturnTagsWithAttr(vr, item, p);
                    if (!vr.Contains(item))
                    {
                        vr.Add(item);
                    }
                }
            }
            else
            {
                RecursiveReturnTagsWithAttr(vr, item, p, atribut, hodnotaAtributu);
            }
        }
    }

    private static bool HasTagAttr(HtmlNode item, string atribut, string hodnotaAtributu, bool enoughIsContainsAttribute)
    {
        if (hodnotaAtributu == AllStrings.asterisk)
        {
            return true;
        }
        bool contains = false;
        var attrValue = HtmlHelper.GetValueOfAttribute(atribut, item);
        if (enoughIsContainsAttribute)
        {
            contains = attrValue.Contains(hodnotaAtributu);
        }
        else
        {
            contains = attrValue == hodnotaAtributu;
        }
        return contains;
    }

/// <summary>
    /// Proch??z?? d??ti A1 a pokud n??kter?? m?? n??zev A2, G
    /// Vr??t?? null pokud se takov?? tag nepoda???? naj??t
    /// </summary>
    /// <param name="body"></param>
    /// <param name="nazevTagu"></param>
    public static HtmlNode ReturnTag(HtmlNode body, string nazevTagu)
    {
        //List<HtmlNode> html = new List<HtmlNode>();
        foreach (HtmlNode item in body.ChildNodes)
        {
            if (item.Name == nazevTagu)
            {
                return item;
            }
        }
        return null;
    }

    /// <summary>
    /// Replace A2 by A3
    /// </summary>
    /// <param name="parentNode"></param>
    /// <param name="o2"></param>
    /// <param name="nc"></param>
    public static void ReplaceChildNodeByOuterHtml(HtmlNode parentNode, string o2, HtmlNode nc)
    {
        for (int i = 0; i < parentNode.ChildNodes.Count; i++)
        {
            var item = parentNode.ChildNodes[i];
            if (item.OuterHtml == o2)
            {
                // First is new, Second is old!!!
                parentNode.ReplaceChild(nc,item);
                break;
            }
        }
    }
}