﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PpkOnDriveArgs
{
    public string file;
    /// <summary>
    /// Originally was false but think that true is better
    /// Its not important because still I'm using old ctor interface and it will set to false if needed
    /// </summary>
    public bool load = true;
    public bool save = true;
    public bool loadChangesFromDrive = true;

}