using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Tools
{
    /// <summary>
    /// 转换
    /// </summary>
    public class RdfConvert
    {
        /// <summary>
        /// 字符串转base64
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StringToBase64(string str)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }
        /// <summary>
        /// base64转字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Base64ToString(string str)
        {
            byte[] bytes = Convert.FromBase64String(str);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
        /// <summary>
        /// 流转字节数组
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(bytes, 0, bytes.Length);
            return bytes;
        }
        /// <summary>
        /// 字节数组转流
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Stream BytesToStream(byte[] bytes)
        {
            return new MemoryStream(bytes);
        }
        /// <summary>
        /// 字节数组转文件
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="fileName"></param>
        public static void BytesToFile(byte[] bytes, string fileName)
        {
            StreamToFile(BytesToStream(bytes), fileName);
        }
        /// <summary>
        /// 流转文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        public static void StreamToFile(Stream stream, string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(StreamToBytes(stream));
            bw.Close();
            fs.Close();
        }
        /// <summary>
        /// 文件转流
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Stream FileToStream(string fileName)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            return BytesToStream(bytes);
        }
        /// <summary>
        /// 文件转字节数组
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static byte[] FileToBytes(string fileName)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            return bytes;
        }
        /// <summary>
        /// 位图文件转string
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static string BitmapToString(Bitmap bitmap)
        {
            return Convert.ToBase64String(BitmapToByte(bitmap));
        }
        /// <summary>
        /// 位图文件转字节数组
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static byte[] BitmapToByte(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Jpeg);
                byte[] data = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(data, 0, Convert.ToInt32(stream.Length));
                return data;
            }
        }
        /// <summary>
        /// MD5加密 32位
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Md5_32(string text)
        {
            byte[] result = Encoding.Default.GetBytes(text);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");
        }
        /// <summary>
        ///MD5加密 16位
        /// </summary>
        /// <param name="ConvertString"></param>
        /// <returns></returns>
        public static string Md5_16(string ConvertString)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            string str = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
            return str.Replace("-", "");
        }
        /// <summary>
        /// sha1加密
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string SHA1(string content)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_in = Encoding.UTF8.GetBytes(content);
            byte[] bytes_out = sha1.ComputeHash(bytes_in);
            sha1.Dispose();
            return BitConverter.ToString(bytes_out);
        }
        /// <summary>
        /// 3DES解密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DESDecrypt(string data, string key)
        {
            if (key.Length != 24)
                throw new Exception("秘钥长度只能是24位!");
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider()
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7
            };
            ICryptoTransform DESDecrypt = DES.CreateDecryptor();
            byte[] Buffer = Convert.FromBase64String(data);
            return Encoding.UTF8.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

        /// <summary>
        /// 3DES加密 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DESEncrypt(string data, string key)
        {
            if (key.Length != 24)
                throw new Exception("秘钥长度只能是24位!");
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider()
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB
            };
            ICryptoTransform DESEncrypt = DES.CreateEncryptor();
            byte[] Buffer = Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }
        public static int ToAscii(string str)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            return encoding.GetBytes(str)[0];
        }

        public static string ToChar(int num)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            return encoding.GetString(new[] { (byte)num });
        }
        public static string Get36Hex(int number)
        {
            int rate = Convert.ToInt32(RdfMath.Floor(number / 36, 0));
            int los = number % 36;
            string result = "";
            if (los > 9)
                result = ToChar(los + 55);
            else
                result = ToChar(los + 48);
            while (rate >= 36)
            {
                los = rate % 36;
                rate = Convert.ToInt32(RdfMath.Floor(rate / 36, 0));
                if (los > 9)
                    result = ToChar(los + 55) + result;
                else
                    result = ToChar(los + 48) + result;
            }
            if (rate > 0)
            {
                if (rate > 9)
                    result = ToChar(rate + 55) + result;
                else
                    result = ToChar(rate + 48) + result;
            }
            return result;
        }
        public static int Get36HexTo10Hex(string code)
        {
            int result = 0, index = code.Length, rate = 1;
            while (index > 0)
            {
                string str = code.Substring(index - 1, 1);
                int num = ToAscii(str);
                if (num >= 48 && num <= 57)
                    num = num - 48;
                else if (num >= 65 && num <= 90)
                    num = num - 55;
                else
                    num = 0;
                result = result + num * rate;
                index = index - 1;
                rate = rate * 36;
            }
            return result;
        }

        /// <summary>
        /// 读取pdf文件的总页数
        /// </summary>
        /// <param name="pdf_filename">pdf文件</param>
        /// <returns></returns>
        public static RdfMsg GetPageCountByPDF(string pdf_filename)
        {
            RdfMsg msg = new RdfMsg(false);
            int pageCount = 0;
            if (File.Exists(pdf_filename))
            {
                try
                {
                    byte[] buffer = File.ReadAllBytes(pdf_filename);
                    if (buffer != null && buffer.Length > 0)
                    {
                        pageCount = -1;
                        string pdfText = Encoding.Default.GetString(buffer);
                        Regex regex = new Regex(@"/Type\s*/Page[^s]");
                        MatchCollection conllection = regex.Matches(pdfText);
                        pageCount = conllection.Count;
                    }
                    msg.Success = true;
                }
                catch (Exception ex)
                {
                    RdfLog.WriteException(ex, "读取pdf文件的总页数异常!");
                    msg.Error = ex.Message;
                }
            }
            msg.Result = pageCount;
            return msg;
        }
        /// <summary>
        /// PFD转换器位置
        /// </summary>
        private static string _EXEFILENAME = System.IO.Path.Combine(RdfFile.AppDir + "\\pdf2swf\\pdf2swf.exe");
        /// <summary>
        /// 转换PDF文件为SWF格式
        /// </summary>
        /// <param name="pdfPath">PDF文件路径</param>
        /// <param name="swfPath">SWF生成目标文件路径</param>
        /// <param name="page">PDF页数</param>
        /// <returns>生成是否成功</returns>
        public static RdfMsg PDFConvertToSwf(string pdfPath, string swfPath, int page)
        {
            RdfMsg msg = new RdfMsg(false);
            StringBuilder sb = new StringBuilder();
            sb.Append(" \"" + pdfPath + "\"");
            sb.Append(" -o \"" + swfPath + "\"");
            sb.Append(" -z");
            //flash version
            sb.Append(" -s flashversion=9");
            //禁止PDF里面的链接
            sb.Append(" -s disablelinks");
            //PDF页数
            sb.Append(" -p " + "\"1" + "-" + page + "\"");
            //SWF中的图片质量
            sb.Append(" -j 100");
            string command = sb.ToString();
            System.Diagnostics.Process p = null;
            try
            {
                using (p = new System.Diagnostics.Process())
                {
                    p.StartInfo.FileName = _EXEFILENAME;
                    p.StartInfo.Arguments = command;
                    p.StartInfo.WorkingDirectory = Path.GetDirectoryName(_EXEFILENAME);
                    //不使用操作系统外壳程序 启动 线程
                    p.StartInfo.UseShellExecute = false;
                    //p.StartInfo.RedirectStandardInput = true;
                    //p.StartInfo.RedirectStandardOutput = true;
                    //把外部程序错误输出写到StandardError流中(pdf2swf.exe的所有输出信息,都为错误输出流,用 StandardOutput是捕获不到任何消息的...
                    p.StartInfo.RedirectStandardError = true;
                    //不创建进程窗口
                    p.StartInfo.CreateNoWindow = false;
                    //启动进程
                    p.Start();
                    //开始异步读取
                    p.BeginErrorReadLine();
                    //等待完成
                    p.WaitForExit();
                }
                msg.Success = true;
            }
            catch (Exception ex)
            {
                RdfLog.WriteException(ex, "读取pdf文件的总页数异常!");
                msg.Error = ex.Message;
            }
            finally
            {
                if (p != null)
                {
                    //关闭进程
                    p.Close();
                    //释放资源
                    p.Dispose();
                }
            }
            return msg;
        }
    }
}
//十进制转二进制
//Console.WriteLine(Convert.ToString(69, 2));
//十进制转八进制
//Console.WriteLine(Convert.ToString(69, 8));
//十进制转十六进制
//Console.WriteLine(Convert.ToString(69, 16));
//二进制转十进制
//Console.WriteLine(Convert.ToInt32("100111101", 2));
//八进制转十进制
//Console.WriteLine(Convert.ToInt32("76", 8));
//16进制转换10进制
//Console.WriteLine(Convert.ToInt32("FF", 16));
