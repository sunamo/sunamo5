using System;
using System.Collections.Generic;
using System.Text;

public class PowershellHelper : IPowershellHelper
{
    public static IPowershellHelper p = null;
    public static PowershellHelper ci = new PowershellHelper();

    private PowershellHelper()
    {

    }

    public void CmdC(string v)
    {
        p.CmdC(v);
    }

    public string DetectLanguageForFileGithubLinguist(string windowsPath)
    {
        return p.DetectLanguageForFileGithubLinguist(windowsPath);
    }

    public List<string> ProcessNames()
    {
        return p.ProcessNames();
    }
}