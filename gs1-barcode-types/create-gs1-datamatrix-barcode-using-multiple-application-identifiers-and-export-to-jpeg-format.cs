// Title: Create GS1 DataMatrix barcode with multiple Application Identifiers and export to JPEG
// Description: This example generates a GS1 DataMatrix barcode that includes several Application Identifiers and saves it as a JPEG image.
// Category-Description: Demonstrates Aspose.BarCode generation of GS1 DataMatrix symbols, a common requirement in supply‑chain and retail labeling. It uses the BarcodeGenerator class with EncodeTypes.GS1DataMatrix and BarCodeImageFormat to produce an image file. Developers often need to embed multiple AIs for product, serial, and batch information, and this snippet shows the typical workflow for creating and exporting such barcodes.
// Prompt: Create a GS1 DataMatrix barcode using multiple Application Identifiers and export to a JPEG format.
// Tags: gs1, datamatrix, barcode, generation, jpeg, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates creating a GS1 DataMatrix barcode with multiple Application Identifiers and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Build the output directory path relative to the current working directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        // Ensure the directory exists
        Directory.CreateDirectory(outputDir);
        // Full path for the resulting JPEG file
        string outputPath = Path.Combine(outputDir, "GS1DataMatrix.jpg");

        // GS1 DataMatrix payload containing multiple Application Identifiers:
        // (01) – GTIN, (21) – Serial Number, (30) – Variable Count
        string codeText = "(01)12345678901231(21)ASPOSE(30)9876";

        // Initialize the barcode generator with GS1 DataMatrix symbology and the payload
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Set the module size (X-dimension) in pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 8f;
            // Save the generated barcode as a JPEG image
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"GS1 DataMatrix barcode saved to: {outputPath}");
    }
}