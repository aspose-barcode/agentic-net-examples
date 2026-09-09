// Title: Micro PDF417 Barcode Generation with Code128 Emulation and Reading
// Description: Demonstrates generating a Micro PDF417 barcode with the Code128 emulation flag enabled, saving it as PNG, then reading the barcode to verify the flag state.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a Micro PDF417 symbol with Code128 emulation, and BarCodeReader to decode the symbol and access extended PDF417 properties. Developers working with compact data encoding, secure document printing, or inventory labeling often need to generate and validate Micro PDF417 barcodes using these core API classes.
// Prompt: Identify Micro PDF417 Code128 emulation flag and handle accordingly in processing logic.
// Tags: micro pdf417, code128 emulation, barcode generation, barcode recognition, aspose.barcode, png output, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates creating a Micro PDF417 barcode with Code128 emulation,
/// saving it as an image, reading it back, and displaying the emulation flag state.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates, reads, and cleans up a Micro PDF417 barcode.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "MicroPdf417Demo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the full path for the generated PNG image
        string imagePath = Path.Combine(tempDir, "MicroPdf417.png");

        // ------------------------------------------------------------
        // Generate a Micro PDF417 barcode with the Code128 emulation flag enabled
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.MicroPdf417, "123456789012345678"))
        {
            // Set the X-dimension (module width) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Enable Code128 emulation for the PDF417 barcode
            generator.Parameters.Barcode.Pdf417.IsCode128Emulation = true;

            // Save the barcode as a PNG image
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Read the generated barcode and output the Code128 emulation flag state
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.MicroPdf417))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"IsCode128Emulation: {result.Extended.Pdf417.IsCode128Emulation}");
            }
        }

        // ------------------------------------------------------------
        // Clean up temporary files and directory
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);

            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}