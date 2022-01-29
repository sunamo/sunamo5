using sunamo.Data;
using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

public class MayExtHelper
{
    /// <summary>
    /// True when is there error
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="result"></param>
    /// <returns></returns>
    public static bool MayExc<T>(ResultWithException<T> result)
    {
        if (result.exc != null)
        {
            ThisApp.SetStatus(TypeOfMessage.Error, result.exc);
            return true;
        }
        return false;
    }

    /// <summary>
    /// True when is there error
    /// </summary>
    /// <param name="r"></param>
    /// <returns></returns>
    public static bool XmlDocument(ResultWithException<XmlDocument> r)
    {
        return MayExc<XmlDocument>(r);
    }
}