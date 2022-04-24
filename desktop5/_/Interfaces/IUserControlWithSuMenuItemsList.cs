using desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

[ComVisible(true)]
[InterfaceType(ComInterfaceType.InterfaceIsDual)]
public interface IUserControlWithSuMenuItemsList : IUserControl
    {
        List<SuMenuItem> SuMenuItems();
    
    void RemoveWhichHaveNoItem();
}