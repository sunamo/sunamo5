using sunamo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sunamo.Essential;

public static partial class WriterEventLog{
    static EventLog mainEventLogOfApplication = null;
    public const string ApplicationLogName = XlfKeys.Application;
    static EventLog eventLogWindowsApplication = null;
    static string scz = "sunamo.cz";

    public static void WriteException(string stacktrace, string exception)
    {
        if (!IsAdmin())
        {
            return;
        }
        WriterEventLog.WriteToMainAppLog(exception + Environment.NewLine + stacktrace, EventLogEntryType.Error);
    }

    #region In scz never call CreateMainAppLogScz, so commented it
    //public static void WriteToMainAppLogScz(string text, EventLogEntryType type)
    //{
    //    if (!IsAdmin())
    //    {
    //        return;
    //    }
    //    WriteToWindowsLogs(scz, text, type);
    //}

    //public static bool CreateMainAppLogScz()
    //{
    //    if (!IsAdmin())
    //    {
    //        return false;
    //    }
    //    bool b = CreateMainAppLog(scz);
    //    WriteToMainAppLogScz(sess.i18n(XlfKeys.Template), EventLogEntryType.Information);
    //    return b;
    //} 
    #endregion

    /// <summary>
    /// Zda to funguje nevím, já už jsem v aplikaci zapisoval metodou WriteToMainAppLog a žádná aplikace nemůže zapisovat do více logů
    /// </summary>
    /// <param name = "text"></param>
    /// <param name = "type"></param>
    public static void WriteToWindowsLogs(string appName, string text, EventLogEntryType type)
    {
        if (!IsAdmin())
        {
            return;
        }

        // Exists every time. Cant iterate - SecurityException will be happen in asp.net app
        if (!EventLog.SourceExists(appName))
        {
            EventLog.CreateEventSource(new EventSourceCreationData(appName, ApplicationLogName));
        }

        if (eventLogWindowsApplication == null)
        {
            eventLogWindowsApplication = new EventLog();
            eventLogWindowsApplication.Source = appName;
        }

        eventLogWindowsApplication.WriteEntry(text, type);
    }

    /// <summary>
    /// Cant use in Web - SecurityException. Use WriteToWindowsLogsApplication()
    /// K prvnímu volání potřebuje administrátorská práva, aby mohla vytvořil log s názvem ThisApp.Name. 
    /// K zapisování událostí již pak administrátorská práva nepotřebuje.
    /// 
    /// In web is too much better WriteToPageWhenMyPoda
    /// </summary>
    /// <param name = "text"></param>
    /// <param name = "type"></param>
    public static void WriteToMainAppLog(string text, EventLogEntryType type, string methodName = null)
    {
        string eventLogNameFromEventLogNames = ThisApp.EventLogName;
        if (!IsAdmin())
        {
            return;
        }
        if (methodName != null)
        {
            text = methodName + ": " + text;
        }

        // Exception in ASP.NET: 'The source was not found, but some or all event logs could not be searched.  Inaccessible logs: Security.'
        // If the user account under which the code is running does not have read access to a subkey that it attempts to access (in your case, the Security subkey)
        string name = ThisApp.EventLogName;
        CreateMainAppLog(eventLogNameFromEventLogNames);
        if (mainEventLogOfApplication == null)
        {
            //logName, Environment.MachineName, ThisApp.Name - Tyto parametry jsem předával do konstruktoru EventLog ale s nimi to nikdy nefungovalo, nevyhodila se sice výjimka ale v EventLogu nic nebylo
            mainEventLogOfApplication = new EventLog(name);
            mainEventLogOfApplication.Source = name;
        }

        TypedSunamoLogger.Instance.Information(text);
        mainEventLogOfApplication.WriteEntry(text, type);
    }

    public static bool IsAdmin()
    {
        return WindowsSecurityHelper.IsUserAdministrator();
    }

    public static bool CreateMainAppLog(string notThisAppNameJustEventLogNames)
    {
        if (!IsAdmin())
        {
            return false;
        }
        //if (EventLog.SourceExists(name))
        //{
        //    // Excetpion: The event log source 'sunamo.cz' cannot be deleted, because it's equal to the log name.
        //    System.Diagnostics.EventLog.DeleteEventSource("sunamo.cz");
        //}

        bool existsSource = false;
        try
        {
            /*SourceExists
Tohle je divné že stačí odebrat .Wpf a už mi to jednak nenajde přes SourceExists
zároveŇ CreateEventSource mi hlásí pře existuje
Only the first eight characters of a custom log name are significant, and there is already another log on the system using the first eight characters of the name given. Name given: 'AllProjectsSearch', name of existing log: 'AllProjectsSearch.Wpf'.'

            Nicméně to dává smysl, existuje log .Wpf, proto už nemůžu vytvořit bez .wpf
            proto musím mít speciální třídu jen pro názvy v eventlogu
             */ 

            existsSource = EventLog.SourceExists(notThisAppNameJustEventLogNames);
        }
        catch (Exception ex)
        {
        }

        if (!existsSource)
        {
            //EventLog.Delete("SunamoAdmin");
            // Zkoušel jsme zadávat do metody CreateEventSource argumenty source, logName s hodnotami name, logName ale s těmi to taky nefungovalo
            EventLog.CreateEventSource(new EventSourceCreationData(notThisAppNameJustEventLogNames, notThisAppNameJustEventLogNames));
        }

        return existsSource;
    }


}