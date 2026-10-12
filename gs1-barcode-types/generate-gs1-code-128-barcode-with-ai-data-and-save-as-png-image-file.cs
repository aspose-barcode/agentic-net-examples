// Title: Generate GS1 Code 128 barcode with AI data and save as PNG
// Description: Demonstrates creating a GS1 Code 128 barcode that includes Application Identifier (AI) data and exporting it to a PNG file using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical scenarios include encoding GS1 standards for product identification, such as GTIN and serial numbers, where developers need to embed AI data in barcodes for supply chain applications.
// Prompt: Generate a GS1 Code 128 barcode with AI data and save as a PNG image file.
// Tags: gs1,code128,barcode,generation,png,aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a GS1 Code 128 barcode containing AI data and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PNG file
        string outPath = Path.Combine(outputDir, "GS1Code128.png");

        // Barcode content with GS1 Application Identifiers:
        // (01) – GTIN-14, (21) – Serial Number
        string codeText = "(01)12345678901231(21)ABC123";

        // Create a barcode generator for GS1 Code 128 using the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Set the X-dimension (module width) to 2 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save the generated barcode as a PNG image
            generator.Save(outPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"GS1 Code 128 barcode saved to: {outPath}");
    }
}