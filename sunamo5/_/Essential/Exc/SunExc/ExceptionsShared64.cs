
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class Exceptions
{
    #region For easy copy from ExceptionsShared64.cs
    public static string NotImplementedCase(string before, object niCase)
    {
        string fr = string.Empty;
        if (niCase != null)
        {
            fr = " for ";
            if (niCase.GetType() == typeof(Type))
            {
                fr += ((Type)niCase).FullName;
            }
            else
            {
                fr += niCase.ToString();
            }
        }

        return CheckBefore(before) + SunamoPageHelperSunamo.i18n(XlfKeys.NotImplementedCase) + fr + ". " + SunamoPageHelperSunamo.i18n(XlfKeys.publicProgramErrorPleaseContactDeveloper) + ".";
    }

    public static string DifferentCountInLists(string before, string namefc, int countfc, string namesc, int countsc)
    {
        if (countfc != countsc)
        {
            // sess and SunamoPageHelperSunamo have the i18n method. Sess calculates that the text translation is in dictionaries, while SunamoPageHelperSunamo needs to have a method set for this. If this doesn't work, replace it with SunamoPageHelperSunamo
            // coz SunamoPageHelperSunamo is not in SunamoExceptions available
            return CheckBefore(before) + " " + sess.i18n(XlfKeys.DifferentCountElementsInCollection) + " " + string.Concat(namefc + AllStrings.swda + countfc) + " vs. " + string.Concat(namesc + AllStrings.swda + countsc);
        }

        return null;
    }

    public static string Custom(string before, string message)
    {
        return CheckBefore(before) + message;
    }

    private static string CheckBefore(string before)
    {
        if (string.IsNullOrWhiteSpace(before))
        {
            return "";
        }
        return before + ": ";
    }

    public static string NotImplementedMethod(string before)
    {
        return CheckBefore(before) + SunamoPageHelperSunamo.i18n(XlfKeys.NotImplementedCasePublicProgramErrorPleaseContactDeveloper) + ".";
    }

    /// <summary>
    /// !FS.IsWindowsPathFormat
    /// </summary>
    /// <param name="before"></param>
    /// <param name="argName"></param>
    /// <param name="argValue"></param>
    /// <returns></returns>
    public static string IsNotWindowsPathFormat(string before, string argName, string argValue)
    {
        if (RaiseIsNotWindowsPathFormat)
        {
            var badFormat = !FS.IsWindowsPathFormat(argValue);

            if (badFormat)
            {
                return CheckBefore(before) + " " + argName + " " + SunamoPageHelperSunamo.i18n(XlfKeys.isNotInWindowsPathFormat);
            }
        }

        return null;
    }

    public static StringBuilder sbAdditionalInfoInner = new StringBuilder();
    public static StringBuilder sbAdditionalInfo = new StringBuilder();

    private static string AddParams()
    {
        sbAdditionalInfo.Insert(0, Environment.NewLine);
        sbAdditionalInfo.Insert(0, "Outer:");
        sbAdditionalInfo.Insert(0, Environment.NewLine);

        sbAdditionalInfoInner.Insert(0, Environment.NewLine);
        sbAdditionalInfoInner.Insert(0, "Inner:");
        sbAdditionalInfoInner.Insert(0, Environment.NewLine);

        var addParams = sbAdditionalInfo.ToString();
        var addParamsInner = sbAdditionalInfoInner.ToString();
        return addParams + addParamsInner;
    }


    public static string IsNullOrEmpty(string before, string argName, string argValue)
    {
        string addParams = null;

        if (argValue == null)
        {
            addParams = AddParams();
            return CheckBefore(before) + argName + " is null" + addParams;
        }
        else if (argValue == string.Empty)
        {
            addParams = AddParams();
            return CheckBefore(before) + argName + " is empty (without trim)" + addParams;
        }
        else if (argValue.Trim() == string.Empty)
        {
            addParams = AddParams();
            return CheckBefore(before) + argName + " is empty (with trim)" + addParams;
        }

        return null;
    }

    public static bool RaiseIsNotWindowsPathFormat;

    /// <summary>
    /// Start with Consts.Exception to identify occur
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="alsoInner"></param>
    public static string TextOfExceptions(Exception ex, bool alsoInner = true)
    {
        if (ex == null)
        {
            return String.Empty;
        }

        StringBuilder sb = new StringBuilder();
        sb.Append(Consts.Exception);
        sb.AppendLine(ex.Message);
        while (ex.InnerException != null)
        {
            ex = ex.InnerException;
            sb.AppendLine(ex.Message);
        }
        var r = sb.ToString();
        return r;
    }

    public static string ArgumentOutOfRangeException(string before, string paramName, string message)
    {
        if (paramName == null)
        {
            paramName = string.Empty;
        }

        if (message == null)
        {
            message = string.Empty;
        }

        return CheckBefore(before) + paramName + " " + message;
    }
    #endregion
}