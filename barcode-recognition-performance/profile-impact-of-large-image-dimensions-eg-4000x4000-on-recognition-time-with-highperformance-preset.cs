using System;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Drawing.Drawing2D;

class Program
{
    static void Main()
    {
        // Create a simple barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            using (Bitmap barcodeBmp = generator.GenerateBarCodeImage())
            {
                // Create a large blank bitmap (4000x4000) and draw the barcode onto it
                using (Bitmap largeBmp = new Bitmap(4000, 4000))
                {
                    using (Graphics g = Graphics.FromImage(largeBmp))
                    {
                        g.Clear(Color.White);
                        int x = (largeBmp.Width - barcodeBmp.Width) / 2;
                        int y = (largeBmp.Height - barcodeBmp.Height) / 2;
                        g.DrawImage(barcodeBmp, x, y, barcodeBmp.Width, barcodeBmp.Height);
                    }

                    // Measure recognition time using HighPerformance preset
                    using (var reader = new BarCodeReader(largeBmp, DecodeType.Code128))
                    {
                        reader.QualitySettings = QualitySettings.HighPerformance;

                        Stopwatch sw = Stopwatch.StartNew();
                        BarCodeResult[] results = reader.ReadBarCodes();
                        sw.Stop();

                        Console.WriteLine($"Recognition time (HighPerformance, 4000x4000): {sw.ElapsedMilliseconds} ms");
                        foreach (BarCodeResult result in results)
                        {
                            Console.WriteLine($"Detected barcode: {result.CodeText}");
                        }
                    }
                }
            }
        }
    }
}