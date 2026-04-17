using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string codeText = "HanXinTest123";

        using (BarcodeGenerator generator1 = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            generator1.Parameters.AutoSizeMode = AutoSizeMode.None;
            generator1.Parameters.Barcode.XDimension.Point = 2f; // 2 points per module

            using (Bitmap bitmap1 = generator1.GenerateBarCodeImage())
            {
                int width1 = bitmap1.Width;
                int height1 = bitmap1.Height;

                using (BarcodeGenerator generator2 = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
                {
                    generator2.Parameters.AutoSizeMode = AutoSizeMode.None;
                    generator2.Parameters.Barcode.XDimension.Point = 4f; // double the previous size

                    using (Bitmap bitmap2 = generator2.GenerateBarCodeImage())
                    {
                        int width2 = bitmap2.Width;
                        int height2 = bitmap2.Height;

                        bool widthOk = Math.Abs(width2 - 2 * width1) <= 2;
                        bool heightOk = Math.Abs(height2 - 2 * height1) <= 2;

                        if (widthOk && heightOk)
                        {
                            Console.WriteLine("Test passed: dimensions scale proportionally with XDimension.");
                            Console.WriteLine($"Size1: {width1}x{height1}, Size2: {width2}x{height2}");
                        }
                        else
                        {
                            Console.WriteLine("Test failed: dimensions did not scale as expected.");
                            Console.WriteLine($"Size1: {width1}x{height1}, Size2: {width2}x{height2}");
                        }

                        bitmap1.Save("HanXin_small.png", ImageFormat.Png);
                        bitmap2.Save("HanXin_large.png", ImageFormat.Png);
                    }
                }
            }
        }
    }
}