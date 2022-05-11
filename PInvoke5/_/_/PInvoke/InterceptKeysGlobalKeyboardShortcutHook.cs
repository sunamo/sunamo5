using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class InterceptKeysGlobalKeyboardShortcutHook : W32Base
{
    private const int WH_KEYBOARD_LL = 13;
    private const int WM_KEYDOWN = 0x0100;
    private static LowLevelKeyboardProc _proc = HookCallback;
    private static IntPtr _hookID = IntPtr.Zero;

    public static void Init()
    {
        _hookID = SetHook(_proc);
        //Application.Run();
        W32.UnhookWindowsHookEx(_hookID);
    }

    private static IntPtr SetHook(LowLevelKeyboardProc proc)
    {
        using (Process curProcess = Process.GetCurrentProcess())
        using (ProcessModule curModule = curProcess.MainModule)
        {
            return W32.SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                W32.GetModuleHandle(curModule.ModuleName), 0);
        }
    }

    public static event Action<Keys> KeyPress;

    private static IntPtr HookCallback(
        int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
        {
            int vkCode = Marshal.ReadInt32(lParam);
            var k = (Keys)vkCode;
            KeyPress(k);
            //CL.WriteLine();
        }
        return W32.CallNextHookEx(_hookID, nCode, wParam, lParam);
    }
}