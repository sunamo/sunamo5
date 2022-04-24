using System.Collections.Generic;


    public interface IPowershellBuilder
    {
        void AddArg(string argName, string argValue);
        void AddRaw(string v);
        void AddRawLine(string v);
        string Cd(string path);
        void Clear();
        void CmdC(string v);
        void RemoveItem(string v);
        List<string> ToList();
        string ToString();
    }
