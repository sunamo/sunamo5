using System.Collections.Generic;


    public interface IPowershellHelper
    {
        void CmdC(string v);
        string DetectLanguageForFileGithubLinguist(string windowsPath);
        List<string> ProcessNames();
    }
