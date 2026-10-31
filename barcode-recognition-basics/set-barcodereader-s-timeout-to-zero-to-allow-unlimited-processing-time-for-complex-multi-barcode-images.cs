// Title: Unlimited barcode reading timeout example
// Description: Demonstrates setting BarCodeReader.Timeout to zero for unlimited processing time when reading complex images containing multiple barcodes.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating how to configure the BarCodeReader for extended processing. It uses BarCodeReader, BarcodeGenerator, and related image classes to generate, combine, and decode QR and Code128 barcodes. Developers often need to adjust timeout settings for large or dense barcode images to prevent premature aborts.
// Prompt: Set BarCodeReader's TimeOut to zero to allow unlimited processing time for complex multi‑barcode images.
// Tags: barcode, timeout, unlimited, multibarcode, qrcode, code128, generation, recognition, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating QR and Code128 barcodes, combining them, and reading them with an unlimited timeout.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, combines them, reads them with unlimited timeout, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the individual and combined barcode images
        string qrPath = Path.Combine(tempFolder, "qr.png");
        string code128Path = Path.Combine(tempFolder, "code128.png");
        string combinedPath = Path.Combine(tempFolder, "combined.png");

        // Generate a QR Code image
        using (var qrGenerator = new BarcodeGenerator(EncodeTypes.QR, "Hello QR"))
        {
            qrGenerator.Save(qrPath, BarCodeImageFormat.Png);
        }

        // Generate a Code128 barcode image
        using (var code128Generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            code128Generator.Save(code128Path, BarCodeImageFormat.Png);
        }

        // Combine the two barcode images side by side into a single image
        using (Bitmap img1 = new Bitmap(qrPath))
        using (Bitmap img2 = new Bitmap(code128Path))
        {
            int combinedWidth = img1.Width + img2.Width;
            int combinedHeight = Math.Max(img1.Height, img2.Height);
            using (Bitmap combined = new Bitmap(combinedWidth, combinedHeight))
            {
                using (Graphics g = Graphics.FromImage(combined))
                {
                    g.DrawImage(img1, 0, 0);
                    g.DrawImage(img2, img1.Width, 0);
                }
                combined.Save(combinedPath, Aspose.Drawing.Imaging.ImageFormat.Png);
            }
        }

        // Verify that the combined image was successfully created
        if (!File.Exists(combinedPath))
        {
            Console.WriteLine("Failed to create combined barcode image.");
            Cleanup(tempFolder);
            return;
        }

        // Read barcodes from the combined image with unlimited timeout (Timeout = 0)
        using (BarCodeReader reader = new BarCodeReader(combinedPath, DecodeType.QR, DecodeType.Code128))
        {
            reader.Timeout = 0; // Unlimited processing time
            try
            {
                BarCodeResult[] results = reader.ReadBarCodes();
                Console.WriteLine($"Found {reader.FoundCount} barcode(s).");
                foreach (BarCodeResult result in reader.FoundBarCodes)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
            catch (RecognitionAbortedException ex)
            {
                Console.WriteLine($"Recognition aborted: {ex.Message}");
            }
        }

        // Clean up temporary files and folder
        Cleanup(tempFolder);
    }

    /// <summary>
    /// Deletes all files in the specified folder and removes the folder itself.
    /// </summary>
    /// <param name="folderPath">The path of the folder to clean up.</param>
    static void Cleanup(string folderPath)
    {
        try
        {
            if (Directory.Exists(folderPath))
            {
                foreach (string file in Directory.GetFiles(folderPath))
                {
                    File.Delete(file);
                }
                Directory.Delete(folderPath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup error: {ex.Message}");
        }
    }
}