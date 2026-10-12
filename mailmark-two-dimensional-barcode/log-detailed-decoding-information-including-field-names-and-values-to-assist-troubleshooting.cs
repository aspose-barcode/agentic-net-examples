// Title: Log detailed decoding information from a barcode image
// Description: Demonstrates generating a Code128 barcode, saving it as PNG, then reading it back and logging comprehensive decoding details for troubleshooting.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader to decode them. Developers commonly use these APIs to embed barcodes in documents, validate scanned data, or troubleshoot decoding issues by inspecting detailed result fields such as region, angle, and extended information.
// Prompt: Log detailed decoding information, including field names and values, to assist troubleshooting.
// Tags: barcode, code128, decoding, logging, aspose.barcode, generation, recognition, png, console

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates a barcode, reads it back, and logs detailed decoding information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode image, decodes it, and writes detailed information to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------
        // Create a unique temporary folder to store the generated barcode image.
        // --------------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "sample.png");

        // --------------------------------------------------------------
        // Generate a Code128 barcode with custom dimensions and save it.
        // --------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4;      // Width of a single barcode module.
            generator.Parameters.Barcode.BarHeight.Pixels = 50;    // Height of the barcode bars.
            generator.Save(imagePath, BarCodeImageFormat.Png);     // Save as PNG file.
        }

        // --------------------------------------------------------------
        // Verify that the image file was created successfully.
        // --------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------
        // Read the barcode from the image and log detailed decoding data.
        // --------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            BarCodeResult[] results = reader.ReadBarCodes();
            Console.WriteLine($"Barcodes detected: {results.Length}");

            int count = 1;
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"--- Barcode {count} ---");
                Console.WriteLine($"CodeTypeName: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");

                // Region information provides the location and size of the detected barcode.
                var rect = result.Region.Rectangle;
                Console.WriteLine($"Region: X={rect.X}, Y={rect.Y}, Width={rect.Width}, Height={rect.Height}");
                Console.WriteLine($"Angle: {result.Region.Angle}");

                // Extended information may contain additional data specific to certain symbologies.
                if (result.Extended != null)
                {
                    Console.WriteLine($"ExtendedInfoType: {result.Extended.GetType().Name}");
                }

                count++;
            }
        }

        // --------------------------------------------------------------
        // Optional cleanup: delete the temporary folder and its contents.
        // Uncomment the line below to enable automatic cleanup.
        // --------------------------------------------------------------
        // Directory.Delete(tempDir, true);
    }
}