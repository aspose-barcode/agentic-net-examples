// Title: Retrieve DotCode Version and Error Correction Level from Scanned Barcode
// Description: Demonstrates generating a DotCode barcode, scanning it, and accessing basic decoded data. The example shows where version and error‑correction information would be retrieved if supported by the Aspose.BarCode API.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It uses the BarcodeGenerator class to create a DotCode symbol and the BarCodeReader class to decode it. Typical scenarios include validating DotCode data in logistics, inventory, or manufacturing systems where developers need to extract encoded information and, when available, symbol metadata such as version and error‑correction level.
// Prompt: Obtain DotCode version information and error correction level from a scanned DotCode barcode.
// Tags: dotcode, barcode, version, error correction, recognition, generation, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample program that creates a DotCode barcode, reads it back, and displays decoded information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a DotCode barcode, decodes it, and outputs the result.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "DotCodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "dotcode.png");

        // Generate a DotCode barcode using default settings (sufficient for this demo)
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, "1234567890"))
        {
            // Save the barcode image as PNG
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Initialize a reader for DotCode barcodes and decode the image
        using (var reader = new BarCodeReader(barcodePath, DecodeType.DotCode))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Output the decoded text
                Console.WriteLine($"Decoded Code Text: {result.CodeText}");

                // Placeholder for extended DotCode metadata (Version, ErrorLevel) when supported by the API
                // Example: Console.WriteLine($"Version: {result.Version}, Error Level: {result.ErrorLevel}");
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failures should not affect the demo
        }
    }
}