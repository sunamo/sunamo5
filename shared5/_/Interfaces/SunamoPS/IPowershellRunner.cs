using System.Collections.Generic;
using System.Threading.Tasks;


    public interface IPowershellRunner
    {
        ProgressState clpb { get; set; } 
        List<List<string>> Invoke(IEnumerable<string> commands);
        List<List<string>> Invoke(IEnumerable<string> commands, PsInvokeArgs e);
        List<string> Invoke(string commands);
        Task<List<List<string>>> InvokeAsync(IEnumerable<string> commands, PsInvokeArgs e = null);
        string InvokeLinesFromString(string v, bool writePb);
        List<string> InvokeProcess(string exeFileNameWithoutPath, string arguments);
        List<string> InvokeSingle(string command);
        //List<string> ProcessPSObjects(ICollection<PSObject> pso);
    }
