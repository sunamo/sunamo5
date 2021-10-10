using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FileTextLogger
{
    /// <summary>
    /// It was be totally nonsense, just do it in memory. Even if I call sw.Close and sw.Dispose app still hold the file
    /// </summary>
    public StreamWriter sw = null;
    public string fn = null;
    public StringBuilder sb = new StringBuilder();

    /// <summary>
    /// Buffer 1MB
    /// </summary>
    /// <param name="fn"></param>
    public FileTextLogger(string fn, int bufferInMb)
    {
        this.fn = fn;
        //FileStream fs = new FileStream(fn, FileMode.OpenOrCreate);

        // 1024 * 1024 *
        // cant use, could terminate itself
        //PH.ShutdownProcessWhichOccupyFileHandleExe(fn);

        // It was be totally nonsense, just do it in memory. Even if I call sw.Close and sw.Dispose app still hold the file
        //sw = File.CreateText(fn);//, Encoding.UTF8,  1024 * bufferInMb);
        //sw.AutoFlush = true;
        WriteNewLine(DateTime.Now.ToLongTimeString());
    }

    public void WriteNewLine(string l)
    {
        sb.AppendLine(l);
    }
}
