using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Core.Enums;
using System.Text.RegularExpressions;
using System.Threading;

namespace Core
{
    /// <summary>
    /// 加密 类
    /// </summary>
    public class MEncryptUtility
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="strEncrypt">加密字符串</param>
        /// <param name="number">固定入参16,密码截取前16位</param>
        /// <returns>密文</returns>
        public static string MD5Encrypt(string strEncrypt, int number)
        {
            byte[] buffer1 = Encoding.Default.GetBytes(strEncrypt);
            buffer1 = new MD5CryptoServiceProvider().ComputeHash(buffer1);
            var text1 = "";
            for (var num1 = 0; num1 < buffer1.Length; num1++)
            {
                text1 = text1 + buffer1[num1].ToString("x").PadLeft(2, '0');
            }
            if (number == 0x10)
            {
                return text1.Substring(8, 0x10);
            }
            return text1;
        }

        /// <summary>
        /// 创建一个Guid字符串
        /// </summary>
        /// <returns></returns>
        public static string NewGuid()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 创建随机字符串
        /// </summary>
        /// <param name="len"></param>
        /// <param name="randomType"></param>
        /// <returns></returns>
        public static string NewRandomStr(int len, MRandomType randomType)
        {
            var result = new StringBuilder(len);
            var charLowerArray = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            var charUpperArray = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            var numArray = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            var otherArray = new char[] { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '{', '}', '[', ']', '|', '?', '<', '>' };

            var seeds = new List<char>();

            if ((randomType & MRandomType.LowerCarh) != 0)
                seeds.AddRange(charLowerArray.ToList<char>());
            if ((randomType & MRandomType.UpperChar) != 0)
                seeds.AddRange(charUpperArray.ToList<char>());
            if ((randomType & MRandomType.Num) != 0)
                seeds.AddRange(numArray.ToList<char>());
            if ((randomType & MRandomType.Other) != 0)
                seeds.AddRange(otherArray.ToList<char>());

            Thread.Sleep(1);
            var random = new Random(unchecked((int)DateTime.Now.Ticks));
            for (var i = 0; i < len; i++)
            {
                result.Append(seeds[random.Next(0, seeds.Count - 1)]);
            }

            return result.ToString();
        }
    }
}
