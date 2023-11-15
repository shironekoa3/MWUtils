
using System.Runtime.InteropServices;
using System;

namespace MWUtils
{
    public partial class Win32
    {

        /// <summary>
        /// 移动鼠标到指定坐标
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public static void MouseMove(int dx, int dy)
        {
            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, dx * 65536 / 1920, dy * 65536 / 1080, 0, 0);
        }

        /// <summary>
        /// 鼠标单击
        /// </summary>
        public static void MouseClick()
        {
            INPUT[] inputs = new INPUT[2];

            // 模拟鼠标左键按下
            inputs[0].type = INPUT_MOUSE;
            inputs[0].U.mi.dwFlags = (uint)MOUSEEVENTF_LEFTDOWN;

            // 模拟鼠标左键释放
            inputs[1].type = INPUT_MOUSE;
            inputs[1].U.mi.dwFlags = (uint)MOUSEEVENTF_LEFTUP;

            SendInput(2, inputs, Marshal.SizeOf(typeof(INPUT)));

            //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        /// <summary>
        /// 鼠标移动到指定坐标并单击
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public static void MouseMoveClick(int dx, int dy)
        {
            SetCursorPos(dx, dy);
            MouseClick();
        }

        /// <summary>
        /// 判断指定按键是否按下
        /// </summary>
        /// <param name="nVirtKey"></param>
        /// <returns></returns>
        public static bool IsKeyDown(int nVirtKey)
        {
            return GetAsyncKeyState(nVirtKey) < 0;
        }

        /// <summary>
        /// 模拟按下指定按键
        /// </summary>
        /// <param name="keyCode"></param>
        public static void KeyPush(int keyCode)
        {
            keybd_event(keyCode, 0, 1, 0);
            keybd_event(keyCode, 0, 3, 0);
        }

        /// <summary>
        /// 模拟粘贴（模拟按下 CTRL-C CTRL-V。）
        /// </summary>
        public static void Paste()
        {
            keybd_event(VK_LCONTROL, 0, 1, 0);
            keybd_event(86, 0, 1, 0);
            keybd_event(86, 0, 3, 0);
            keybd_event(VK_LCONTROL, 0, 3, 0);
        }
    }
}
