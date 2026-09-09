// Title: Generate GS1 Code 128 barcode with AI data and save as PNG
// Description: Demonstrates creating a GS1 Code 128 barcode containing Application Identifiers (AI) for GTIN and serial number, then saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.GS1Code128. Developers often need to embed AI data for product identification and serialization, and this snippet shows typical setup, dimension configuration, and image export for such use cases.
// Prompt: Generate a GS1 Code 128 barcode with AI data and save as a PNG image file.
// Tags: gs1code128, barcode generation, png output, aicode, aspnet.barcode, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a GS1 Code 128 barcode with AI data and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, generates the barcode, and saves the image.
    /// </summary>
    static void Main()
    {
        // Determine output folder path relative to current directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full file path for the resulting PNG image
        string filePath = Path.Combine(outputDir, "GS1Code128.png");

        // Sample GS1 Code 128 string with Application Identifiers:
        // (01) – GTIN (14 digits), (21) – Serial number
        string codeText = "(01)12345678901231(21)ABC123";

        // Initialize barcode generator with GS1 Code 128 symbology and the AI data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Set X-dimension (module width) to 2 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            // Save the generated barcode as a PNG image
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"GS1 Code 128 barcode saved to: {filePath}");
    }
}