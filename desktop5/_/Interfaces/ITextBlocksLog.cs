using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

public interface ITextBlocksLog
{
    TextBlock TbLastErrorOrWarning { get; }
    TextBlock TbLastOtherMessage { get; }

}
