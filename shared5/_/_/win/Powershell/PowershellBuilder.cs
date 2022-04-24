using System;
using System.Collections.Generic;
using System.Text;

public class PowershellBuilder : IPowershellBuilder
{
    public static IPowershellBuilder p = null;
    public static PowershellBuilder ci = new PowershellBuilder();

    public PowershellBuilder()
    {

    }

    public void AddArg(string argName, string argValue)
    {
        p.AddArg(argName, argValue);
    }

    public void AddRaw(string v)
    {
        p.AddRaw(v);
    }

    public void AddRawLine(string v)
    {
        p.AddRawLine(v);
    }

    public string Cd(string path)
    {
        return p.Cd(path);
    }

    public void Clear()
    {
        p.Clear();
    }

    public void CmdC(string v)
    {
        p.CmdC(v);
    }

    public void RemoveItem(string v)
    {
        p.RemoveItem(v);
    }

    public List<string> ToList()
    {
        return p.ToList();
    }
}