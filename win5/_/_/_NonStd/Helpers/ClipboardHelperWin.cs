

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using win;
// if app isnt STA, raise exception
//using System.Windows;
// if app isnt STA, return empty. 
using System.Collections;
//using System.Windows;
using sunamo.Essential;
using System.Windows.Forms;
using cl;
//using System.Windows;

/// <summary>
/// In all apps use only ClipboardHelperWin and don't import win project!!!! Import only win.std should be enough for most of projects because contains ClipboardHelper
/// 
/// Must be in win due to ClipboardMonitor which uses W32 methods
/// Use here only managed method! I could avoid reinstall Windows (RepairJpn). Use only managed also for working with formats.
/// Use in ClipboardAsync and ClipboardHelperWin only System.Windows.Forms as using, not System.Windows which have very similar interface.
/// As single variable without using is use TextCopy
/// </summary>
public class ClipboardHelperWin : IClipboardHelper
{
    public const uint CF_UNICODETEXT = 13U;
    public const uint CF_DSPTEXT = 0x0081;
    public const uint CF_LOCALE = 16U;
    public const uint CF_OEMTEXT = 7U;
    public const uint CF_TEXT = 1U;

    IClipboardMonitor clipboardMonitor = null;
    public static ClipboardHelperWin Instance = new ClipboardHelperWin();
    TextCopy.Clipboard clipboard = null;

    private ClipboardHelperWin()
    {
        clipboard = new TextCopy.Clipboard();
        // NOT here, must be assigned in every app instead of 
        //if (ThisApp.Name != null)
        //{
        //    if (ThisApp.Name != "ConsoleApp1" && ThisApp.Name != "ConsoleAppEd")
        //    {
        //        clipboardMonitor = ClipboardMonitor.Instance;
        //    }
        //}

    }

    public static IClipboardHelper CreateInstance()
    {
        if (ThisApp.async_)
        {
            // use ClipboardHelperWinTextCopy
            return null;
        }
        if (Instance == null)
        {
            Instance = new ClipboardHelperWin();
        }

        return Instance;
    }



    #region Get,Set
    /// <summary>
    /// Use here only managed method! I could avoid reinstall Windows (RepairJpn). Use only managed also for working with formats.
    /// not working if was pasted into visual studio (but code yes), created SetText2
    /// </summary>
    /// <param name="v"></param>
    public void SetText(string v)
    {
        if (clipboardMonitor != null)
        {
            clipboardMonitor.afterSet = null;
        }

        //bool tryAgain = false;


        clipboard.SetText(v);

        //        for (int i = 0; i < 10; i++)
        //        {
        //            try
        //            {
        //                // true to keep data also after app close
        //                // Commented coz require import PresentationFramework to every assembly
        //                //Clipboard.SetDataObject(v, true);

        //                /*
        //Zkusil jsem všechny možné metody uložení textu, u všech bylo 0xc0000374 exc:
        //                Clipboard.SetDataObject(v, true);
        //                SetText3
        //                čekání a opakovaný pokus
        //                ProcessHoldingClipboard
        //                 */
        //                System.Windows.Forms.ClipboardHelper.SetText(v);

        //                //SetText3(v);

        //                return;
        //            }
        //            catch {
        //                //var comException = ex as System.Runtime.InteropServices.COMException;

        //                //if (comException != null && comException.ErrorCode == -2147221040)
        //                //{
        //                //    var pr = W32.ProcessHoldingClipboard();
        //                //    pr.Kill();

        //                //    //tryAgain = true;
        //                //}

        //            }
        //            System.Threading.Thread.Sleep(10);
        //        }

        //if (tryAgain)
        //{
        //    SetText3(v);
        //}
        // coz OpenClipboard exit app without exception
        //SetText2(v);
    }

    public static string GetTextW32()
    {
        #region Původní verze jež funguje
        if (!W32.IsClipboardFormatAvailable(CF_UNICODETEXT))
            return null;

        try
        {
            try
            {
                if (!W32.OpenClipboard(IntPtr.Zero))
                    return null;
            }
            catch (Exception ex)
            {
            }

            IntPtr handle = IntPtr.Zero;

            try
            {
                handle = W32.GetClipboardData(CF_UNICODETEXT);
                if (handle == IntPtr.Zero)
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }



            IntPtr pointer = IntPtr.Zero;

            try
            {
                pointer = W32.GlobalLock(handle);
                if (pointer == IntPtr.Zero)
                    return null;

                UIntPtr size2 = W32.GlobalSize(handle);
                int size = (int)size2;
                byte[] buff = new byte[size];

                Marshal.Copy(pointer, buff, 0, size);

                return Encoding.Unicode.GetString(buff).TrimEnd('\0');
            }
            catch (Exception ex)
            {
                return null;

            }
            finally
            {
                if (pointer != IntPtr.Zero)
                    W32.GlobalUnlock(handle);
            }
        }
        finally
        {
            W32.CloseClipboard();
        }
        #endregion

        #region Mnou editovaná verze když to nefungovalo
        //if (!W32.IsClipboardFormatAvailable(CF_UNICODETEXT))
        //    return null;

        //bool opened = false;
        //for (int i = 0; i < 10; i++)
        //{
        //    try
        //    {
        //        opened = W32.OpenClipboard(IntPtr.Zero);
        //        break;
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
        //if (!opened)
        //{
        //    return null;
        //}

        //IntPtr handle = IntPtr.Zero;

        //try
        //{
        //    handle = W32.GetClipboardData(CF_UNICODETEXT);
        //    if (handle == IntPtr.Zero)
        //        return null;
        //}
        //catch (Exception ex)
        //{
        //    return null;
        //}



        //IntPtr pointer = IntPtr.Zero;

        //try
        //{
        //    pointer = W32.GlobalLock(handle);
        //    if (pointer == IntPtr.Zero)
        //        return null;

        //    UIntPtr size2 = W32.GlobalSize(handle);
        //    int size = (int)size2;
        //    byte[] buff = new byte[size];

        //    Marshal.Copy(pointer, buff, 0, size);

        //    return Encoding.Unicode.GetString(buff).TrimEnd('\0');
        //}
        //catch (Exception ex)
        //{
        //    return null;

        //}
        //finally
        //{
        //    if (pointer != IntPtr.Zero)
        //        W32.GlobalUnlock(handle);
        //}
        ////}
        ////finally
        ////{
        //W32.CloseClipboard();
        ////} 
        #endregion
    }

    /// <summary>
    /// Use here only managed method! I could avoid reinstall Windows (RepairJpn). Use only managed also for working with formats.
    /// </summary>
    public string GetText()
    {
        #region Nepoužívat, 1) celá třída vypadá jak by ji psal totální amatér. 2) havaruje mi to app a nevyhodi pritom zadnou UnhaldedException
        //ClipboardAsync ca = new ClipboardAsync();
        //string s = ca.GetText();
        //return s;
        #endregion

        string result = "";
        //
        try
        {
            result = GetTextW32();
            //result = Clipboard.GetText();
        }
        catch (Exception ex)
        {
        }
        return result;
    }

    /// <summary>
    /// Dont use
    /// OpenClipboard exit app without exception
    /// </summary>
    /// <param name="v"></param>
    public void SetText2(string v)
    {
        if (!string.IsNullOrWhiteSpace(v))
        {
            //for (int i = 0; i < 10; i++)
            //{
            //try
            //{
            //ClipboardHelper.SetText(v);

            #region Funguje dobře ale třeba VS má svou schránku. Když jsem vložil zkopírovaný text do VSCode, byl tam
            // Další možnosti:
            // https://github.com/CopyText/TextCopy
            // E:\Documents\vs\Projects\ConsoleNetFw\ConsoleNetFw\Clippy.cs


            for (int i2 = 0; i2 < 10; i2++)
            {
                try
                {
                    W32.OpenClipboard(IntPtr.Zero);
                    break;
                }
                catch { }
                System.Threading.Thread.Sleep(10);
            }

            var ptr = Marshal.StringToHGlobalUni(v);
            W32.SetClipboardData(13, ptr);
            W32.CloseClipboard();
            Marshal.FreeHGlobal(ptr);
            #endregion

            return;
            //}
            //catch { }
            //System.Threading.Thread.Sleep(10);
            //}


        }
    }

    public List<string> GetLines()
    {
        var text = GetText();
        return SH.GetLines(text);
    }
    #endregion

    public void SetList(List<string> d)
    {
        SetLines(d);
    }

    public void SetLines(IEnumerable lines)
    {
        string s = SH.JoinNL(lines);
        SetText(s);
    }

    public void CutFiles(params string[] selected)
    {
        byte[] moveEffect = { 2, 0, 0, 0 };
        MemoryStream dropEffect = new MemoryStream();
        dropEffect.Write(moveEffect, 0, moveEffect.Length);

        var filestToCut = new StringCollection();
        filestToCut.AddRange(selected);

        DataObject data = new DataObject(sess.i18n(XlfKeys.PreferredDropEffect), dropEffect);
        data.SetFileDropList(filestToCut);

        Clipboard.Clear();
        Clipboard.SetDataObject(data, true);
    }


    public void SetText(StringBuilder stringBuilder)
    {
        SetText(stringBuilder.ToString());
    }

    public void SetText3(string s)
    {
        if (clipboardMonitor != null)
        {
            clipboardMonitor.afterSet = null;
        }

        Thread thread = new Thread(() => SetText2(s));
        thread.SetApartmentState(ApartmentState.STA); //Set the thread to STA
        thread.Start();
        thread.Join();
    }



    public bool ContainsText()
    {
        return Clipboard.ContainsText();
    }
}