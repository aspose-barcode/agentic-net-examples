// Title: Retrieve DotCode version and error correction level from a scanned barcode
// Description: Demonstrates generating a DotCode barcode, scanning it, and attempting to read version and error correction level information using Aspose.BarCode. Shows how to access available extended DotCode metadata.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It illustrates the use of BarcodeGenerator to create a DotCode symbol and BarCodeReader to decode it. Developers working with 2D symbologies such as DotCode often need to extract metadata like version, error correction level, and structured append details. The key API classes shown are BarcodeGenerator, BarCodeReader, BarCodeResult, and the Extended property for accessing DotCode-specific information.
// Prompt: Obtain DotCode version information and error correction level from a scanned DotCode barcode.
// Tags: dotcode, barcode, version, error correction, recognition, generation, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a DotCode barcode, reads it back, and displays available metadata.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder and file path for the generated barcode image.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "DotCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "dotcode.png");

        // --------------------------------------------------------------------
        // Generate a DotCode barcode with a sample text.
        // --------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DotCode, "AsposeDemo"))
        {
            // Set the X-dimension (module size) to 4 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Save the barcode image as PNG.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify that the barcode image was created successfully.
        // --------------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Read the DotCode barcode and attempt to obtain version and error correction info.
        // --------------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.DotCode))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Basic barcode information.
                Console.WriteLine($"Code Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");

                // Version and error correction level are not exposed by the current API.
                Console.WriteLine("Version Information: Not available via Aspose.BarCode API.");
                Console.WriteLine("Error Correction Level: Not available via Aspose.BarCode API.");

                // Display any extended DotCode metadata that is available.
                if (result.Extended?.DotCode != null)
                {
                    Console.WriteLine($"IsReaderInitialization: {result.Extended.DotCode.IsReaderInitialization}");
                    Console.WriteLine($"Structured Append Barcodes Count: {result.Extended.DotCode.StructuredAppendModeBarcodesCount}");
                    Console.WriteLine($"Structured Append Barcode Id: {result.Extended.DotCode.StructuredAppendModeBarcodeId}");
                }
                else
                {
                    Console.WriteLine("No extended DotCode metadata available.");
                }
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and directories.
        // --------------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome.
        }
    }
}