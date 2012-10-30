using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Core.FileUtility
{
    /// <summary>
    /// 文件读取类
    /// </summary>
    public class MFileUtility
    {
        /// <summary>
        /// 读取Txt文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ReadTxt(string filePath)
        {
            var result = string.Empty;
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                using (var readerStream = new StreamReader(filePath, Encoding.UTF8))
                {
                    result = readerStream.ReadToEnd();
                }
            }
            return result;
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileContent"></param>
        /// <param name="isAppend">是否append</param>
        /// <returns></returns>
        public static bool WriteTxt(string filePath, string fileContent, bool isAppend)
        {
            var result = false;
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                using (var writeStream = new StreamWriter(filePath, isAppend, Encoding.UTF8))
                {
                    writeStream.Write(fileContent);
                }
            }
            return result;
        }
        
    }
}
