using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Tools
{
    /// <summary>
    /// 文件类
    /// </summary>
    public class RdfFile
    {
        public static string AppDir = AppDomain.CurrentDomain.BaseDirectory;
        // 编写程序的时候，经常需要用的项目根目录。自己总结如下
        // 1、取得控制台应用程序的根目录方法
        //     方法1、Environment.CurrentDirectory 取得或设置当前工作目录的完整限定路径
        //     方法2、AppDomain.CurrentDomain.BaseDirectory 获取基目录，它由程序集冲突解决程序用来探测程序集
        // 2、取得Web应用程序的根目录方法
        //     方法1、HttpRuntime.AppDomainAppPath.ToString();//获取承载在当前应用程序域中的应用程序的应用程序目录的物理驱动器路径。用于App_Data中获取
        //     方法2、Server.MapPath("") 或者 Server.MapPath("~/");//返回与Web服务器上的指定的虚拟路径相对的物理文件路径
        //     方法3、Request.ApplicationPath;//获取服务器上ASP.NET应用程序的虚拟应用程序根目录
        // 3、取得WinForm应用程序的根目录方法
        //     1、Environment.CurrentDirectory.ToString();//获取或设置当前工作目录的完全限定路径
        //     2、Application.StartupPath.ToString();//获取启动了应用程序的可执行文件的路径，不包括可执行文件的名称
        //     3、Directory.GetCurrentDirectory();//获取应用程序的当前工作目录
        //     4、AppDomain.CurrentDomain.BaseDirectory;//获取基目录，它由程序集冲突解决程序用来探测程序集
        //     5、AppDomain.CurrentDomain.SetupInformation.ApplicationBase;//获取或设置包含该应用程序的目录的名称
        //其中：以下两个方法可以获取执行文件名称
        //     1、Process.GetCurrentProcess().MainModule.FileName;//可获得当前执行的exe的文件名。
        //     2、Application.ExecutablePath;//获取启动了应用程序的可执行文件的路径，包括可执行文件的名称
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="path">文件夹路径</param>
        public static void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="path">路径</param>
        public static void CreateFile(string fileName, string path)
        {
            CreateFolder(path);

            if (!File.Exists(path + "\\" + fileName))
            {
                FileStream vFs = File.Create(path + "\\" + fileName);
                vFs.Dispose();
                vFs.Close();
            }
        }
        /// <summary>
        /// 获取目录下所有的文件
        /// </summary>
        /// <param name="path"></param>
        public static string[] GetFiles(string path)
        {
            if (!Directory.Exists(path))
                return new string[] { };
            return Directory.GetFiles(path);
        }
        /// <summary>
        /// 获取目录下所有的文件夹
        /// </summary>
        /// <param name="path"></param>
        public static string[] GetDirectories(string path)
        {
            if (!Directory.Exists(path))
                return new string[] { };
            return Directory.GetDirectories(path);
        }
        /// <summary>
        /// 获取目录下所有的文件夹和文件
        /// </summary>
        /// <param name="path"></param>
        public static List<string> GetFileAll(string path)
        {
            List<string> list = new List<string>();
            list.AddRange(GetFiles(path).AsEnumerable());
            list.AddRange(GetDirectories(path).AsEnumerable());
            return list;
        }
        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static FileInfo GetFileInfo(string path)
        {
            FileInfo info = new FileInfo(path);
            return info;
        }
        /// <summary>
        /// 读取所有
        /// </summary>
        /// <param name="url">路径</param>
        /// <returns></returns>
        public static string ReadAll(string url)
        {
            string vStr = "";
            if (File.Exists(url))
            {
                using (FileStream vfs = new FileStream(url, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    StreamReader vSr = new StreamReader(vfs);
                    vStr = vSr.ReadToEnd();
                    vSr.Dispose();
                    vSr.Close();
                }
            }
            return vStr;
        }
        /// <summary>
        /// 读取所有行
        /// </summary>
        /// <param name="url">路径</param>
        /// <returns></returns>
        public static List<string> ReadLine(string url)
        {
            List<string> list = new List<string>();
            if (File.Exists(url))
            {
                using (FileStream vfs = new FileStream(url, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    StreamReader vSr = new StreamReader(vfs);
                    string str;
                    while ((str = vSr.ReadLine()) != null)
                    {
                        list.Add(str);
                    }
                    vSr.Dispose();
                    vSr.Close();
                }
            }
            return list;
        }
        /// <summary>
        /// 写入覆盖
        /// </summary>
        /// <param name="fileName">文件</param>
        /// <param name="path">路径</param>
        /// <param name="content">内容</param>
        public static void Write(string fileName, string path, string content)
        {
            CreateFile(fileName, path);
            using (FileStream vFs = new FileStream(path + "\\" + fileName, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                StreamWriter vSw = new StreamWriter(vFs);
                vSw.Write(content);
                vSw.Dispose();
                vSw.Close();
            }
        }
        /// <summary>
        /// 追加写入
        /// </summary>
        /// <param name="fileName">文件</param>
        /// <param name="path">路径</param>
        /// <param name="content">内容</param>
        public static void WriteAppend(string fileName, string path, string content)
        {
            CreateFile(fileName, path);
            StreamWriter sw = File.AppendText(path + "\\" + fileName);
            sw.Write(content);
            sw.Close();
        }

        #region 获取系统图标
        [DllImport("Shell32.dll")]
        static extern int SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, uint uFlags);
        struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            public char szDisplayName;
            public char szTypeName;
        }
        /// <summary>
        /// 从文件扩展名得到文件关联图标
        /// </summary>
        /// <param name="fileName">文件名或文件扩展名</param>
        /// <param name="smallIcon">true小图标，false大图标</param>
        /// <returns>图标</returns>
        public static Icon GetFileIcon(string fileName, bool smallIcon = false)
        {
            SHFILEINFO fi = new SHFILEINFO();
            Icon ic = null;
            int iTotal = (int)SHGetFileInfo(fileName, 100, ref fi, 0, (uint)(smallIcon ? 273 : 272));
            if (iTotal > 0)
            {
                ic = Icon.FromHandle(fi.hIcon);
            }
            return ic;
        }
        public static void SavePng(Bitmap bitmap, string path)
        {
            bitmap.MakeTransparent(Color.Transparent);
            bitmap.Save(path, ImageFormat.Png);
        }
        #endregion
    }
}
