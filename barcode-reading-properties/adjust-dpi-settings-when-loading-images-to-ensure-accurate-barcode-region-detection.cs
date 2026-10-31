// Title: Adjust DPI Settings for Accurate Barcode Detection
// Description: Demonstrates how to set and preserve DPI when generating and loading a barcode image to ensure reliable region detection.
// Category-Description: This example belongs to the Aspose.BarCode image processing category, illustrating the use of BarcodeGenerator, Bitmap, and BarCodeReader classes. It shows typical scenarios where developers need to control image resolution for precise barcode recognition, such as scanning high‑resolution prints or adjusting DPI for different output devices.
// Prompt: Adjust DPI settings when loading images to ensure accurate barcode region detection.
// Tags: barcode, dpi, resolution, barcodegeneration, barcoderecognition, code128, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a barcode image at a specific DPI,
/// loads it with the same DPI, and reads the barcode while reporting its region.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, adjusts DPI on load,
    /// reads the barcode, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a temporary folder for any files (not strictly needed here)
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDpiDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------------
        // Generate a sample barcode image with a specific resolution (300 DPI)
        // --------------------------------------------------------------------
        string barcodeText = "1234567890";
        string imagePath = Path.Combine(tempFolder, "barcode.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, barcodeText))
        {
            // Set DPI for generation
            generator.Parameters.Resolution = 300f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Load the image into a bitmap and ensure its DPI matches the generation DPI
        // --------------------------------------------------------------------
        using (var bitmap = new Bitmap(imagePath))
        {
            // Adjust bitmap resolution if necessary
            bitmap.SetResolution(300f, 300f);

            // ----------------------------------------------------------------
            // Create a reader for all supported barcode types and configure quality
            // ----------------------------------------------------------------
            using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                // Set high quality recognition settings for better accuracy
                reader.QualitySettings = QualitySettings.HighQuality;

                // Read barcodes from the image
                BarCodeResult[] results = reader.ReadBarCodes();

                // ----------------------------------------------------------------
                // Output results or indicate that no barcodes were detected
                // ----------------------------------------------------------------
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcodes detected.");
                }
                else
                {
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                        var bounds = result.Region.Rectangle;
                        Console.WriteLine($"Region - X:{bounds.X}, Y:{bounds.Y}, Width:{bounds.Width}, Height:{bounds.Height}, Angle:{result.Region.Angle}");
                    }
                }
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files
        // --------------------------------------------------------------------
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}