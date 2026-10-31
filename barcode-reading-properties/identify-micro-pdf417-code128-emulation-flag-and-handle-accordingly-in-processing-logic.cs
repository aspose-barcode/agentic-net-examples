// Title: Micro PDF417 Barcode Generation with Code128 Emulation and Detection
// Description: Demonstrates generating a Micro PDF417 barcode with the Code128 emulation flag enabled, saving it as a PNG image, and then reading the barcode to verify the emulation mode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create Micro PDF417 barcodes, the BarCodeReader to decode them, and the handling of the IsCode128Emulation flag. Typical use cases include encoding data in compact Micro PDF417 format while preserving compatibility with Code128 scanners, a scenario often required in logistics and inventory systems. Developers looking for barcode creation, image export, and flag‑based processing will find this pattern useful.
/// Prompt: Identify Micro PDF417 Code128 emulation flag and handle accordingly in processing logic.
/// Tags: micro pdf417, code128, barcode generation, barcode recognition, aspose.barcode, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a Micro PDF417 barcode with Code128 emulation,
/// saves it to a temporary PNG file, reads it back, and reports the emulation status.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, detection, and cleanup.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "MicroPdf417Demo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempDir, "MicroPdf417_Code128.png");

        // ------------------------------------------------------------
        // Generate a Micro PDF417 barcode with the Code128 emulation flag enabled
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.MicroPdf417, "1234567890"))
        {
            // Set barcode visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Enable Code128 emulation for Micro PDF417
            generator.Parameters.Barcode.Pdf417.IsCode128Emulation = true;

            // Save the barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode image and inspect the Code128 emulation flag
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.MicroPdf417))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("CodeText: " + result.CodeText);
                Console.WriteLine("IsCode128Emulation: " + result.Extended.Pdf417.IsCode128Emulation);

                // Output a friendly message based on the emulation flag
                if (result.Extended.Pdf417.IsCode128Emulation)
                {
                    Console.WriteLine("Detected Code128 emulation mode.");
                }
                else
                {
                    Console.WriteLine("Standard Micro PDF417 barcode.");
                }
            }
        }

        // ------------------------------------------------------------
        // Cleanup temporary files and directory
        // ------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}