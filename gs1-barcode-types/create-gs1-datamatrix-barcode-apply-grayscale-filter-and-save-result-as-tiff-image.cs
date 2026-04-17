using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string outputFile = "gs1_datamatrix_grayscale.tif";

        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, "(01)12345678901231"))
        {
            using (Bitmap originalBitmap = generator.GenerateBarCodeImage())
            {
                using (Bitmap grayBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height, originalBitmap.PixelFormat))
                {
                    using (Graphics graphics = Graphics.FromImage(grayBitmap))
                    {
                        var colorMatrix = new ColorMatrix(new float[][]
                        {
                            new float[] { 0.3f, 0.3f, 0.3f, 0, 0 },
                            new float[] { 0.59f, 0.59f, 0.59f, 0, 0 },
                            new float[] { 0.11f, 0.11f, 0.11f, 0, 0 },
                            new float[] { 0, 0, 0, 1, 0 },
                            new float[] { 0, 0, 0, 0, 1 }
                        });

                        using (ImageAttributes imgAttr = new ImageAttributes())
                        {
                            imgAttr.SetColorMatrix(colorMatrix);
                            graphics.DrawImage(
                                originalBitmap,
                                new Rectangle(0, 0, originalBitmap.Width, originalBitmap.Height),
                                0,
                                0,
                                originalBitmap.Width,
                                originalBitmap.Height,
                                GraphicsUnit.Pixel,
                                imgAttr);
                        }
                    }

                    grayBitmap.Save(outputFile, ImageFormat.Tiff);
                }
            }
        }

        Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputFile)}");
    }
}