using System.Drawing;
using System.Drawing.Imaging;

namespace MWUtils
{
    public class IMGUtil
    {
        /// <summary>
        /// 获取屏幕指定坐标和大小的截图
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Bitmap GetScreenCapture(int dx, int dy, int width, int height)
        {
            Rectangle tScreenRect = new Rectangle(0, 0, width, height);
            //Rectangle tScreenRect = new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Bitmap tSrcBmp = new Bitmap(tScreenRect.Width, tScreenRect.Height); // 用于屏幕原始图片保存
            Graphics gp = Graphics.FromImage(tSrcBmp);
            gp.CopyFromScreen(dx, dy, 0, 0, tScreenRect.Size);
            gp.DrawImage(tSrcBmp, 0, 0, tScreenRect, GraphicsUnit.Pixel);
            return tSrcBmp;
        }

        /// <summary>
        /// 判断两个 BMP 是否完全匹配
        /// </summary>
        /// <param name="bmp1"></param>
        /// <param name="bmp2"></param>
        /// <returns></returns>
        public static bool BmpIsMatch(Bitmap bmp1, Bitmap bmp2)
        {
            if (bmp1 == null || bmp2 == null)
            {
                return false;
            }
            if (bmp1.Width != bmp2.Width || bmp1.Height != bmp2.Height)
            {
                return false;
            }

            for (int y = 0; y < bmp1.Height; y++)
            {
                for (int x = 0; x < bmp1.Width; x++)
                {
                    Color c1 = bmp1.GetPixel(x, y);
                    Color c2 = bmp2.GetPixel(x, y);
                    if (c1.R != c2.R
                        || c1.G != c2.G
                        || c1.B != c2.B
                        || c1.A != c2.A)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        /// <summary>
        /// 大图内查找小图，效率低，待优化
        /// </summary>
        /// <param name="largeImage"></param>
        /// <param name="smallImage"></param>
        /// <returns></returns>
        public static Point TemplateMatch(Bitmap largeImage, Bitmap smallImage)
        {
            var largeData = largeImage.LockBits(new Rectangle(0, 0, largeImage.Width, largeImage.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var smallData = smallImage.LockBits(new Rectangle(0, 0, smallImage.Width, smallImage.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int width = largeData.Width - smallData.Width + 1;
            int height = largeData.Height - smallData.Height + 1;
            float[,] scoreMap = new float[width, height];
            // 计算误差分数
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    float score = CalculateScore(largeData, smallData, x, y);
                    scoreMap[x, y] = score;
                }
            }
            largeImage.UnlockBits(largeData);
            smallImage.UnlockBits(smallData);
            // 找到最佳匹配位置
            float maxScore = float.MinValue;
            Point maxPos = Point.Empty;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (scoreMap[x, y] > maxScore)
                    {
                        maxScore = scoreMap[x, y];
                        maxPos = new Point(x, y);
                    }
                }
            }
            return maxPos;
        }
        private static float CalculateScore(BitmapData largeData, BitmapData smallData, int x, int y)
        {
            int width = smallData.Width;
            int height = smallData.Height;
            int strideL = largeData.Stride;
            int strideS = smallData.Stride;
            int pixelSize = 4;
            float sum = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    unsafe
                    {
                        byte* pL = (byte*)largeData.Scan0.ToPointer() + (y + j) * strideL + (x + i) * pixelSize;
                        byte* pS = (byte*)smallData.Scan0.ToPointer() + j * strideS + i * pixelSize;
                        float diff = (*pL - *pS) / 255.0f;
                        sum += diff * diff;
                    }
                }
            }
            return -sum / (width * height);
        }


    }
}
