﻿using desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

public interface IControlPlugin
{
    List<SuMenuItem> RootUc { get; }
    SuMenuItem MiUc { get; }
    
}

