using System;
using System.Collections.Generic;
using System.Text;

public class TextOutputGeneratorArgs
{
    public bool headerWrappedEmptyLines = true;
    public bool insertCount = false;
    public string whenNoEntries = Consts.NoEntries;
    public TextOutputGeneratorArgs()
    {

    }

    public TextOutputGeneratorArgs(bool headerWrappedEmptyLines, bool insertCount)
    {
        this.headerWrappedEmptyLines = headerWrappedEmptyLines;
        this.insertCount = insertCount;
    }
}