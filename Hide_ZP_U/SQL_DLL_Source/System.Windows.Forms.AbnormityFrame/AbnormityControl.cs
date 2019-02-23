namespace System.Windows.Forms.AbnormityFrame
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    internal static class AbnormityControl
    {
        public static GraphicsPath CalculateControlGraphicsPath(Image image)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            Bitmap bitmap = (Bitmap) image;
            Color colorTransparent = bitmap.GetPixel(0, 0);
            int colOpaquePixel = 0;
            for (int row = 0; row < bitmap.Height; row++)
            {
                colOpaquePixel = 0;
                for (int col = 0; col < bitmap.Width; col++)
                {
                    if (!(bitmap.GetPixel(col, row) != colorTransparent))
                    {
                        continue;
                    }
                    colOpaquePixel = col;
                    int colNext = col;
                    colNext = colOpaquePixel;
                    while (colNext < bitmap.Width)
                    {
                        if (bitmap.GetPixel(colNext, row) == colorTransparent)
                        {
                            break;
                        }
                        colNext++;
                    }
                    graphicsPath.AddRectangle(new Rectangle(colOpaquePixel, row, colNext - colOpaquePixel, 1));
                    col = colNext;
                }
            }
            return graphicsPath;
        }
    }
}

