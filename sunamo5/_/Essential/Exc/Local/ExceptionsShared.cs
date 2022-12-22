using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public partial class Exceptions
    {
        public static string RepeatAfterTimeXTimesFailed(string before, int times, int timeoutInMs, string address)
    {
        return before + $"Loading uri {address} failed {times} ({timeoutInMs} ms timeout) HTTP Error: {SharedAlgorithms.lastError}";
    }


    public static string NotInt(string before, string what, object value)
    {
        if (!BTS.IsInt(value.ToString()))
        {
            return before + what + " is not with value " + value  + " valid integer number";
        }
        return null;
    }

    public static string NotValidXml(string before, string path, Exception ex)
    {
        return before + path + AllStrings.space + Exceptions.TextOfExceptions(ex);
    }

    public static string ViolationSqlIndex(string before, string tableName, ABC columnsInIndex)
    {
        return before + $"{tableName} {columnsInIndex.ToString()}";
    }



    
}