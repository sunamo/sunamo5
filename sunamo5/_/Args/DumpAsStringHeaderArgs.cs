using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DumpAsStringHeaderArgs
{
    public static DumpAsStringHeaderArgs Default = new DumpAsStringHeaderArgs();

    public DumpAsStringHeaderArgs()
    {

    }

    /// <summary>
    /// Only names of properties to get
    /// If starting with ! => surely delete
    /// </summary>
    public List<string> onlyNames = new List<string>();
}
