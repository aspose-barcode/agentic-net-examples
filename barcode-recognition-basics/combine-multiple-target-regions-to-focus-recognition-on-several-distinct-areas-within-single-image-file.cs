using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Create a blank canvas
        using (Bitmap canvas = new Bitmap(400, 200, PixelFormat.Format24bppRgb))
        {
            using (Graphics graphics = Graphics.FromImage(canvas))
            {
                graphics.Clear(Color.White);

                // Generate first barcode (Code128)
                using (MemoryStream ms1 = new MemoryStream())
                {
                    using (BarcodeGenerator gen1 = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
                    {
                        gen1.Save(ms1, BarCodeImageFormat.Png);
                    }
                    ms1.Position = 0;
                    using (Bitmap bmp1 = new Bitmap(ms1))
                    {
                        // Draw first barcode at (10,10)
                        graphics.DrawImage(bmp1, 10, 10, bmp1.Width, bmp1.Height);
                    }
                }

                // Generate second barcode (QR)
                using (MemoryStream ms2 = new MemoryStream())
                {
                    using (BarcodeGenerator gen2 = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
                    {
                        gen2.Save(ms2, BarCodeImageFormat.Png);
                    }
                    ms2.Position = 0;
                    using (Bitmap bmp2 = new Bitmap(ms2))
                    {
                        // Draw second barcode at (250,30)
                        graphics.DrawImage(bmp2, 250, 30, bmp2.Width, bmp2.Height);
                    }
                }
            }

            // Define target regions covering the two barcodes
            Rectangle[] targetRegions = new Rectangle[]
            {
                new Rectangle(10, 10, 150, 50),   // Approximate area of first barcode
                new Rectangle(250, 30, 120, 120) // Approximate area of second barcode
            };

            // Initialize reader and configure it to scan all supported types
            using (BarCodeReader reader = new BarCodeReader())
            {
                reader.BarCodeReadType = DecodeType.AllSupportedTypes;
                reader.SetBarCodeImage(canvas, targetRegions);

                // Perform recognition
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Type: {result.CodeTypeName}");
                    Console.WriteLine($"Text: {result.CodeText}");
                    Rectangle bounds = result.Region.Rectangle;
                    Console.WriteLine($"Region: X={bounds.X}, Y={bounds.Y}, Width={bounds.Width}, Height={bounds.Height}");
                    Console.WriteLine();
                }
            }
        }
    }
}