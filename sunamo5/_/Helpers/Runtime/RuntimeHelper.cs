using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;

public partial class RuntimeHelper
{
    static Type type = typeof(RuntimeHelper);

    public static List<Delegate> GetInvocationList(Delegate e)
    {
        if (e == null)
        {
            return new List<Delegate>();
        }

        return e.GetInvocationList().ToList();
    }

    /// <summary>
    /// Not working for WPF
    /// </summary>
    /// <returns></returns>
    public static bool IsConsole()
    {
        return Environment.UserInteractive;
    }

   

    public static bool HasEventHandler(Delegate e)
    {
        return GetInvocationList(e).Count() > 0;
    }

    /// <summary>
    /// Nedokázal jsem zjistit zda SuMenuItem má registrovaný Click - reflexe u něj nenašla vlastnost Events
    /// Musel jsem kvůli toho vytvořit SuMenuItem
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="control"></param>
    /// <param name="eventName"></param>
    /// <returns></returns>
    public static bool HasEventHandler<T>(T control, string eventName)
    {
#if DEBUG
        
#endif
        var pi = typeof(T).GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);
        if (pi == null)
        {
            return false;
        }
        EventHandlerList events = (EventHandlerList)pi.GetValue(control, null);

        object key = typeof(T)
            .GetField(eventName, BindingFlags.NonPublic | BindingFlags.Static)
            .GetValue(null);

        Delegate handlers = events[key];

        return handlers != null && handlers.GetInvocationList().Any();
    }


    //public static List<Delegate> GetEventHandlers(DataGridView obj, string eventName)
    //{
    //    EventHandlerList eventHandlerList = (EventHandlerList)obj.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(obj, null);
    //    MethodInfo findMethod = eventHandlerList.GetType().GetMethod("Find", BindingFlags.NonPublic | BindingFlags.Instance);

    //    string fieldName = "EVENT_DATAGRIDVIEW" + eventName.ToUpper();
    //    FieldInfo field = obj.GetType().GetField(fieldName, BindingFlags.Static | BindingFlags.NonPublic);

    //    if (obj.GetType().GetEvent(eventName) != null && field != null)
    //    {
    //        object value = findMethod.Invoke(eventHandlerList, new object[] { field.GetValue(obj) });
    //        if (value != null)
    //        {
    //            FieldInfo handlerField = value.GetType().GetField("handler", BindingFlags.NonPublic | BindingFlags.Instance);
    //            return ((Delegate)handlerField.GetValue(value)).GetInvocationList().ToList();
    //        }
    //    }

    //    return new List<Delegate>();
    //}



    /// <summary>
    /// Default is true for automatically avoiding errors
    /// </summary>
    /// <param name = "controlWithResult"></param>
    /// <param name = "a"></param>
    /// <param name = "throwException"></param>
    public static void AttachChangeDialogResult(IControlWithResultDebug controlWithResult, VoidBoolNullable a, bool throwException = true)
    {
        var count = controlWithResult.CountOfHandlersChangeDialogResult();
        if (count > 0)
        {
            if (throwException )
            {
                ThrowEx.Custom( SunamoPageHelperSunamo.i18n(XlfKeys.ChangeDialogResultHasAlredyRegisteredHandler));
            }
            else
            {
            // Do nothing
            }
        }
        else
        {
            controlWithResult.ChangeDialogResult += a;
        }
    }
}