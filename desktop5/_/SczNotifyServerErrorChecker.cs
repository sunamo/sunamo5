using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using sunamo.Essential;

public class SczNotifyServerErrorChecker
{
    static Type type = typeof(SczNotifyServerErrorChecker);

    System.Timers.Timer t;

    public SczNotifyServerErrorChecker()
    {
        if (!VpsHelperSunamo.IsVps)
        {
            t = new System.Timers.Timer(60000);
            t.AutoReset = true;
            t.Elapsed += T_Elapsed;
            T_Elapsed(null, null);
        }
    }

    private void T_Elapsed(object sender, ElapsedEventArgs e2)
    {
        if (VpsHelperSunamo.IsQ)
        {
            return;
        }
            var p = Process.GetProcesses().Where(e => e.ProcessName == "SczNotifyServerError").Select(d => d.ProcessName).ToList();
            if (p.Count == 0)
            {
            var f1 = @"D:\pa\_sunamo\SczNotifyServerError\SczNotifyServerError.exe";
            var f2 = @"D:\pa\_sunamo\SczNotifyServerError\SczNotifyServerError2.exe";

            var b1 = FS.ExistsFile(f1);
            var b2 = FS.ExistsFile(f2);

            //MessageBox.Show(b1 + " " + b2);

            //ThrowExceptions.Custom(Exc.GetStackTrace(),type, Exc.CallingMethod(), "SczNotifyServerError is not running, starting it");

            if (b1)
            {
                PH.Start(f1);
            }
            if (!b1 && b2)
            {
                // Do nothing, its feature
            }
            else if (!b1 &&!b2)
            {
                ThrowExceptions.Custom(f1 +" doesn't exists!");
            }
            
           }
    }
}