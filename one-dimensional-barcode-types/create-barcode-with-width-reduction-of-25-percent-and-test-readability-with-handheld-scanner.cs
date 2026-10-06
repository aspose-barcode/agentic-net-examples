// Title: Generate Code128 Barcode with 25% Width Reduction and Verify Readability
// Description: This example creates a Code128 barcode, reduces its bar width by 25 percent, saves it as a PNG image, and validates that the barcode can be read using Aspose.BarCode's scanner.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and recognition. It showcases the use of BarcodeGenerator for creating barcodes with custom dimensions and BarCodeReader for decoding them. Typical scenarios include preparing barcodes for printing with specific size constraints and ensuring they remain scannable, a common requirement for inventory, shipping, and retail applications.
// Prompt: Create a barcode with width reduction of 25 percent and test readability with a handheld scanner.
// Tags: code128, width-reduction, barcode-generation, barcode-recognition, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to generate a Code128 barcode with a 25 percent bar width reduction,
/// save it to a temporary PNG file, and verify its readability using Aspose.BarCode's
/// recognition engine.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, reads it back, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a unique temporary folder for the barcode image
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the barcode PNG file
        string barcodePath = Path.Combine(tempFolder, "barcode.png");

        // --------------------------------------------------------------------
        // Generate a Code128 barcode with a 25% bar width reduction
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set a reasonable XDimension (module width) in points
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Apply a 25 percent reduction to the bar width
            generator.Parameters.Barcode.BarWidthReduction.Point = 25f;

            // Save the generated barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {barcodePath}");

        // --------------------------------------------------------------------
        // Verify that the saved barcode can be read by the Aspose.BarCode reader
        // --------------------------------------------------------------------
        if (File.Exists(barcodePath))
        {
            using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
            {
                var results = reader.ReadBarCodes();
                if (results.Length > 0)
                {
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Detected CodeText: {result.CodeText}");
                        Console.WriteLine($"Detected CodeType: {result.CodeTypeName}");
                    }
                }
                else
                {
                    Console.WriteLine("No barcode detected. The barcode may be unreadable.");
                }
            }
        }
        else
        {
            Console.WriteLine("Barcode file was not created.");
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and directories
        // --------------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup failed: {ex.Message}");
        }
    }
}