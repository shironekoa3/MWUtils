using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using static MWUtils.Win32;

namespace MWUtils
{
    public partial class WindowsTools
    {
        [Flags]
        public enum KeyCode
        {
            None = 0,
            // 数字键
            D0 = 0x30,
            D1 = 0x31,
            D2 = 0x32,
            D3 = 0x33,
            D4 = 0x34,
            D5 = 0x35,
            D6 = 0x36,
            D7 = 0x37,
            D8 = 0x38,
            D9 = 0x39,

            // 字母键
            A = 0x41,
            B = 0x42,
            C = 0x43,
            D = 0x44,
            E = 0x45,
            F = 0x46,
            G = 0x47,
            H = 0x48,
            I = 0x49,
            J = 0x4A,
            K = 0x4B,
            L = 0x4C,
            M = 0x4D,
            N = 0x4E,
            O = 0x4F,
            P = 0x50,
            Q = 0x51,
            R = 0x52,
            S = 0x53,
            T = 0x54,
            U = 0x55,
            V = 0x56,
            W = 0x57,
            X = 0x58,
            Y = 0x59,
            Z = 0x5A,

            // 功能键
            Escape = 0x1B,
            Space = 0x20,
            Enter = 0x0D,
            Tab = 0x09,
            Backspace = 0x08,

            // 控制键
            Shift = 0x10,
            Ctrl = 0x11,
            Alt = 0x12,

            // 方向键
            Left = 0x25,
            Up = 0x26,
            Right = 0x27,
            Down = 0x28,

            // 功能键 F1 - F12
            F1 = 0x70,
            F2 = 0x71,
            F3 = 0x72,
            F4 = 0x73,
            F5 = 0x74,
            F6 = 0x75,
            F7 = 0x76,
            F8 = 0x77,
            F9 = 0x78,
            F10 = 0x79,
            F11 = 0x7A,
            F12 = 0x7B
        }
        public enum FuncKeys
        {
            NONE = 0,
            CTRL = 1,
            ALT = 2,
            SHIFT = 4,
            CTRL_ALT = 3,
            CTRL_SHIFT = 5,
            ALT_SHIFT = 6,
            CTRL_ALT_SHIFT = 7
        }
        private struct Hotkey
        {
            public int key;
            public FuncKeys funcKey;
            public HotkeyFunc hotkeyFunc;
        }

        private static List<Hotkey> hotkeyList = new List<Hotkey>();
        private static Thread tHotkey;
        public delegate void HotkeyFunc();
        public static HotkeyFunc hotkeyFunc;
        private static void HotkeyHandle()
        {
            if (hotkeyFunc != null)
            {
                hotkeyFunc();
            }
        }
        private static void HotkeyThread()
        {
            while (true)
            {
                for (int i = 0; i < hotkeyList.Count; i++)
                {
                    int fk = 0;
                    if (IsKeyDown(17))   // CTRL
                        fk += (int)FuncKeys.CTRL;
                    if (IsKeyDown(18))   // ALT
                        fk += (int)FuncKeys.ALT;
                    if (IsKeyDown(16))   // SHIFT
                        fk += (int)FuncKeys.SHIFT;

                    if (fk != (int)hotkeyList[i].funcKey)
                        continue;

                    if (IsKeyDown(hotkeyList[i].key))
                    {
                        while (IsKeyDown(hotkeyList[i].key)) { Thread.Sleep(1); }
                        hotkeyFunc = hotkeyList[i].hotkeyFunc;
                        Thread temp = new Thread(HotkeyHandle);
                        temp.SetApartmentState(ApartmentState.STA);
                        temp.Start();
                    }
                }
                Thread.Sleep(1);
            }
        }

        /// <summary>
        /// 注册热键
        /// </summary>
        /// <param name="func"></param>
        /// <param name="key"></param>
        /// <param name="funcKey"></param>
        public static void RegHotkey(HotkeyFunc func, KeyCode key, FuncKeys funcKey = FuncKeys.NONE)
        {
            Hotkey hk = new Hotkey();
            hk.hotkeyFunc = func;
            hk.key = (int)key;
            hk.funcKey = funcKey;
            hotkeyList.Add(hk);
            if (tHotkey == null)
            {
                tHotkey = new Thread(HotkeyThread);
                tHotkey.IsBackground = true;
                tHotkey.Start();
            }
        }

    }
}
