using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MSSloupecDBArgs
{
    public string nazev;
    public bool primaryKey;
    public string referencesTable;
    public string referencesColumn;
    public bool mustBeUnique;
    public bool canBeNull;
    public Signed signed;
    public bool identityIncrementBy1;
}