
using System.Runtime.InteropServices;
using System;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;
using System.Security.Cryptography;

namespace MWUtils
{
    public partial class Win32
    {
        private static DD dd = new DD();
        private static string _loadDDPath = "";
        private static int _loadDDResult = 0x100;

        /// <summary>
        /// 
        /// <para>
        /// 使用 DD81200 模拟鼠标键盘。
        /// </para>
        /// 
        /// <para>
        /// 推荐：
        /// <br/>
        /// var res = Win32.UseDD(Application.StartupPath + "\\DD81200x64.64.dll");<br/>
        /// if (res == -3)<br/>
        /// ····MessageBox.Show("DD81200 DLL 未找到");<br/>
        /// else if (res == -2)<br/>
        /// ····MessageBox.Show("DD81200 DLL 装载失败");<br/>
        /// else if (res == -1)<br/>
        /// ····MessageBox.Show("DD81200 DLL 获取函数地址失败");
        /// </para>
        /// 
        /// </summary>
        /// 
        /// <param name="dllfile">DD81200 DLL 文件的路径</param>
        /// 
        /// <returns>
        /// -3: 文件不存在<br/>
        /// -2: 装载失败<br/>
        /// -1: 获取函数地址失败<br/>
        /// ·0: 成功，非增强模块
        /// </returns>
        public static int UseDD(string dllfile)
        {
            _loadDDPath = dllfile;
            Thread t = new Thread(HandleUseDD); 
            t.Start();
            while (_loadDDResult == 0x100)
            {
                Thread.Sleep(1);
            }
            return _loadDDResult;
        }
        private static void HandleUseDD()
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(_loadDDPath);
            if (!fi.Exists)
                _loadDDResult  = -3;
            else
                _loadDDResult = dd.Load(_loadDDPath);
        }
        /// <summary>
        /// 使用 DD 移动鼠标到指定坐标
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public static void DDMoveMouse(int dx, int dy)
        {
            dd.mov(dx, dy);
        }

        /// <summary>
        /// 使用 DD 鼠标单击
        /// </summary>
        public static void DDMouseClick(int interval = 50)
        {
            dd.btn(1);
            Thread.Sleep(interval);
            dd.btn(2);
        }

        /// <summary>
        /// 使用 DD 鼠标移动到指定坐标并单击
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public static void DDMouseMoveClick(int dx, int dy)
        {
            DDMoveMouse(dx, dy);
            DDMouseClick();
        }

        /// <summary>
        /// 使用 DD 模拟按下指定按键
        /// </summary>
        /// <param name="keyCode"></param>
        public static void DDKeyPush(int keyCode)
        {
            dd.key(dd.todc(keyCode), 1);
            dd.key(dd.todc(keyCode), 2);
        }

        public static int DDKeyGetDDCode(int keyCode)
        {
            return dd.todc(keyCode);
        }


        /// <summary>
        /// 使用 DD 模拟按下 CTRL+A
        /// </summary>
        /// <param name="keyCode"></param>
        public static void DDKeyCtrlA(int interval = 50)
        {
            dd.key(600, 1);
            Thread.Sleep(interval);
            dd.key(dd.todc(65), 1);
            Thread.Sleep(interval);
            dd.key(dd.todc(65), 2);
            Thread.Sleep(interval);
            dd.key(600, 2);
        }

        /// <summary>
        /// 使用 DD 模拟按下 CTRL+C
        /// </summary>
        /// <param name="keyCode"></param>
        public static void DDKeyCtrlC(int interval = 50)
        {
            dd.key(600, 1);
            Thread.Sleep(interval);
            dd.key(dd.todc(67), 1);
            Thread.Sleep(interval);
            dd.key(dd.todc(67), 2);
            Thread.Sleep(interval);
            dd.key(600, 2);
        }

        /// <summary>
        /// 使用 DD 模拟按下 CTRL+V
        /// </summary>
        /// <param name="keyCode"></param>
        public static void DDKeyCtrlV(int interval = 50)
        {
            dd.key(600, 1);
            Thread.Sleep(interval);
            dd.key(dd.todc(86), 1);
            Thread.Sleep(interval);
            dd.key(dd.todc(86), 2);
            Thread.Sleep(interval);
            dd.key(600, 2);
        }

        /// <summary>
        /// 使用 DD 模拟键盘依次输出字符
        /// </summary>
        /// <param name="keyCode"></param>
        public static void DDOutString(string str)
        {
            dd.str(str);
        }

    }
}
