﻿
using sunamo.Essential;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public partial class LoggerBase
{
    // TODO: Make logger public class as base and replace all occurences With Instance 
    private VoidStringParamsObjects _writeLineDelegate;
    public bool IsActive = true;
    private static Type type = typeof(LoggerBase);
    private StringBuilder _sb = new StringBuilder();
    /// <summary>
    /// Because in sunamo is not any MessageBox
    /// </summary>
     //public static event VoidString ShowMessageBox;
    public void DumpObject(string name, object o, DumpProvider d, params string[] onlyNames)
    {
        var dump = RH.DumpAsString(new DumpAsStringArgs { name = name, o = o, d = d, onlyNames = onlyNames.ToList() });//  , o, d, onlyNames);
        WriteLine(dump);
        WriteLine(AllStrings.space);
    }

    public void DumpObjects(string name, IEnumerable o, DumpProvider d, params string[] onlyNames)
    {
        int i = 0;
        foreach (var item in o)
        {
            DumpObject(name + " #" + i, item, d, onlyNames);
            i++;
        }
    }

    /// <summary>
    /// Only for debug purposes
    /// </summary>
    /// <param name = "v"></param>
    /// <param name = "args"></param>
    public void ClipboardOrDebug(string v, params string[] args)
    {
#if DEBUG
        //DebugLogger.DebugWriteLine(TypeOfMessage.Appeal, v, args);
#else
    //sb.AppendLine(TypeOfMessage.Appeal + ": " + string.Format(v, args));
    //ClipboardHelper.SetText(sb.ToString());
#endif
    }


    /// <summary>
    /// Only due to Old sfw apps
    /// </summary>
    /// <param name = "v1"></param>
    /// <param name = "name"></param>
    /// <param name = "v2"></param>
    public void WriteLineFormat(string v1, params string[] name)
    {
        WriteLine(v1, name);
    }

    public LoggerBase(VoidStringParamsObjects writeLineDelegate)
    {
        _writeLineDelegate = writeLineDelegate;
    }

    public void WriteCount(string collectionName, IEnumerable list)
    {
        WriteLine(collectionName + " count: " + list.Count());
    }

    public void WriteList(string collectionName, List<string> list)
    {
        WriteLine(collectionName + " elements:");
        WriteList(list);
    }

    internal void WriteListOneRow(List<string> item, string swd)
    {
#if DEBUG
        _writeLineDelegate.Invoke(SH.JoinString(swd, item));
#endif
    }

    public void WriteArgs(params object[] args)
    {
        _writeLineDelegate.Invoke(SH.JoinPairs(args));
    }

    public bool IsInRightFormat(string text, params object[] args)
    {
        try
        {
            _writeLineDelegate.Invoke(text, args);
        }
        catch (Exception ex)
        {
            return false;
        }

        return true;
    }

    public void WriteLine(string text, params object[] args)
    {
        if (IsActive)
        {
            _writeLineDelegate.Invoke(text, args);
        }
    }

    public void WriteLineNull(string text, params object[] args)
    {
        if (IsActive)
        {
            _writeLineDelegate.Invoke(SH.NullToStringOrDefault( text), args);
        }
    }

    /// <summary>
    /// for compatibility with Console.WriteLine 
    /// </summary>
    /// <param name = "what"></param>
    public void WriteLine(object what)
    {
        if (what != null)
        {
            WriteLine(SH.ListToString(what));
        }
    }

    /// <summary>
    /// Will auto append ": "
    /// </summary>
    /// <param name="what"></param>
    /// <param name="text"></param>
    public void WriteLine(string what, object text)
    {
        if (text == null)
        {
            text = Consts.nulled;
        }

        

            string append = string.Empty;
            if (!string.IsNullOrEmpty(what))
            {
                append = what + ": ";
            }

            WriteLine(append + text.ToString());
        
    }

    public void WriteNumberedList(string what, List<string> list, bool numbered)
    {
        _writeLineDelegate.Invoke(what + AllStrings.colon);
        for (int i = 0; i < list.Count; i++)
        {
            if (numbered)
            {
                WriteLine((i + 1).ToString(), list[i]);
            }
            else
            {
                WriteLine(list[i]);
            }
        }
    }

    public void WriteList(List<string> list)
    {
        list.ForEach(d => WriteLine(d));
    }
}