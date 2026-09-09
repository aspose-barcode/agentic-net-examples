// Title: Generate and Rotate a GS1 Code 128 Barcode to BMP
// Description: This example creates a GS1 Code 128 barcode, rotates the image 90 degrees clockwise, and saves it as a BMP file.
// Category-Description: Demonstrates barcode generation and image manipulation with Aspose.BarCode. The sample uses BarcodeGenerator to produce a GS1‑compliant Code 128 symbol, applies a rotation transformation, and exports the result to BMP format. Developers commonly need to generate GS1 barcodes, adjust orientation for printing or display, and select suitable image types for downstream processing.
// Prompt: Create a GS1 Code 128 barcode, rotate the image 90 degrees clockwise, and store as BMP.
// Tags: gs1,code128,barcode,rotation,bmp,aspose.barcode,generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a GS1 Code 128 barcode, rotate it, and save as BMP using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, applies rotation, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting BMP file
        string outputPath = Path.Combine(outputDir, "Gs1Code128_Rotated.bmp");

        // GS1 Code 128 requires a 14‑digit GTIN prefixed with the (01) Application Identifier
        string codeText = "(01)12345678901231";

        // Initialize the barcode generator with GS1 Code 128 symbology and the data string
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Set rotation angle to 90 degrees clockwise
            generator.Parameters.RotationAngle = 90;

            // Save the generated barcode image as BMP
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}