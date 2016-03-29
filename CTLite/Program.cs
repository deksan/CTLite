using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;


    
namespace CTLite {
    static class Program {




        private static readonly Win32.LowLevelMouseProc _proc = HookCallback;
        public static IntPtr HookId = IntPtr.Zero;
 
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            HookId = SetHook(_proc);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
           CTLite.Win32.UnhookWindowsHookEx(HookId);

        }

        private static IntPtr SetHook(Win32.LowLevelMouseProc proc) {
            using (Process curProcess = Process.GetCurrentProcess())
                using (ProcessModule curModule = curProcess.MainModule) {
                    return Win32.SetWindowsHookEx(Win32.WH_MOUSE_LL, proc, Win32.GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        public delegate void MouseMove(int x, int y);

        public static MouseMove OnMouseMove;
        private static IntPtr HookCallback( int nCode, IntPtr wParam, IntPtr lParam) {

            if (nCode >= 0 && Win32.MouseMessages.WM_MOUSEMOVE == (Win32.MouseMessages)wParam && OnMouseMove != null ) {
                Win32.MSLLHOOKSTRUCT hookStruct = (Win32.MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(Win32.MSLLHOOKSTRUCT));
                OnMouseMove(hookStruct.pt.x, hookStruct.pt.y);
            }

            return Win32.CallNextHookEx(HookId, nCode, wParam, lParam);

        }



    }
}
