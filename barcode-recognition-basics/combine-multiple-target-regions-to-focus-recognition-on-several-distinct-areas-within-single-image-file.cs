// Title: Combine Multiple Target Regions for Barcode Recognition
// Description: Demonstrates how to define several rectangular regions in a single image and read barcodes only from those areas.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showcasing the use of BarCodeReader with target regions. It highlights key API classes such as BarCodeReader, DecodeType, and Rectangle to focus scanning on specific parts of an image. Developers often need to limit recognition to certain zones to improve performance or avoid false positives, especially when multiple barcodes are present in one picture.
// Prompt: Combine multiple target regions to focus recognition on several distinct areas within a single image file.
// Tags: barcode, recognition, target region, code128, qr, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates an image containing two different barcodes,
/// defines separate target regions for each, and reads them using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a combined barcode image, sets up target regions,
    /// performs recognition, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a temporary working directory to store the generated image.
        // --------------------------------------------------------------------
        string workDir = Path.Combine(Path.GetTempPath(), "BarcodeRegionDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);
        string imagePath = Path.Combine(workDir, "combined.png");

        // --------------------------------------------------------------------
        // Create a blank canvas and draw two barcodes onto it.
        // --------------------------------------------------------------------
        using (Bitmap canvas = new Bitmap(800, 400))
        {
            using (Graphics g = Graphics.FromImage(canvas))
            {
                // Fill background with white.
                g.Clear(Color.White);

                // ----- First barcode: Code128 -----
                using (MemoryStream ms1 = new MemoryStream())
                {
                    BarcodeGenerator gen1 = new BarcodeGenerator(EncodeTypes.Code128, "CODE128TEXT");
                    gen1.Save(ms1, BarCodeImageFormat.Png);
                    ms1.Position = 0;
                    using (Bitmap bmp1 = new Bitmap(ms1))
                    {
                        // Draw the Code128 barcode at (50,100).
                        g.DrawImage(bmp1, 50, 100, bmp1.Width, bmp1.Height);
                    }
                }

                // ----- Second barcode: QR -----
                using (MemoryStream ms2 = new MemoryStream())
                {
                    BarcodeGenerator gen2 = new BarcodeGenerator(EncodeTypes.QR, "https://example.com");
                    gen2.Save(ms2, BarCodeImageFormat.Png);
                    ms2.Position = 0;
                    using (Bitmap bmp2 = new Bitmap(ms2))
                    {
                        // Draw the QR code at (450,100).
                        g.DrawImage(bmp2, 450, 100, bmp2.Width, bmp2.Height);
                    }
                }
            }

            // Save the combined image to disk.
            canvas.Save(imagePath, ImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Define rectangular target regions that roughly cover each barcode.
        // --------------------------------------------------------------------
        Rectangle regionCode128 = new Rectangle(50, 100, 300, 100);   // Area of the Code128 barcode
        Rectangle regionQR = new Rectangle(450, 100, 300, 300);      // Area of the QR code

        // --------------------------------------------------------------------
        // Perform barcode recognition limited to the defined regions.
        // --------------------------------------------------------------------
        using (Bitmap sourceBmp = new Bitmap(imagePath))
        {
            using (BarCodeReader reader = new BarCodeReader())
            {
                // Set the image and the array of target regions.
                reader.SetBarCodeImage(sourceBmp, new Rectangle[] { regionCode128, regionQR });

                // Restrict decoding to the expected symbologies.
                reader.SetBarCodeReadType(DecodeType.Code128, DecodeType.QR);

                Console.WriteLine("Reading barcodes from multiple regions:");
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and directory.
        // --------------------------------------------------------------------
        try
        {
            File.Delete(imagePath);
            Directory.Delete(workDir, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program exit.
        }
    }
}