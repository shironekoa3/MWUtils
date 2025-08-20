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
        /// <summary>
        /// 执行cmd命令
        /// 多命令请使用批处理命令连接符：
        /// <![CDATA[
        /// &:同时执行两个命令
        /// |:将上一个命令的输出,作为下一个命令的输入
        /// &&：当&&前的命令成功时,才执行&&后的命令
        /// ||：当||前的命令失败时,才执行||后的命令]]>
        /// 其他请百度
        /// </summary>
        /// <param name="cmd"></param>
        public static string RunCmd(string cmd)
        {
            string output = "";
            cmd = cmd.Trim().TrimEnd('&') + "&exit";//说明：不管命令是否成功均执行exit命令，否则当调用ReadToEnd()方法时，会处于假死状态
            using (Process p = new Process())
            {
                p.StartInfo.FileName = @"C:\Windows\System32\cmd.exe";
                p.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;  //由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;   //重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;          //不显示程序窗口
                p.Start();//启动程序

                //向cmd窗口写入命令
                p.StandardInput.WriteLine(cmd);
                p.StandardInput.AutoFlush = true;

                //获取cmd窗口的输出信息
                output += p.StandardOutput.ReadToEnd() + "\r\n";
                p.WaitForExit();//等待程序执行完退出进程
                p.Close();
            }

            return output;
        }

        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="recEmail"></param>
        /// <param name="senderEmail"></param>
        /// <param name="senderName"></param>
        /// <param name="emailTitle"></param>
        /// <param name="emailContent"></param>
        /// <param name="emailBoxPwd"></param>
        /// <returns></returns>
        public static bool SendEmail(string recEmail, string senderEmail, string senderName, string emailTitle, string emailContent, string emailBoxPwd)
        {
            try
            {
                MailMessage msg = new MailMessage();

                msg.To.Add(recEmail);//收件人地址

                msg.From = new MailAddress(senderEmail, senderName);//发件人邮箱，名称  

                msg.Subject = emailTitle;//邮件标题  
                msg.SubjectEncoding = Encoding.UTF8;//标题格式为UTF8  

                msg.Body = emailContent;//邮件内容  
                msg.BodyEncoding = Encoding.UTF8;//内容格式为UTF8  

                SmtpClient client = new SmtpClient();

                client.Host = "smtp.qq.com";//SMTP服务器地址  
                client.Port = 587;//SMTP端口，QQ邮箱填写587  

                client.EnableSsl = true;//启用SSL加密  

                client.Credentials = new NetworkCredential(senderEmail, emailBoxPwd);//发件人邮箱账号，密码  

                client.Send(msg);//发送邮件  

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 判断是否为管理员启动
        /// </summary>
        /// <returns></returns>
        public static bool IsAdministrator()
        {
            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }

        public static void RestartWithAdministrator(string ExecutablePath)
        {
            //创建启动对象
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            //设置运行文件
            startInfo.FileName = ExecutablePath;
            //设置启动参数
            //startInfo.Arguments = String.Join(" ", Args);
            //设置启动动作,确保以管理员身份运行
            startInfo.Verb = "runas";
            try
            {
                //如果不是管理员，则启动UAC
                Process.Start(startInfo);
                //退出
                Environment.Exit(0);
            }
            catch { }
        }
       

        /// <summary>
        /// 设置剪贴板文本
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool SetClipboardText(string text)
        {
            if (OpenClipboard(IntPtr.Zero))
            {
                EmptyClipboard();
                SetClipboardData(13, Marshal.StringToHGlobalUni(text));
                CloseClipboard();
                return true;
            }
            return false;
        }

        //internal static string GetText(int format)
        //{
        //    string value = string.Empty;
        //    OpenClipboard(IntPtr.Zero);
        //    if (IsClipboardFormatAvailable(format))
        //    {
        //        IntPtr ptr = NativeMethods.GetClipboardData(format);
        //        if (ptr != IntPtr.Zero)
        //        {
        //            value = Marshal.PtrToStringUni(ptr);
        //        }
        //    }
        //    CloseClipboard();
        //    return value;
        //}

        //*********************************** 网络代理 ************************************************************

        /// <summary>
        /// 设置 Windows IE 代理，传入 IP 地址和端口
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public static bool SetIEAgent(string ip, string port)
        {
            try
            {
                RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
                registry.SetValue("ProxyEnable", 1);
                registry.SetValue("ProxyServer", ip + ":" + port);
                registry.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 关闭 Windows IE 代理设置
        /// </summary>
        /// <returns></returns>
        public static bool CloseIEAgent()
        {
            try
            {
                RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
                registry.SetValue("ProxyEnable", 0);
                registry.SetValue("ProxyServer", "");
                registry.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 判断是否开启 IE 代理
        /// </summary>
        /// <returns></returns>
        public static bool IsIEAgent()
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            int isAgent = (int)registry.GetValue("ProxyEnable");
            registry.Close();
            return isAgent == 1;
        }

       
    }
}
