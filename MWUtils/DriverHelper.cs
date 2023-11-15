using System;
using System.IO;
using System.Runtime.InteropServices;

namespace MWUtils
{
    public class DriverHelper
    {

        #region DriverEntity

        // Service Types (Bit Mask)
        public static readonly UInt32 SERVICE_KERNEL_DRIVER = 0x00000001;
        public static readonly UInt32 SERVICE_FILE_SYSTEM_DRIVER = 0x00000002;
        public static readonly UInt32 SERVICE_ADAPTER = 0x00000004;
        public static readonly UInt32 SERVICE_RECOGNIZER_DRIVER = 0x00000008;
        public static readonly UInt32 SERVICE_WIN32_OWN_PROCESS = 0x00000010;
        public static readonly UInt32 SERVICE_WIN32_SHARE_PROCESS = 0x00000020;
        public static readonly UInt32 SERVICE_INTERACTIVE_PROCESS = 0x00000100;
        public static readonly UInt32 SERVICE_WIN32 =
            SERVICE_WIN32_OWN_PROCESS | SERVICE_WIN32_SHARE_PROCESS;
        public static readonly UInt32 SERVICE_DRIVER =
            SERVICE_KERNEL_DRIVER | SERVICE_FILE_SYSTEM_DRIVER | SERVICE_RECOGNIZER_DRIVER;
        public static readonly UInt32 SERVICE_TYPE_ALL =
            SERVICE_WIN32 | SERVICE_ADAPTER | SERVICE_DRIVER | SERVICE_INTERACTIVE_PROCESS;

        // Start Type
        public static readonly UInt32 SERVICE_BOOT_START = 0x00000000;
        public static readonly UInt32 SERVICE_SYSTEM_START = 0x00000001;
        public static readonly UInt32 SERVICE_AUTO_START = 0x00000002;
        public static readonly UInt32 SERVICE_DEMAND_START = 0x00000003;
        public static readonly UInt32 SERVICE_DISABLED = 0x00000004;

        // Error control type
        public static readonly UInt32 SERVICE_ERROR_IGNORE = 0x00000000;
        public static readonly UInt32 SERVICE_ERROR_NORMAL = 0x00000001;
        public static readonly UInt32 SERVICE_ERROR_SEVERE = 0x00000002;
        public static readonly UInt32 SERVICE_ERROR_CRITICAL = 0x00000003;

        // Controls
        public static readonly UInt32 SERVICE_CONTROL_STOP = 0x00000001;
        public static readonly UInt32 SERVICE_CONTROL_PAUSE = 0x00000002;
        public static readonly UInt32 SERVICE_CONTROL_CONTINUE = 0x00000003;
        public static readonly UInt32 SERVICE_CONTROL_INTERROGATE = 0x00000004;
        public static readonly UInt32 SERVICE_CONTROL_SHUTDOWN = 0x00000005;

        // ServiceStatus
        public static readonly UInt32 SERVICE_CONTINUE_PENDING = 0x00000005;
        public static readonly UInt32 SERVICE_PAUSE_PENDING = 0x00000006;
        public static readonly UInt32 SERVICE_PAUSED = 0x00000007;
        public static readonly UInt32 SERVICE_RUNNING = 0x00000004;
        public static readonly UInt32 SERVICE_START_PENDING = 0x00000002;
        public static readonly UInt32 SERVICE_STOP_PENDING = 0x00000003;
        public static readonly UInt32 SERVICE_STOPPED = 0x00000001;

        public static readonly UInt32 SERVICE_ALL_ACCESS = 0xF01FF;

        // Service Control Manager object specific access types
        public static readonly UInt32 SC_MANAGER_ALL_ACCESS = 0xF003F;
        public static readonly UInt32 SC_MANAGER_CREATE_SERVICE = 0x0002;
        public static readonly UInt32 SC_MANAGER_CONNECT = 0x0001;
        public static readonly UInt32 SC_MANAGER_ENUMERATE_SERVICE = 0x0004;
        public static readonly UInt32 SC_MANAGER_LOCK = 0x0008;
        public static readonly UInt32 SC_MANAGER_MODIFY_BOOT_CONFIG = 0x0020;
        public static readonly UInt32 SC_MANAGER_QUERY_LOCK_STATUS = 0x0010;



        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SECURITY_ATTRIBUTES
        {
            public UInt32 nLength;
            public IntPtr lpSecurityDescriptor;
            public bool bInheritHandle;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct OVERLAPPED
        {
            public UInt32 Internal;
            public UInt32 InternalHigh;
            public UInt32 Offset;
            public UInt32 OffsetHigh;
            public IntPtr hEvent;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SERVICE_STATUS
        {
            public UInt32 dwServiceType;
            public UInt32 dwCurrentState;
            public UInt32 dwControlsAccepted;
            public UInt32 dwWin32ExitCode;
            public UInt32 dwServiceSpecificExitCode;
            public UInt32 dwCheckPoint;
            public UInt32 dwWaitHint;
        }

        #endregion

        #region DriverApi

        [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr OpenSCManager(
            String lpMachineName,
            String lpDatabaseName,
            UInt32 dwDesiredAccess
            );

        [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CreateService(
            IntPtr hSCManager,
            String lpServiceName,
            String lpDisplayName,
            UInt32 dwDesiredAccess,
            UInt32 dwServiceType,
            UInt32 dwStartType,
            UInt32 dwErrorControl,
            String lpBinaryPathName,
            String lpLoadOrderGroup,
            UInt32 lpdwTagId,
            String lpDependencies,
            String lpServiceStartName,
            String lpPassword
            );

        [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseServiceHandle(
            IntPtr hSCObject
            );

        [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
        public static extern bool StartService(
            IntPtr hService,
            UInt32 dwNumServiceArgs,
            String lpServiceArgVectors
            );

        [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr OpenService(
            IntPtr hSCManager,
            String lpServiceName,
            UInt32 dwDesiredAccess
            );

        [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
        public static extern bool DeleteService(
            IntPtr hService
            );

        [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
        public static extern bool ControlService(
            IntPtr hService,
            UInt32 dwControl,
            ref SERVICE_STATUS lpServiceStatus
            );

        [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
        public static extern bool QueryServiceStatus(
            IntPtr hService,
            ref SERVICE_STATUS lpServiceStatus
            );
        #endregion

        /// <summary>
        /// 安装驱动
        /// </summary>
        /// <param name="drvPath"></param>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        private static bool DriverInstall(string drvPath, string serviceName)
        {
            bool result = false;

            // 打开服务控制管理器数据库
            IntPtr schSCManager = OpenSCManager(
                null,                   // 目标计算机的名称,NULL：连接本地计算机上的服务控制管理器
                null,                   // 服务控制管理器数据库的名称，NULL：打开 SERVICES_ACTIVE_DATABASE 数据库
                SC_MANAGER_ALL_ACCESS   // 所有权限
            );

            if (schSCManager != IntPtr.Zero)
            {
                // 创建服务对象，添加至服务控制管理器数据库
                IntPtr schService = CreateService(
                    schSCManager,               // 服务控件管理器数据库的句柄
                    serviceName,                // 要安装的服务的名称
                    serviceName,                // 用户界面程序用来标识服务的显示名称
                    SERVICE_ALL_ACCESS,         // 对服务的访问权限：所有全权限
                    SERVICE_KERNEL_DRIVER,      // 服务类型：驱动服务
                    SERVICE_DEMAND_START,       // 服务启动选项：进程调用 StartService 时启动
                    SERVICE_ERROR_IGNORE,       // 如果无法启动：忽略错误继续运行
                    drvPath,                    // 驱动文件绝对路径，如果包含空格需要多加双引号
                    null,                       // 服务所属的负载订购组：服务不属于某个组
                    0,                          // 接收订购组唯一标记值：不接收
                    null,                       // 服务加载顺序数组：服务没有依赖项
                    null,                       // 运行服务的账户名：使用 LocalSystem 账户
                    null                        // LocalSystem 账户密码
                );

                if (schService != IntPtr.Zero)
                {
                    result = true;
                    CloseServiceHandle(schService);
                }

                CloseServiceHandle(schSCManager);
            }
            return result;
        }

        /// <summary>
        /// 启动驱动
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        private static bool DriverStart(string serviceName)
        {
            bool result = false;

            // 打开服务控制管理器数据库
            IntPtr schSCManager = OpenSCManager(null, null, SC_MANAGER_ALL_ACCESS);

            if (schSCManager != IntPtr.Zero)
            {
                // 打开服务
                IntPtr schDriverService = OpenService(schSCManager, serviceName, SERVICE_ALL_ACCESS);

                if (schDriverService != IntPtr.Zero)
                {
                    result = StartService(schDriverService, 0, null);
                    CloseServiceHandle(schDriverService);
                }

                CloseServiceHandle(schSCManager);
            }

            return result;
        }

        /// <summary>
        /// 停止驱动
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        private static bool DriverStop(String serviceName)
        {
            bool result = false;

            // 打开服务控制管理器数据库
            IntPtr schSCManager = OpenSCManager(null, null, SC_MANAGER_ALL_ACCESS);
            if (schSCManager != IntPtr.Zero)
            {
                // 打开服务
                IntPtr schDriverService = OpenService(schSCManager, serviceName, SERVICE_ALL_ACCESS);

                if (schDriverService != IntPtr.Zero)
                {
                    SERVICE_STATUS serviceStatus = new SERVICE_STATUS();
                    if (QueryServiceStatus(schDriverService, ref serviceStatus))
                    {
                        if (serviceStatus.dwCurrentState != SERVICE_STOPPED && serviceStatus.dwCurrentState != SERVICE_STOP_PENDING)
                        {
                            result = ControlService(schDriverService, SERVICE_CONTROL_STOP, ref serviceStatus);
                        }
                    }
                    CloseServiceHandle(schDriverService);
                }
                CloseServiceHandle(schSCManager);
            }
            return result;
            // 判断超时
            //int timeOut = 0;
            //while (serviceStatus.dwCurrentState != SERVICE_STOPPED)
            //{
            //    timeOut++;
            //    QueryServiceStatus(schDriverService, ref serviceStatus);
            //    Thread.Sleep(50);
            //}

            //if (timeOut > 80)
            //{
            //    CloseServiceHandle(schDriverService);
            //    CloseServiceHandle(schSCManager);
            //    return false;
            //}

        }

        /// <summary>
        /// 卸载驱动
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        private static bool DriverUnInstall(String serviceName)
        {
            bool result = false;

            // 打开服务控制管理器数据库
            IntPtr schSCManager = OpenSCManager(null, null, SC_MANAGER_ALL_ACCESS);
            if (schSCManager != IntPtr.Zero)
            {
                // 打开服务
                IntPtr schDriverService = OpenService(schSCManager, serviceName, SERVICE_ALL_ACCESS);

                if (schDriverService != IntPtr.Zero)
                {
                    result = DeleteService(schDriverService);
                    CloseServiceHandle(schDriverService);
                }
                CloseServiceHandle(schSCManager);
            }
            return result;
        }


        static string driverName = "GExtra";
        static string driverPath = "GExtra.sys";

        // 安装并启动驱动
        public static bool LoadDriver()
        {
            if (!new FileInfo(driverPath).Exists)
                return false;

            driverPath = new FileInfo(driverPath).FullName;

            string svcDriverName = driverName + "_Server";

            // 安装驱动
            DriverStop(svcDriverName);
            DriverUnInstall(svcDriverName);
            if (!DriverInstall(driverPath, svcDriverName))
            {
                return false;
            }

            // 启动服务
            if (!DriverStart(svcDriverName))
            {
                DriverUnInstall(svcDriverName);
                return false;
            }

            return true;
        }

        public static bool UnLoadDriver()
        {
            if (!new FileInfo(driverPath).Exists)
            {
                return false;
            }

            string svcDriverName = driverName + "_Server";

            // 停止驱动
            if (!DriverStop(svcDriverName))
            {
                //MessageBox.Show("DriverStop");
                return false;
            }

            // 卸载驱动
            if (!DriverUnInstall(svcDriverName))
            {
                //MessageBox.Show("DriverUnInstall");
                return false;
            }

            return true;
        }
    }
}
