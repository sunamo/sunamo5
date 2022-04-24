using System;
using System.Collections.Generic;
using System.Text;

public interface IDW
{
    string SelectOfFolder(string rootFolder);
    string SelectOfFolder(Environment.SpecialFolder rootFolder);
}