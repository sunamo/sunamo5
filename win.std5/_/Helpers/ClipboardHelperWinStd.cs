using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class ClipboardHelperWinStd : IClipboardHelper
{
    static TextCopy.Clipboard textCopy = new TextCopy.Clipboard();
    public static ClipboardHelperWinStd Instance = null;

    public static IClipboardHelper CreateInstance()
    {
        if (Instance == null)
        {
            Instance = new ClipboardHelperWinStd();
        }

        return Instance;
    }

    public bool ContainsText()
    {
        return true;
    }

    public void CutFiles(params string[] selected)
    {
        ThrowEx.NotImplementedMethod();
    }

    public List<string> GetLines()
    {
        return SH.GetLines(GetText());
    }

    public string GetText()
    {
        return textCopy.GetText();
    }

    public void SetLines(IEnumerable d)
    {
        SetText(SH.JoinNL(d));
    }

    public void SetList(List<string> d)
    {
        SetText(SH.JoinNL(d));
    }

    public void SetText(string s)
    {
        textCopy.SetText(s);
    }

    public void SetText(StringBuilder stringBuilder)
    {
        SetText(stringBuilder.ToString());
    }

    public void SetText2(string s)
    {
        SetText(s);
    }

    public void SetText3(string s)
    {
        SetText(s);
    }
}