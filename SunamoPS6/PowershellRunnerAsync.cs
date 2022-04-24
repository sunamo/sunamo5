using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using win.Helpers.Powershell;

public class PowershellRunnerAsync : PowershellRunnerBase
{
    public ProgressState clpb { get; set; }
    public static PowershellRunnerAsync ci = new PowershellRunnerAsync();

    public async Task< string> InvokeLinesFromString(string v, bool writePb)
    {
        var l = SH.GetLines(v);

        var result = await InvokeAsync(l, new PsInvokeArgs { writePb = writePb });

        StringBuilder sb = new StringBuilder();

        foreach (var item in result)
        {
            sb.AppendLine(SH.JoinNL(item));
        }

        return sb.ToString();
    }

    /// <summary>
    /// Tested, working
    /// For every command return at least one entry in result
    /// </summary>
    /// <param name="commands"></param>
    public async Task<List<List<string>>> InvokeAsync(IEnumerable<string> commands, PsInvokeArgs e = null)
    {
        if (e == null)
        {
            e = new PsInvokeArgs();
        }

        List<List<string>> returnList = new List<List<string>>();
        PowerShell ps = null;
        //  After leaving using is closed pipeline, must watch for complete or 
        var writePb = e.writePb;
        using (ps = PowerShell.Create())
        {
            if (writePb)
            {
                clpb.OnOverallSongs(commands.Count());
            }
            foreach (var item in commands)
            {
                ps.AddScript(item);

                var psObjects = await ps.InvokeAsync();

                returnList.Add(ProcessPSObjects(psObjects));
                if (writePb)
                {
                    clpb.OnAnotherSong();
                }
            }
            if (writePb)
            {
                clpb.OnWriteProgressBarEnd();
            }
            
        }

        return returnList;
    }
}