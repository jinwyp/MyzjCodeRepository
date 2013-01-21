using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Core.Enums;

namespace Core.Secure
{
    /// <summary>
    ///   验证码选项MCaptcha
    /// </summary>
    public class MCaptchaOptions
    {
        private int _fontSize = 13;
        private int _height = 35;
        private string _verifyCode;
        private int _verifyCodeLen = 4;
        private int _width = 120;

        /// <summary>
        ///   宽度
        /// </summary>
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        ///   高度
        /// </summary>
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        /// <summary>
        ///   字符长度
        /// </summary>
        public int VerifyCodeLen
        {
            get
            {
                return _verifyCodeLen;
            }
            set { _verifyCodeLen = value; }
        }

        /// <summary>
        ///   字体大小
        /// </summary>
        public int FontSize
        {
            get { return _fontSize; }
            set { _fontSize = value; }
        }

        /// <summary>
        ///   需要生成的验证码
        /// </summary>
        public string VerifyCode
        {
            get { return _verifyCode; }
            set { _verifyCode = value; }
        }
    }

    /// <summary>
    ///   验证码生成
    /// </summary>
    public class MCaptcha
    {
        /// <summary>
        ///   构造函数
        /// </summary>
        /// <param name="captchaOptions"> </param>
        public MCaptcha(MCaptchaOptions captchaOptions)
        {
            if (string.IsNullOrEmpty(captchaOptions.VerifyCode) && captchaOptions.VerifyCodeLen > 0)
                captchaOptions.VerifyCode = MEncryptUtility.NewRandomStr(captchaOptions.VerifyCodeLen, MRandomType.Num | MRandomType.LowerCarh);
            else if (string.IsNullOrEmpty(captchaOptions.VerifyCode))
                captchaOptions.VerifyCodeLen = captchaOptions.VerifyCode.Length;

            Options = captchaOptions;
        }

        /// <summary>
        ///   生成选项
        /// </summary>
        public MCaptchaOptions Options { get; set; }

        private MemoryStream _imageStream = new MemoryStream();
        /// <summary>
        /// 图片内存流
        /// </summary>
        public MemoryStream ImageStream
        {
            get { return _imageStream; }
            set { _imageStream = value; }
        }

        /// <summary>
        /// 生成图片
        /// </summary>
        /// <returns> Image </returns>
        public MemoryStream Gengrate() //返回 Image
        {
            //声明一个位图对象
            Bitmap bitMap = null;
            //声明一个绘图画面
            Graphics gph = null;

            //由给定的需要生成字符串中字符个数 CharNum， 图片宽度 Width 和高度 Height 确定字号 FontSize，
            //确保不因字号过大而不能全部显示在图片上
            var fontWidth = (double)Options.Width / (Options.VerifyCodeLen + 2);
            var fontHeight = (double)Options.Height / 0.5;

            //字号取二者中小者，以确保所有字符能够显示，并且字符的下半部分也能显示
            Options.FontSize = (int)Math.Min(fontWidth, fontHeight);

            //创建位图对象
            bitMap = new Bitmap(Options.Width + Options.FontSize, Options.Height);
            //根据上面创建的位图对象创建绘图图面
            gph = Graphics.FromImage(bitMap);

            //设定验证码图片背景色
            gph.Clear(GetControllableColor(200));

            var random = new Random();
            //产生随机干扰线条
            for (int i = 0; i < 10; i++)
            {
                var backPen = new Pen(GetControllableColor(150), 1);
                //线条起点
                int x = random.Next(3, Options.Width / 2);
                int y = random.Next(3, Options.Height / 2);
                //线条终点
                int x2 = random.Next(Options.Width / 2, Options.Width);
                int y2 = random.Next(Options.Height / 2, Options.Height);
                //划线
                gph.DrawLine(backPen, x, y, x2, y2);
            }

            //定义一个含10种字体的数组
            String[] fontFamily = { "Arial", "Georgia" };
            var sb = new SolidBrush(GetControllableColor(0));

            //通过循环,绘制每个字符,
            for (int i = 0; i < Options.VerifyCode.Length; i++)
            {
                var textFont = new Font(fontFamily[random.Next(fontFamily.Length)], Options.FontSize, FontStyle.Bold);
                //字体随机,字号大小30,加粗 

                //每次循环绘制一个字符,设置字体格式,画笔颜色,字符相对画布的X坐标,字符相对画布的Y坐标
                var space =
                    (int)
                    Math.Round(
                        (double)((Options.Width - Options.FontSize * (Options.VerifyCodeLen + 2)) / Options.VerifyCodeLen));
                //纵坐标
                var y = (int)Math.Round((double)((Options.Height - Options.FontSize) / 3));
                gph.DrawString(Options.VerifyCode.Substring(i, 1), textFont, sb,
                               Options.FontSize + i * (Options.FontSize + space), y);
            }

            //扭曲图片
            bitMap = TwistImage(bitMap, true, random.Next(3, 4), random.Next(2, 3));

            try
            {
                bitMap.Save(_imageStream, ImageFormat.Gif);
            }
            catch (Exception ex)
            {
            }

            //gph.Dispose();
            bitMap.Dispose();

            //gph.DrawImage(img, 50, 20, Options.Width, 10);

            return _imageStream;
        }

        /// <summary>
        ///   产生一种 R,G,B 均大于 colorBase 随机颜色，以确保颜色不会过深
        /// </summary>
        /// <returns> 背景色 </returns>
        private Color GetControllableColor(int colorBase)
        {
            if (colorBase > 200)
            {
            }
            var random = new Random();
            //确保 R,G,B 均大于 colorBase，这样才能保证背景色较浅
            var color = Color.FromArgb(random.Next(56) + colorBase, random.Next(56) + colorBase, random.Next(56) + colorBase);
            return color;
        }

        /// <summary>
        ///   扭曲图片
        /// </summary>
        /// <param name="srcBmp"> </param>
        /// <param name="bXDir"> </param>
        /// <param name="dMultValue"> </param>
        /// <param name="dPhase"> </param>
        /// <returns> </returns>
        private Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            var leftMargin = 0;
            var rightMargin = 0;
            var topMargin = 0;
            var bottomMargin = 0;
            //float PI = 3.14159265358979f;
            float PI2 = 6.28318530717959f;
            var destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);
            double dBaseAxisLen = bXDir ? Convert.ToDouble(destBmp.Height) : Convert.ToDouble(destBmp.Width);
            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = bXDir ? PI2 * Convert.ToDouble(j) / dBaseAxisLen : PI2 * Convert.ToDouble(i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);

                    //取得当前点的颜色        
                    int nOldX = 0;
                    int nOldY = 0;
                    nOldX = bXDir ? i + Convert.ToInt32(dy * dMultValue) : i;
                    nOldY = bXDir ? j : j + Convert.ToInt32(dy * dMultValue);
                    Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= leftMargin && nOldX < destBmp.Width - rightMargin && nOldY >= bottomMargin &&
                        nOldY < destBmp.Height - topMargin)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }
            return destBmp;
        }
    }
}