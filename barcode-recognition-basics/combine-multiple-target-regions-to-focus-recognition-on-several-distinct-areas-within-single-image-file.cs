using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const int canvasWidth = 800;
        const int canvasHeight = 400;

        using (var canvas = new Bitmap(canvasWidth, canvasHeight))
        {
            using (var g = Graphics.FromImage(canvas))
            {
                g.Clear(Color.White);
            }

            // First barcode: Code128
            using (var generator1 = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
            {
                using (var ms1 = new MemoryStream())
                {
                    generator1.Save(ms1, BarCodeImageFormat.Png);
                    ms1.Position = 0;
                    using (var bmp1 = new Bitmap(ms1))
                    {
                        using (var g = Graphics.FromImage(canvas))
                        {
                            g.DrawImage(bmp1, 50, 50);
                        }
                    }
                }
            }

            // Second barcode: QR
            using (var generator2 = new BarcodeGenerator(EncodeTypes.QR, "XYZ"))
            {
                using (var ms2 = new MemoryStream())
                {
                    generator2.Save(ms2, BarCodeImageFormat.Png);
                    ms2.Position = 0;
                    using (var bmp2 = new Bitmap(ms2))
                    {
                        using (var g = Graphics.FromImage(canvas))
                        {
                            g.DrawImage(bmp2, 400, 200);
                        }
                    }
                }
            }

            // Save combined image
            const string combinedPath = "combined.png";
            canvas.Save(combinedPath, ImageFormat.Png);

            var region1 = new Rectangle(50, 50, 300, 100);
            var region2 = new Rectangle(400, 200, 300, 300);
            var targetAreas = new[] { region1, region2 };

            using (var reader = new BarCodeReader())
            {
                reader.BarCodeReadType = DecodeType.AllSupportedTypes;
                reader.SetBarCodeImage(canvas, targetAreas);

                foreach (var result in reader.ReadBarCodes())
                {
                    var bounds = result.Region.Rectangle;
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text: {result.CodeText}");
                    Console.WriteLine($"Region: X={bounds.X}, Y={bounds.Y}, Width={bounds.Width}, Height={bounds.Height}");
                    Console.WriteLine();
                }
            }
        }
    }
}