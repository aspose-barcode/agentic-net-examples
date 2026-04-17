using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            using (Bitmap barcodeBitmap = generator.GenerateBarCodeImage())
            {
                using (Bitmap grayBitmap = new Bitmap(barcodeBitmap.Width, barcodeBitmap.Height))
                {
                    using (Graphics graphics = Graphics.FromImage(grayBitmap))
                    {
                        float[][] matrixElements =
                        {
                            new float[] {0.3f, 0.3f, 0.3f, 0f, 0f},
                            new float[] {0.59f, 0.59f, 0.59f, 0f, 0f},
                            new float[] {0.11f, 0.11f, 0.11f, 0f, 0f},
                            new float[] {0f, 0f, 0f, 1f, 0f},
                            new float[] {0f, 0f, 0f, 0f, 1f}
                        };
                        ColorMatrix colorMatrix = new ColorMatrix(matrixElements);
                        using (ImageAttributes attributes = new ImageAttributes())
                        {
                            attributes.SetColorMatrix(colorMatrix);
                            graphics.DrawImage(
                                barcodeBitmap,
                                new Rectangle(0, 0, grayBitmap.Width, grayBitmap.Height),
                                0,
                                0,
                                barcodeBitmap.Width,
                                barcodeBitmap.Height,
                                GraphicsUnit.Pixel,
                                attributes);
                        }
                    }

                    using (FileStream fileStream = new FileStream("barcode_gray.jpg", FileMode.Create, FileAccess.Write))
                    {
                        grayBitmap.Save(fileStream, ImageFormat.Jpeg);
                    }
                }
            }
        }
    }
}