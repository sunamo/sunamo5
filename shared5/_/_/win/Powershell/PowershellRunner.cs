using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

public class PowershellRunner : IPowershellRunner
{
    public static IPowershellRunner p = null;
    public static PowershellRunner ci = new PowershellRunner();

    private PowershellRunner()
    {

    }

    public ProgressState clpb { get; set; }

    public List<List<string>> Invoke(IEnumerable<string> commands)
    {
        return p.Invoke(commands);
    }

    public List<List<string>> Invoke(IEnumerable<string> commands, PsInvokeArgs e)
    {
        return p.Invoke(commands, e);
    }

    public List<string> Invoke(string commands)
    {
        return p.Invoke(commands);
    }

    public Task<List<List<string>>> InvokeAsync(IEnumerable<string> commands, PsInvokeArgs e = null)
    {
        return p.InvokeAsync(commands, e);
    }

    public string InvokeLinesFromString(string v, bool writePb)
    {
        return p.InvokeLinesFromString(v, writePb);
    }

    public List<string> InvokeProcess(string exeFileNameWithoutPath, string arguments)
    {
        return p.InvokeProcess(exeFileNameWithoutPath, arguments);
    }

    public List<string> InvokeSingle(string command)
    {
        return p.InvokeSingle(command);
    }
}