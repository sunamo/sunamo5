using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ValidationHelper
{
    public static bool checkForFalseValidated = false;

    static bool _validated = false;

    public static bool validated
    {
        set
        {
            if (checkForFalseValidated && !value)
            {

            }
            _validated = value;
        }
        get
        {
            return _validated;
        }
    }
}
